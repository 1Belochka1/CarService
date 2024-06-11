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
	private readonly IHubContext<NotifyHub> _hubNotifyContext;

	public RecordsController(
		RecordsService recordsService,
		IHubContext<NotifyHub>
			hubNotifyContext)
	{
		_recordsService = recordsService;
		_hubNotifyContext = hubNotifyContext;
	}

	[HttpPost("Create/WithoutUserAuth")]
	public async Task<IActionResult>
		CreateRecordWithoutAuthUser(
			CreateRequestWithoutAuthUserRequest request)
	{
		var result =
			await _recordsService.CreateWithoutAuthUser(
				request.Email,
				request.Phone,
				request.FirstName,
				request.CarInfo,
				request.Description,
				request.DayRecordsId
			);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await _hubNotifyContext.Clients.Group("admin")
			.SendAsync("NewRequest", "Пришла новая заявка");

		return Ok(result.Value);
	}

	[HttpPost("Create")]
	public async Task<IActionResult> CreateRecord(
		CreateRecordRequest request)
	{
		var result =
			await _recordsService.CreateRequestAsync(
				request.Email,
				request.CarInfo,
				request.Description, null
			);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await _hubNotifyContext.Clients.Group("admin")
			.SendAsync("NewRequest", "Пришла новая заявка");

		return Ok(result.Value);
	}

	[HttpPost("Update")]
	public async Task<IActionResult> Update(
		UpdateRequestRequest request)
	{
		await _recordsService.UpdateRequestAsync(
			request.Id,
			request.Phone,
			request.Description,
			request.Priority,
			request.Status
		);

		return Ok();
	}

	[HttpPost("Update/AddMaster/{id}")]
	public async Task<IActionResult> AddMasters(
		Guid id,
		List<Guid> mastersIds)
	{
		await _recordsService.AddMastersAsync(
			id, mastersIds
		);

		foreach (var mastersId in mastersIds)
		{
			await _hubNotifyContext.Clients.User(mastersId.ToString())
				.SendCoreAsync("NewRequestForYou", [id]);
		}

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
				.GetCompletedRequestByMasterIdAsync(id);

		return Ok(records);
	}

	[Authorize(Roles = "1,2")]
	[HttpGet("Get/ActiveByMasterId/{id}")]
	public async Task<IActionResult> GetActiveByMasterId(
		Guid id)
	{
		var records =
			await _recordsService.GetActiveRequestByMasterIdAsync(
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
			.GetAllRequestAsync(roleId!, Guid.Parse(userId!));

		return Ok(records);
	}

	[Authorize]
	[HttpDelete("Delete/{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await _recordsService.DeleteRequestAsync(id);

		return Ok();
	}

	[Authorize(Roles = "1")]
	[HttpPost("Calendars/Create")]
	public async Task<IActionResult> CreateRecords(
		CreateRequestRequest request)
	{
		await _recordsService.CreateRecordAsync(
			request.ServiceId,
			request.Name,
			request.Description
		);

		return Ok();
	}

	[HttpGet("Calendars/Get/All")]
	public async Task<IActionResult> GetAllRecords
		()
	{
		var calendarsRecordsAsync = await _recordsService
			.GetAllRecordsAsync();

		return Ok(calendarsRecordsAsync);
	}

	[Authorize]
	[HttpGet("Calendars/Get/{id}")]
	public async Task<IActionResult> GetRecords(
		Guid id)
	{
		var result = await _recordsService
			.GetRecordsAsync(id);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok(result.Value);
	}

	[Authorize(Roles = "1")]
	[HttpPost("Calendars/Update")]
	public async Task<IActionResult> UpdateRecords(
		UpdateRecordRequest request)
	{
		var result =
			await _recordsService.UpdateRecordAsync(
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
	public async Task<IActionResult> DeleteRecords(Guid id)
	{
		var result =
			await _recordsService.DeleteRecordsAsync(id);

		if (result.IsFailure)
			return BadRequest(result.Error);

		return Ok();
	}

	[Authorize]
	[HttpPost("DayRecords/Fill")]
	public async Task<IActionResult> FillDayRecords(
		FillDayRecordsRequest request)
	{
		var notNullStart = TimeOnly.TryParse(
			request.BreakStartTime,
			out var breakStartTime);

		var notNullEnd = TimeOnly.TryParse(request.BreakEndTime,
			out var breakEndTime);

		await _recordsService.FillDayRecordsAsync(
			Guid.Parse(request.CalendarId),
			request.StartDate,
			request.EndDate,
			TimeOnly.Parse(request.StartTime),
			TimeOnly.Parse(request.EndTime),
			notNullStart ? breakStartTime : null,
			notNullEnd ? breakEndTime : null,
			request.Duration,
			request.Offset);

		return Ok();
	}

	[HttpGet(
		"DayRecords/Get/byCalendarId/{id}&{month}&{year}")]
	public async Task<IActionResult>
		GetAllDayRecordsByCalendarId(
			Guid id,
			int month,
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

	[HttpGet(
		"DayRecords/Get/{id}")]
	public async Task<IActionResult>
		GetById(Guid id)
	{
		var result = await _recordsService
			.GetByIdAsync(id);

		return Ok(result);
	}
	
	[HttpGet(
		"DayRecords/Delete/{id}")]
	public async Task<IActionResult>
		DeleteDayRecord(Guid id)
	{
		await _recordsService
			.DeleteDayRecord(id);

		return Ok();
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
		Guid id,
		bool isBusy,
		string? email,
		string? phone,
		string? name)
	{
		var result = await _recordsService
			.UpdateTimeRecordAsync(id, isBusy, email, phone,
				name);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await _hubNotifyContext.Clients.All.SendCoreAsync(
			"UpdateTimeRecord",
			[result.Value]);

		return Ok(result.Value);
	}

	[Authorize]
	[HttpDelete("TimeRecords/Delete/{id}")]
	public async Task<IActionResult> DeleteTimeRecords(
		Guid id)
	{
		var result = await _recordsService
			.DeleteTimeRecordAsync(id);

		if (result.IsFailure)
			return BadRequest(result.Error);

		await _hubNotifyContext.Clients.All.SendCoreAsync(
			"DeleteTimeRecord",
			[id]);

		return Ok();
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

		for (int i = 0; i < 1000; i++)
		{
			await _recordsService.CreateWithoutAuthUser
			($"beld{random.Next(100)}@gmail.com",
				numbers[random.Next(0, 100)],
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
		var number = "8990"; // Начало номера

		// Генерация оставшихся 7 цифр
		for (int i = 0; i < 7; i++)
		{
			number += random.Next(0, 10).ToString();
		}

		return number;
	}
}