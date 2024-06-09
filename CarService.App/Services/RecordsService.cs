using CarService.App.Common.Records;
using CarService.App.Interfaces.CalendarsHelper;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Requests;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class RecordsService
{
	private readonly IRequestRepository _requestRepository;
	private readonly IUserInfoRepository _userInfoRepository;
	private readonly IUserAuthRepository _userAuthRepository;

	private readonly IRecordsRepository
		_RecordsRepository;

	private readonly IDayRecordsRepository
		_dayRecordsRepository;

	private readonly ITimeRecordsRepository
		_timeRecordsRepository;

	private readonly ICalendarHelper _calendarHelper;

	public RecordsService(
		IRequestRepository requestRepository,
		IUserInfoRepository userInfoRepository,
		IUserAuthRepository userAuthRepository,
		IRecordsRepository RecordsRepository,
		ICalendarHelper calendarHelper,
		IDayRecordsRepository dayRecordsRepository,
		ITimeRecordsRepository timeRecordsRepository)
	{
		_requestRepository = requestRepository;
		_userInfoRepository = userInfoRepository;
		_userAuthRepository = userAuthRepository;
		_RecordsRepository = RecordsRepository;
		_calendarHelper = calendarHelper;
		_dayRecordsRepository = dayRecordsRepository;
		_timeRecordsRepository = timeRecordsRepository;
	}

	public async Task<Result<Guid>> CreateWithoutAuthUser(
		string email,
		string phone,
		string firstName,
		string? carInfo,
		string? description,
		Guid? timeRecordsId
	)
	{
		var user = await _userInfoRepository.GetByPhone(phone);

		if (user == null)
		{
			var newUser = UserInfo.Create(
				Guid.NewGuid(),
				email,
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

		var record = Request.Create(Guid.NewGuid(), user.Id,
			carInfo,
			description, DateTime.UtcNow, null, timeRecordsId);

		var recordId =
			await _requestRepository.CreateAsync(
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

		var record = Request.Create(id, clientId, carInfo,
			description, DateTime.UtcNow, null, dayRecordsId);

		if (record.IsFailure)
			return Result.Failure<Guid>(record.Error);

		var recordId = await _requestRepository.CreateAsync(
			record.Value);

		return Result.Success(recordId);
	}

	public async Task<Result<RequestsDto>> GetRecordByIdAsync(
		Guid id)
	{
		var record = await _requestRepository.GetByIdAsync(id);

		if (record == null)
			return
				Result.Failure<RequestsDto>("Запись не найдена");

		return Result.Success(record);
	}

	public async Task<List<Request>>
		GetAllRecordsAsync(string roleId, Guid? userId)
	{
		return await _requestRepository.GetAllAsync(roleId,
			userId);
	}

	public async Task<List<Request>>
		GetActiveRecordsByMasterIdAsync(
			Guid masterId)
	{
		return await
			_requestRepository.GetActiveByMasterIdAsync(masterId);
	}

	public async Task<List<Request>>
		GetCompletedRecordsByMasterIdAsync(
			Guid masterId)
	{
		return await _requestRepository
			.GetCompletedByMasterIdAsync(masterId);
	}

	public async Task UpdateRecordAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RequestPriority? priority = null,
		RequestStatus? status = null)
	{
		await _requestRepository.UpdateAsync(id, phone,
			description,
			priority, status);
	}

	public async Task AddMastersAsync(
		Guid recordId,
		List<Guid> masterIds)
	{
		var masters =
			await _userAuthRepository.GetWorkersByIds(masterIds);

		await _requestRepository.AddMasters(recordId, masters);
	}

	// Record records

	public async Task CreateRecordAsync(
		Guid serviceId,
		string name,
		string? description)
	{
		var calendar = Record.Create(Guid.NewGuid(),
			name, description, serviceId);

		await _RecordsRepository.Create(calendar.Value);
	}

	public async Task<List<Record>>
		GetAllRecordsAsync()
	{
		return await _RecordsRepository.GetAll();
	}

	public async Task<Result<Record>>
		GetRecordsAsync(Guid id)
	{
		var calendar =
			await _RecordsRepository.GetById(id);

		if (calendar == null)
			return Result.Failure<Record>(
				"Календарь не найден");

		return Result.Success(calendar);
	}

	public async Task<Result> UpdateRecordAsync(
		Guid
			requestId,
		string? requestName,
		string? requestDescription)
	{
		var calendar =
			await _RecordsRepository.GetById(requestId);

		if (calendar == null)
			return Result.Failure("Календарь не найден");

		calendar.Update(requestName, requestDescription);

		await _RecordsRepository.Update(calendar);

		return Result.Success();
	}

	public async Task<Result> DeleteRecordsAsync(
		Guid id)
	{
		var calendar =
			await _RecordsRepository.GetById(id);

		if (calendar == null)
			return Result.Failure("Календарь не найден");

		await _RecordsRepository.Delete(id);

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
			Guid calendarId,
			int?
				month = null,
			int? year = null)
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
		UpdateTimeRecordAsync(Guid id, bool isBusy, string? email, string? phone, string? name)
	{
		var timeRecord = await _timeRecordsRepository
			.GetById(id);

		if (timeRecord == null)
			return
				Result.Failure<TimeRecord>("Запись не найдена");

		UserInfo? user = null;

		if (phone != null && name != null && email != null)
		{
			user =
				await _userInfoRepository.GetByPhone(phone);

			if (user == null)
			{
				var newUser = UserInfo.Create(
					Guid.NewGuid(),
					email,
					null,
					name,
					null,
					null,
					phone
				);

				if (newUser.IsFailure)
					return Result.Failure<TimeRecord>(newUser.Error);

				await _userInfoRepository
					.CreateAsync(newUser.Value);

				user = newUser.Value;
			}
		}

		timeRecord.Update(isBusy, user?.Id);

		await _timeRecordsRepository.Update(timeRecord);

		return Result.Success(timeRecord);
	}
}