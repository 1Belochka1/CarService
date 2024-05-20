using CarService.Api.Contracts;
using CarService.Api.Contracts.Records;
using CarService.App.Common.ListWithPage;
using CarService.App.Services;
using CarService.Core.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
	private readonly RecordsService _recordsService;
	private readonly UsersService _usersService;

	public RecordsController(RecordsService recordsService, UsersService usersService)
	{
		_recordsService = recordsService;
		_usersService = usersService;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateRecordRequest request)
	{
		var result = await _recordsService.CreateRecordAsync(
			request.ClientId,
			request.Description
		);

		return Ok(result);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("getCompletedByMasterId/{id}")]
	public async Task<IActionResult> GetCompletedByMasterId(Guid id, [FromQuery] GetListWithPageRequest request)
	{
		var records = await _recordsService.GetCompletedRecordsByMasterIdAsync(id, new Params(
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
	public async Task<IActionResult> GetActiveByMasterId(Guid id, [FromQuery] GetListWithPageRequest request)
	{
		var records = await _recordsService.GetActiveRecordsByMasterIdAsync(id, new Params(
			request.SortDescending,
			request.SearchValue,
			request.Page,
			request.PageSize,
			request.SortProperty
		));

		return Ok(records);
	}

	[Authorize(Roles = "1")]
	[HttpGet("getAll")]
	public async Task<IActionResult> GetAll()
	{
		var records = await _recordsService.GetAllRecordsAsync();

		Console.WriteLine(records.Count);

		return Ok(records.ToList());
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