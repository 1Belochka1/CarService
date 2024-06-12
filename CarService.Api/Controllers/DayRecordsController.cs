using CarService.Api.Contracts.DayRecords;
using CarService.App.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

public class DayRecordsController : ApiController
{
	private readonly IDayRecordsRepository
		_dayRecordsRepository;

	public DayRecordsController(
		IDayRecordsRepository dayRecordsRepository)
	{
		_dayRecordsRepository = dayRecordsRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var dayRecords = await _dayRecordsRepository.GetAll();
		return Ok(dayRecords);
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		CreateDayRecordsRequest
			dayRecords)
	{
		throw new NotImplementedException();
	}
}