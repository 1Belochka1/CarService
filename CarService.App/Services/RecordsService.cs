using CarService.App.Common.DayRecordsWithWeeks;
using CarService.App.Common.ListWithPage;
using CarService.App.Interfaces.CalendarsHelper;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class RecordsService
{
	private readonly IRecordsRepository _recordsRepository;
	private readonly IUserInfoRepository _userInfoRepository;
	private readonly IUserAuthRepository _userAuthRepository;

	private readonly ICalendarRecordsRepository
		_calendarRecordsRepository;

	private readonly IDayRecordsRepository
		_dayRecordsRepository;

	private readonly ITimeRecordsRepository
		_timeRecordsRepository;


	private readonly ICalendarHelper _calendarHelper;

	public RecordsService(
		IRecordsRepository recordsRepository,
		IUserInfoRepository userInfoRepository,
		IUserAuthRepository userAuthRepository,
		ICalendarRecordsRepository calendarRecordsRepository,
		ICalendarHelper calendarHelper,
		IDayRecordsRepository dayRecordsRepository,
		ITimeRecordsRepository timeRecordsRepository)
	{
		_recordsRepository = recordsRepository;
		_userInfoRepository = userInfoRepository;
		_userAuthRepository = userAuthRepository;
		_calendarRecordsRepository = calendarRecordsRepository;
		_calendarHelper = calendarHelper;
		_dayRecordsRepository = dayRecordsRepository;
		_timeRecordsRepository = timeRecordsRepository;
	}

	public async Task<Result<Guid>> CreateWithoutAuthUser(
		string phone,
		string firstName,
		string? carInfo,
		string description,
		Guid? dayRecordsId
	)
	{
		var user = await _userInfoRepository.GetByPhone(phone);

		if (user == null)
		{
			var newUser = UserInfo.Create(
				Guid.NewGuid(),
				null,
				firstName,
				null,
				null,
				phone
			);

			if (newUser.IsFailure)
				return Result.Failure<Guid>(newUser.Error);

			await _userInfoRepository.CreateAsync(newUser.Value);

			user = newUser.Value;
		}

		var record = Record.Create(Guid.NewGuid(), user.Id,
			carInfo,
			description, DateTime.UtcNow, null, dayRecordsId);

		var recordId =
			await _recordsRepository.CreateAsync(
				record.Value);

		return Result.Success(recordId);
	}

	public async Task<Result<Guid>> CreateRecordAsync(
		Guid clientId,
		string? carInfo,
		string description,
		Guid? dayRecordsId)
	{
		var id = Guid.NewGuid();

		var record = Record.Create(id, clientId, carInfo,
			description, DateTime.UtcNow, null, dayRecordsId);

		if (record.IsFailure)
			return Result.Failure<Guid>(record.Error);

		var recordId = await _recordsRepository.CreateAsync(
			record.Value);

		return Result.Success(recordId);
	}

	public async Task<Result<Record>> GetRecordByIdAsync(
		Guid id)
	{
		var record = await _recordsRepository.GetByIdAsync(id);

		if (record == null)
			return Result.Failure<Record>("Запись не найдена");

		return Result.Success(record);
	}

	public async Task<ListWithPage<Record>>
		GetAllRecordsAsync(ParamsWhitFilter parameters)
	{
		return await _recordsRepository.GetAllAsync(parameters);
	}

	public async Task<ListWithPage<Record>>
		GetActiveRecordsByMasterIdAsync(
			Guid masterId,
			Params parameters)
	{
		return await
			_recordsRepository.GetActiveByMasterIdAsync(masterId,
				parameters);
	}

	public async Task<ListWithPage<Record>>
		GetCompletedRecordsByMasterIdAsync(
			Guid masterId,
			Params parameters)
	{
		return await _recordsRepository
			.GetCompletedByMasterIdAsync(masterId, parameters);
	}

	public async Task UpdateRecordAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RecordPriority? priority = null,
		RecordStatus? status = null)
	{
		await _recordsRepository.UpdateAsync(id, phone,
			description,
			priority, status);
	}

	public async Task AddMastersAsync(
		Guid recordId,
		List<Guid> masterIds)
	{
		var masters =
			await _userAuthRepository.GetWorkersByIds(masterIds);

		await _recordsRepository.AddMasters(recordId, masters);
	}

	// Calendar records

	public async Task CreateCalendarRecordAsync(
		Guid serviceId,
		string name, string? description)
	{
		var calendar = CalendarRecord.Create(Guid.NewGuid(),
			name, description, serviceId);

		await _calendarRecordsRepository.Create(calendar.Value);
	}

	public async Task<List<CalendarRecord>>
		GetAllCalendarRecordsAsync()
	{
		return await _calendarRecordsRepository.GetAll();
	}

	public async Task<Result<CalendarRecord>>
		GetCalendarRecordsAsync(Guid id)
	{
		var calendar =
			await _calendarRecordsRepository.GetById(id);

		if (calendar == null)
			return Result.Failure<CalendarRecord>(
				"Календарь не найден");

		return Result.Success(calendar);
	}

	public async Task<Result> UpdateCalendarRecordAsync(Guid
			requestId, string? requestName,
		string? requestDescription)
	{
		var calendar =
			await _calendarRecordsRepository.GetById(requestId);

		if (calendar == null)
			return Result.Failure("Календарь не найден");

		calendar.Update(requestName, requestDescription);

		await _calendarRecordsRepository.Update(calendar);

		return Result.Success();
	}

	public async Task<Result> DeleteCalendarRecordsAsync(
		Guid id)
	{
		var calendar =
			await _calendarRecordsRepository.GetById(id);

		if (calendar == null)
			return Result.Failure("Календарь не найден");

		await _calendarRecordsRepository.Delete(id);

		return Result.Success();
	}

	public async Task FillDayRecordsAsync(
		Guid calendarId,
		DateTime startDate,
		DateTime endDate,
		TimeOnly startTime,
		TimeOnly endTime,
		TimeOnly? breakStartTime,
		TimeOnly? breakEndTime,
		int duration,
		int offset,
		List<DateTime> weekendsDay)
	{
		var daysDateTime =
			_calendarHelper.GenerateDays(startDate, endDate);
		var daysRecords = new List<DayRecord>();

		foreach (var day in daysDateTime)
		{
			var isWeekend =
				weekendsDay.Any(x => x.Date == day.Date);

			var dayRecord = DayRecord.Create(
				Guid.NewGuid(),
				calendarId,
				day,
				startTime,
				endTime,
				0,
				isWeekend);

			daysRecords.Add(dayRecord);
		}

		await _dayRecordsRepository.CreateRangeAsync(
			daysRecords);

		var timesRecords = new List<TimeRecord>();
		foreach (var dayRecord in daysRecords)
		{
			if (!dayRecord.IsWeekend)
			{
				var currentTime = dayRecord.StartTime;
				while (currentTime.AddMinutes(duration) <=
				       dayRecord.EndTime)
				{
					// Проверяем, не попадает ли текущее время в перерыв
					if (!(currentTime >= breakStartTime &&
					      currentTime < breakEndTime))
					{
						var timeRecord = TimeRecord.Create(
							Guid.NewGuid(),
							dayRecord.Id,
							null,
							null,
							currentTime,
							currentTime.AddMinutes(duration),
							false);

						timesRecords.Add(timeRecord.Value);
					}

					currentTime =
						currentTime.AddMinutes(duration + offset);
				}
			}
		}

		await _timeRecordsRepository.CreateRangeAsync(
			timesRecords);
	}

	public async Task<List<DayRecord>>
		GetDayRecordsByCalendarIdByMonthByYearAsync(
			Guid calendarId, int?
				month = null, int? year = null)
	{
		return await _dayRecordsRepository
			.GetByCalendarIdByMonthByYearId
				(calendarId, month, year);
	}

	public async Task<List<DayRecord>>
		GetDayRecordsByCalendarIdAsync(Guid calendarId)
	{
		return await _dayRecordsRepository
			.GetByCalendarId
				(calendarId);
	}

	public async Task<List<TimeRecord>>
		GetTimeRecordsByRecordIdAsync(Guid id)
	{
		return await _timeRecordsRepository
			.GetTimeRecordsByRecordIdAsync(id);
	}

	public async Task<Result<TimeRecord>>
		UpdateTimeRecordAsync
		(Guid id, bool isBusy)
	{
		var timeRecord = await _timeRecordsRepository
			.GetById(id);

		if (timeRecord == null)
			return
				Result.Failure<TimeRecord>("Запись не найдена");

		timeRecord.Update(isBusy);

		await _timeRecordsRepository.Update(timeRecord);

		return Result.Success(timeRecord);
	}
}