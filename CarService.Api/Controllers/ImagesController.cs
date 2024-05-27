using System.Net.Mime;
using CarService.App.Interfaces.Persistence;
using CarService.App.Services;
using CarService.Core.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
	private readonly IImageRepository _imageRepository;

	private readonly UsersService _usersService;

	public ImagesController(IImageRepository imageRepository,
		UsersService usersService)
	{
		_imageRepository = imageRepository;
		_usersService = usersService;
	}

	[HttpPost]
	public async Task UploadImageAsync(IFormFile? file,
		Guid? userInfoId, Guid? productId)
	{
		if (file == null || file.Length == 0)
		{
			HttpContext.Response.StatusCode = 400;
			return;
		}

		var id = Guid.NewGuid();

		var filename = id + $"_{file.FileName}";

		var path = Path.Combine(Directory.GetCurrentDirectory(),
			"../Images", filename);

		Directory.CreateDirectory(Path.GetDirectoryName(path));

		await using (var stream =
		             new FileStream(path, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		var image = Image.Create(id, filename, null, productId,
			userInfoId);

		await _imageRepository.Create(image);
	}


	[Authorize]
	[HttpGet("{filename}")]
	public async Task GetImagesAsync(
		string filename)
	{
		var image = await _imageRepository
			.GetByName
				(filename);

		if (image == null)
		{
			HttpContext.Response.StatusCode = 404;
			return;
		}

		await HttpContext.Response
			.SendFileAsync(image.FullName);
	}
}