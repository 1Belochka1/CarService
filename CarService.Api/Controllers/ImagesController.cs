using System.Net.Mime;
using CarService.Api.Contracts.Images;
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
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> UploadImageAsync
		([FromForm] UploadImagesRequest request)
	{
		var result = await _imageService.UploadImageAsync(
			request.File,
			request.UserInfoId,
			request.ProductId, request.ServiceId);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok();
	}

	[HttpPost("update")]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> UpdateImageAsync(
		[FromForm] UpdateImagesRequest request)
	{
		var result = await _imageService.UpdateImageAsync(
			request.ImageId,
			request.NewFile,
			request.UserInfoId,
			request.ProductId,
			request.ServiceId);

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

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> DeleteImageAsync(
		Guid id)
	{
		var result = await _imageService.DeleteImageAsync(id);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok();
	}
}