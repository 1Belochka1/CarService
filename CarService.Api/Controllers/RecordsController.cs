using System.Security.Claims;
using CarService.Api.Contracts;
using CarService.Api.Contracts.Records;
using CarService.App.Common.ListWithPage;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

// TODO: https://localhost:7298/api/Services?Filters[0].Value=True&Filters[0].Name=IsShowLending

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
	private readonly RecordsService _recordsService;

	public RecordsController(
		RecordsService recordsService)
	{
		_recordsService = recordsService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		CreateRecordRequest request)
	{
		var result = await _recordsService.CreateRecordAsync(
			request.ClientId,
			request.Phone,
			request.CarInfo,
			request.Description,
			request.dayRecordsId
		);

		return Ok(result);
	}

	[HttpPost("Update")]
	public async Task<IActionResult> Update(
		UpdateRecordRequest request)
	{
		await _recordsService.UpdateRecordAsync(
			request.Id,
			request.Phone,
			request.Description,
			request.Priority,
			request.Status
		);

		return Ok();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		var result = await _recordsService.GetRecordByIdAsync
			(Guid.Parse(id));
		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("getCompletedByMasterId/{id}")]
	public async Task<IActionResult> GetCompletedByMasterId(
		Guid id,
		[FromQuery] GetListWithPageRequest request)
	{
		var records =
			await _recordsService
				.GetCompletedRecordsByMasterIdAsync(id, new Params(
					null,
					null,
					request.SortDescending,
					request.SearchValue,
					request.Page,
					request.PageSize,
					request.SortProperty
				));

		return Ok(records);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("getActiveByMasterId/{id}")]
	public async Task<IActionResult> GetActiveByMasterId(
		Guid id,
		[FromQuery] GetListWithPageRequest request)
	{
		var records =
			await _recordsService.GetActiveRecordsByMasterIdAsync(
				id, new Params(
					null,
					null,
					request.SortDescending,
					request.SearchValue,
					request.Page,
					request.PageSize,
					request.SortProperty
				));

		return Ok(records);
	}

	[Authorize]
	[HttpGet("getAll")]
	public async Task<IActionResult> GetAll(
		[FromQuery] GetListWithPageAndFilterRequest request)
	{
		var roleId =
			HttpContext.User.FindFirstValue(ClaimTypes.Role);

		var userId = HttpContext.User.FindFirstValue
			(ClaimTypes.NameIdentifier);

		var records = await _recordsService.GetAllRecordsAsync(
			new ParamsWhitFilter(
				Guid.Parse(userId!), roleId!,
				request.SearchValue,
				request.Filters,
				request.Page,
				request.PageSize,
				request.SortProperty,
				request.SortDescending
			)
		);

		return Ok(records);
	}

	// [HttpGet("заполнить")]
	// public async Task<IActionResult> Write()
	// {
	// 	// var users = await _usersService.GetClientsAsync(pageSize: 100);
	// 	//
	// 	// var countUser = users.Users.Count();
	// 	//
	// 	var rand = new Random();
	// 	//
	// 	// for (var i = 0; i < countUser; i++)
	// 	// {
	// 	// 	var id = rand.Next(0, 100);
	// 	// 	var user = users.Users.ElementAt(id);
	// 	// 	await _recordsService.CreateRecordAsync(user.Id,
	// 	// 		$"Запись {i} клиента {user.FullName}. Тут должно быть описание, очень много текста. " +
	// 	// 		$"Тут должно быть описание, очень много текста. Тут должно быть описание, очень много текста.");
	// 	// }
	//
	// 	var records = await _recordsService.GetAllRecordsAsync();
	//
	// 	var workers = await _usersService.GetWorkersAsync(pageSize: 10);
	//
	// 	var ids = workers.Items.Select(u => u.Id.ToString()).ToList();
	//
	//
	// 	foreach (var record in records)
	// 	{
	// 		var masters =
	// 			await _usersService.GetWorkersByIds(ids);
	// 		var id = rand.Next(0, 10);
	// 		var master = masters.ElementAt(id);
	//
	// 		await _recordsService.AddMastersAsync(record.Id, master);
	//
	// 		await _recordsService.UpdateRecordAsync(record.Id, status: RecordStatus.Work);
	// 	}
	//
	// 	return Ok();
	// }
}