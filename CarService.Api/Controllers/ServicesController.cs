using CarService.Api.Contracts;
using CarService.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
	private readonly ServicesService _servicesService;

	public ServicesController(ServicesService servicesService)
	{
		_servicesService = servicesService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateServiceAsync(
		CreateServiceRequest request)
	{
		var service = await _servicesService.CreateAsync(
			request.Name, request.Description
			, request.IsShowLanding);

		if (service.IsFailure)
			return BadRequest(service.Error);

		return Ok(service.Value);
	}

	[HttpGet("lending")]
	public async Task<IActionResult> GetServicesLending()
	{
		var services = await _servicesService.GetLendingAsync();

		return Ok(services);
	}

	[HttpGet]
	public async Task<IActionResult> GetServicesAsync()
	{
		var services = await _servicesService.GetAllAsync();

		return Ok(services);
	}
}