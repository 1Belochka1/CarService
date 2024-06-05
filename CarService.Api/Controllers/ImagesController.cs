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
	private readonly ImageService _imageService;

	private readonly UsersService _usersService;

	public ImagesController(ImageService imageService,
		UsersService usersService)
	{
		_imageService = imageService;
		_usersService = usersService;
	}

	[HttpPost("upload")]
	public async Task<IActionResult> UploadImageAsync(
		IFormFile? file,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		var result = await _imageService.UploadImageAsync(file,
			userInfoId,
			productId, serviceId);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok();
	}

	[HttpPost("update")]
	public async Task<IActionResult> UpdateImageAsync(
		Guid imageId,
		IFormFile? newFile,
		Guid? userInfoId, Guid? productId, Guid? serviceId)
	{
		var result = await _imageService.UpdateImageAsync(
			imageId,
			newFile,
			userInfoId,
			productId, serviceId);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok();
	}

	[HttpGet("get/{imageId}")]
	public async Task GetImagesAsync(
		Guid imageId)
	{
		var image = await _imageService
			.GetImageAsync(imageId);

		if (image.IsFailure)
		{
			HttpContext.Response.StatusCode = 404;
			return;
		}

		await HttpContext.Response
			.SendFileAsync(image.Value.FullName);
	}
}