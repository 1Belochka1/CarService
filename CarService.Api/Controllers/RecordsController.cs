using System.Security.Claims;
using CarService.Api.Contracts.Records;
using CarService.Api.Hubs;
using CarService.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Controllers;

// TODO: https://localhost:7298/api/Services?Filters[0].Value=True&Filters[0].Name=IsShowLending

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
	private readonly RecordsService _recordsService;
	private readonly IHubContext<TimeRecordsHub> _hubContext;

	public RecordsController(
		RecordsService recordsService,
		IHubContext<TimeRecordsHub>
			hubContext)
	{
		_recordsService = recordsService;
		_hubContext = hubContext;
	}

	[HttpPost("Create/WithoutUserAuth")]
	public async Task<IActionResult>
		CreateRecordWithoutAuthUser(
			CreateRecordWithoutAuthUserRequest request)
	{
		var result =
			await _recordsService.CreateWithoutAuthUser(
				request.Phone,
				request.FirstName,
				request.CarInfo,
				request.Description,
				request.DayRecordsId
			);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
	}

	[HttpPost("Create")]
	public async Task<IActionResult> CreateRecord(
		CreateRecordRequest request)
	{
		var result =
			await _recordsService.CreateRecordAsync(
				request.ClientId,
				request.CarInfo,
				request.Description,
				request.DayRecordsId
			);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
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

	[HttpPost("Update/AddMaster/{id}")]
	public async Task<IActionResult> AddMasters(Guid id,
		List<Guid> mastersIds)
	{
		await _recordsService.AddMastersAsync(
			id, mastersIds
		);

		return Ok();
	}

	[HttpGet("Get/{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		var result = await _recordsService.GetRecordByIdAsync
			(Guid.Parse(id));
		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("Get/CompletedByMasterId/{id}")]
	public async Task<IActionResult> GetCompletedByMasterId(
		Guid id)
	{
		var records =
			await _recordsService
				.GetCompletedRecordsByMasterIdAsync(id);

		return Ok(records);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("Get/ActiveByMasterId/{id}")]
	public async Task<IActionResult> GetActiveByMasterId(
		Guid id)
	{
		var records =
			await _recordsService.GetActiveRecordsByMasterIdAsync(
				id);

		return Ok(records);
	}

	[Authorize]
	[HttpGet("Get/All")]
	public async Task<IActionResult> GetAll()
	{
		var roleId =
			HttpContext.User.FindFirstValue(ClaimTypes.Role);

		var userId = HttpContext.User.FindFirstValue
			(ClaimTypes.NameIdentifier);

		var records = await _recordsService
			.GetAllRecordsAsync(roleId!, Guid.Parse(userId!));

		return Ok(records);
	}

	[Authorize(Roles = "1")]
	[HttpPost("Calendars/Create")]
	public async Task<IActionResult> CreateCalendarRecords
		(CreateCalendarRecordRequest request)
	{
		await _recordsService.CreateCalendarRecordAsync(
			request.ServiceId,
			request.Name,
			request.Description
		);

		return Ok();
	}


	[HttpGet("Calendars/Get/All")]
	public async Task<IActionResult> GetAllCalendarRecords
		()
	{
		var calendarsRecordsAsync = await _recordsService
			.GetAllCalendarRecordsAsync();

		return Ok(calendarsRecordsAsync);
	}

	[Authorize]
	[HttpGet("Calendars/Get/{id}")]
	public async Task<IActionResult> GetCalendarRecords(
		Guid id)
	{
		var result = await _recordsService
			.GetCalendarRecordsAsync(id);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
	}

	[Authorize(Roles = "1")]
	[HttpPost("Calendars/Update")]
	public async Task<IActionResult> UpdateCalendarRecords(
		UpdateCalendarRecordRequest request)
	{
		var result =
			await _recordsService.UpdateCalendarRecordAsync(
				request.Id,
				request.Name,
				request.Description
			);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[Authorize(Roles = "1")]
	[HttpDelete("Calendars/Delete/{id}")]
	public async Task<IActionResult> DeleteCalendarRecords
		(Guid id)
	{
		var result =
			await _recordsService.DeleteCalendarRecordsAsync(id);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[Authorize]
	[HttpPost("DayRecords/Fill")]
	public async Task<IActionResult> FillDayRecords(
		FillDayRecordsRequest request)
	{
		await _recordsService.FillDayRecordsAsync(
			request.CalendarId,
			request.StartDate,
			request.EndDate,
			request.StartTime,
			request.EndTime,
			request.BreakStartTime,
			request.BreakEndTime,
			request.Duration,
			request.Offset,
			request.WeekendsDay);

		return Ok();
	}


	[HttpGet(
		"DayRecords/Get/byCalendarId/{id}&{month}&{year}")]
	public async Task<IActionResult>
		GetAllDayRecordsByCalendarId(Guid id, int month,
			int year)
	{
		var result = await _recordsService
			.GetDayRecordsByCalendarIdByMonthByYearAsync(id,
				month, year);

		return Ok(result);
	}

	[HttpGet(
		"DayRecords/Get/byCalendarId/{id}")]
	public async Task<IActionResult>
		GetAllDayRecords(Guid id)
	{
		var result = await _recordsService
			.GetDayRecordsByCalendarIdAsync(id);

		if (result.Count < 1)
			return Ok(null);
		return Ok(result);
	}

	[Authorize]
	[HttpGet("TimeRecords/Get/ByRecordId/{id}")]
	public async Task<IActionResult> GetTimeRecordsByRecordId(
		Guid id)
	{
		var result = await _recordsService
			.GetTimeRecordsByRecordIdAsync(id);

		return Ok(result);
	}

	[Authorize]
	[HttpGet("TimeRecords/Update")]
	public async Task<IActionResult> UpdateTimeRecords(
		Guid id, bool isBusy, string? phone, string? name)
	{
		var result = await _recordsService
			.UpdateTimeRecordAsync(id, isBusy, phone, name);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await _hubContext.Clients.All.SendCoreAsync(
			"UpdateTimeRecord",
			[result.Value]);

		return Ok(result.Value);
	}

	[HttpGet("filllllll")]
	public async Task FillRecord()
	{
		var numbers = GeneratePhoneNumbers(100);

		string[] firstNames =
		{
			"Ярослав", "Евгений", "Дмитрий", "Александр",
			"Максим",
			"Андрей", "Артём", "Илья", "Михаил", "Никита",
			"Алексей", "Роман", "Данил", "Егор", "Арсений",
			"Владимир", "Матвей", "Тимофей", "Кирилл", "Глеб"
		};

		var random = new Random();

		for (int i = 0; i < 5000; i++)
		{
			await _recordsService.CreateWithoutAuthUser
			(numbers[random.Next(0, 100)],
				firstNames[random.Next(0, 20)], null,
				"Тут будет большое описание. " +
				"Тут будет большое описание. " +
				"Тут будет большое описание. " +
				"Тут будет большое описание. " +
				"Тут будет большое описание", null);
		}
	}

	private static List<string> GeneratePhoneNumbers(
		int count)
	{
		var phoneNumbers = new List<string>();
		var random = new Random();

		for (int i = 0; i < count; i++)
		{
			string phoneNumber = GeneratePhoneNumber(random);
			phoneNumbers.Add(phoneNumber);
		}

		return phoneNumbers;
	}

	private static string GeneratePhoneNumber(Random random)
	{
		var number = "8900"; // Начало номера

		// Генерация оставшихся 7 цифр
		for (int i = 0; i < 7; i++)
		{
			number += random.Next(0, 10).ToString();
		}

		return number;
	}
}