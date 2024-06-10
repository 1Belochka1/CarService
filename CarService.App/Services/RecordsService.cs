using CarService.App.Common.Email;
using CarService.App.Common.Requests;
using CarService.App.Interfaces.CalendarsHelper;
using CarService.App.Interfaces.Email;
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
	private readonly IRecordsRepository _recordsRepository;

	private readonly IDayRecordsRepository
		_dayRecordsRepository;

	private readonly ITimeRecordsRepository
		_timeRecordsRepository;

	private readonly ICalendarHelper _calendarHelper;
	private readonly IEmailService _emailService;

	public RecordsService(
		IRequestRepository requestRepository,
		IUserInfoRepository userInfoRepository,
		IUserAuthRepository userAuthRepository,
		IRecordsRepository recordsRepository,
		ICalendarHelper calendarHelper,
		IDayRecordsRepository dayRecordsRepository,
		ITimeRecordsRepository timeRecordsRepository,
		IEmailService emailService)
	{
		_requestRepository = requestRepository;
		_userInfoRepository = userInfoRepository;
		_userAuthRepository = userAuthRepository;
		_recordsRepository = recordsRepository;
		_calendarHelper = calendarHelper;
		_dayRecordsRepository = dayRecordsRepository;
		_timeRecordsRepository = timeRecordsRepository;
		_emailService = emailService;
	}

	public async Task<Result<Guid>> CreateWithoutAuthUser(
		string email,
		string? phone,
		string? firstName,
		string? carInfo,
		string description,
		Guid? timeRecordsId
	)
	{
		var user = await _userInfoRepository.GetByEmail(email);

		if (user == null)
		{
			if (phone == null || firstName == null)
			{
				return Result.Failure<Guid>(
					"Пользователь не найден");
			}

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

		var request = Request.Create(Guid.NewGuid(), user.Id,
			carInfo,
			description, DateTime.UtcNow, null, timeRecordsId);

		var recordId =
			await _requestRepository.CreateAsync(
				request.Value);

		// await _emailService.SendEmail(new EmailDto()
		// {
		// 	To = user.Email,
		// 	Subject = "Заявка на ремонт",
		// 	Body = $"Здравствуйте, {user.FirstName}. " +
		// 	       $"Ваша заявка принята. " +
		// 	       $"В ближайшее время с вами свяжется наш администратор."
		// });

		return Result.Success(recordId);
	}

	public async Task<Result<Guid>> CreateRequestAsync(
		string email,
		string? carInfo,
		string description,
		Guid? dayRecordsId)
	{
		var user =
			await _userInfoRepository.GetByEmail(email);

		if (user == null)
			return Result.Failure<Guid>("Пользователь не найден");

		var clientId = user.Id;

		var id = Guid.NewGuid();

		var record = Request.Create(id, clientId, carInfo,
			description, DateTime.UtcNow, null, dayRecordsId);

		if (record.IsFailure)
			return Result.Failure<Guid>(record.Error);

		var recordId = await _requestRepository.CreateAsync(
			record.Value);

		// await _emailService.SendEmail(new EmailDto()
		// {
		// 	To = user.Email,
		// 	Subject = "Заявка на ремонт",
		// 	Body = $"Здравствуйте, {user.FirstName}. " +
		// 	       $"Ваша заявка принята. " +
		// 	       $"В ближайшее время с вами свяжется наш администратор."
		// });

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
		GetAllRequestAsync(string roleId, Guid? userId)
	{
		return await _requestRepository.GetAllAsync(roleId,
			userId);
	}

	public async Task<List<Request>>
		GetActiveRequestByMasterIdAsync(
			Guid masterId)
	{
		return await
			_requestRepository.GetActiveByMasterIdAsync(masterId);
	}

	public async Task<List<Request>>
		GetCompletedRequestByMasterIdAsync(
			Guid masterId)
	{
		return await _requestRepository
			.GetCompletedByMasterIdAsync(masterId);
	}

	public async Task UpdateRequestAsync(
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

	public async Task DeleteMastersAsync(
		Guid recordId,
		List<Guid> masterIds)
	{
		var masters =
			await _userAuthRepository.GetWorkersByIds(masterIds);

		await _requestRepository.AddMasters(recordId, masters);
	}


	public async Task DeleteRequestAsync(Guid id)
	{
		await _requestRepository.DeleteAsync(id);
	}

	// Record records

	public async Task CreateRecordAsync(
		Guid serviceId,
		string name,
		string? description)
	{
		var calendar = Record.Create(Guid.NewGuid(),
			name, description, serviceId);

		await _recordsRepository.Create(calendar.Value);
	}

	public async Task<List<Record>>
		GetAllRecordsAsync()
	{
		return await _recordsRepository.GetAll();
	}

	public async Task<Result<Record>>
		GetRecordsAsync(Guid id)
	{
		var calendar =
			await _recordsRepository.GetById(id);

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
			await _recordsRepository.GetById(requestId);

		if (calendar == null)
			return Result.Failure("Расписание не найдено");

		calendar.Update(requestName, requestDescription);

		await _recordsRepository.Update(calendar);

		return Result.Success();
	}

	public async Task<Result> DeleteRecordsAsync(
		Guid id)
	{
		var calendar =
			await _recordsRepository.GetById(id);

		if (calendar == null)
			return Result.Failure("Расписание не найдено");

		await _recordsRepository.Delete(id);

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
		int offset)
	{
		var startDateUTC = startDate.ToUniversalTime();
		var endDateUTC = endDate.ToUniversalTime();
		var daysDateTime =
			_calendarHelper.GenerateDays(startDateUTC,
				endDateUTC);
		var daysRecords = new List<DayRecord>();

		foreach (var day in daysDateTime)
		{
			var isWeekend = false;
			// weekendsDay.Any(x => x.Date == day.Date);

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
		UpdateTimeRecordAsync(Guid id, bool isBusy,
			string? email, string? phone, string? name)
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

	public async Task<Result> DeleteTimeRecordAsync(
		Guid id)
	{
		var timeRecord = await _timeRecordsRepository
			.GetById(id);

		if (timeRecord == null)
			return Result.Failure("Запись не найдена");

		await _timeRecordsRepository.Delete(timeRecord.Id);

		return Result.Success();
	}
}