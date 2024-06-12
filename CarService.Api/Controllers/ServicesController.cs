using CarService.Api.Contracts;
using CarService.Api.Helper.Json;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;


public class ServicesController : ApiController
{
	private readonly ServicesService _servicesService;
	private readonly ImageService _imageService;

	public ServicesController(
		ServicesService servicesService,
		ImageService imageService)
	{
		_servicesService = servicesService;
		_imageService = imageService;
	}

	[HttpPost("create")]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> CreateServiceAsync([FromForm] CreateServiceRequest request)
	{
		var serviceResult = await _servicesService.CreateAsync(
			request.Name, request.Description
			, request.IsShowLanding);

		if (serviceResult.IsFailure)
			return BadRequest(serviceResult.Error);

		if (request.File != null)
		{
			byte[]? fileBytes;
			using (var memoryStream = new MemoryStream())
			{
				await request.File.CopyToAsync(memoryStream);
				fileBytes = memoryStream.ToArray();
			}


			var fileResult = await _imageService.UploadImageAsync
			(fileBytes, request.File.FileName,
				null, null, serviceResult.Value);

			if (fileResult.IsFailure)
			{
				return BadRequest(
					$"Ошибка при загрузке файла: ${fileResult.Error}");
			}
		}

		var service = await _servicesService.GetById(
			serviceResult
				.Value);

		return Ok(service.Value);
	}

	[HttpGet("get/lending")]
	public async Task<IActionResult> GetServicesLending()
	{
		var services = await _servicesService.GetLendingAsync();

		return Ok(services);
	}

	[HttpGet("get/all")]
	public async Task<IActionResult> GetServicesAsync()
	{
		var services = await _servicesService.GetAllAsync();

		return Ok(services);
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> GetServicesAsync(Guid id)
	{
		var service = await _servicesService.GetById(id);

		if (service.IsFailure)
			return BadRequest(service.Error);

		return Ok(service.Value);
	}

	[HttpPost("update")]
	public async Task<IActionResult> Update(UpdateServiceRequest request)
	{
		var serviceResult = await _servicesService.UpdateAsync(
			request.Id,
			request.Name,
			request.Description,
			request.IsShowLending
		);

		if (serviceResult.IsFailure)
			return BadRequest(serviceResult.Error);

		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		try
		{
			await _servicesService.DeleteAsync(id);
		}
		catch (Exception e)
		{
			return BadRequest("На сервере прозошла ошибка");
		}

		return Ok();
	}

	[Authorize(Roles = "1")]
	[HttpGet("Get/Autocomplete")]
	public async Task<IActionResult>
		GetWorkersForAutocomplete()
	{
		return Ok(JsonSerializerHelp.Serialize(
			await _servicesService.GetServicesForAutocomplete()));
	}
}

public record UpdateServiceRequest(
	Guid Id,
	string Name,
	string Description,
	bool IsShowLending);