using CarService.Core.Services;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Records;

public class Record
{
	public Record()
	{
	}

	private Record(
		Guid id,
		Guid? clientId,
		string? carInfo,
		string description,
		DateTime createTime,
		DateTime? visitTime,
		Guid? timeRecordId
	)
	{
		Id = id;
		ClientId = clientId;
		CarInfo = carInfo;
		Description = description;
		CreateTime = createTime;
		VisitTime = visitTime;
		TimeRecordId = timeRecordId;
	}

	public Guid Id { get; private set; }

	public Guid? ClientId { get; private set; }

	public UserInfo? Client { get; private set; } = null!;

	public string? CarInfo { get; private set; }
	public string Description { get; private set; }

	public DateTime CreateTime { get; private set; }

	public DateTime? VisitTime { get; private set; }

	public bool IsTransferred { get; private set; } = false;

	public DateTime? CompleteTime { get; private set; }

	public RecordPriority Priority { get; private set; } =
		RecordPriority.Normal;

	public RecordStatus Status { get; private set; } =
		RecordStatus.New;

	public Guid? TimeRecordId { get; private set; }

	public TimeRecord? TimeRecord { get; private set; }

	public virtual ICollection<Service> Services
	{
		get;
		private set;
	} = [];

	public virtual ICollection<UserAuth> Masters
	{
		get;
		private set;
	} = [];

	public void SetClient(UserInfo client)
	{
		Client = client;
		ClientId = client.Id;
	}

	public void SetDescription(string description)
	{
		Description = description;
	}

	public void SetPriority(RecordPriority priority)
	{
		Priority = priority;
	}

	public void SetStatus(RecordStatus status)
	{
		Status = status;
	}

	public void SetMasters(ICollection<UserAuth> masters)
	{
		Masters = masters;
	}

	public void AddMasters(ICollection<UserAuth> masters)
	{
		foreach (var master in masters) Masters.Add(master);
	}

	public void SetTimeComplete(DateTime completeTime)
	{
		CompleteTime = completeTime;
	}

	public void SetTimeVisit(DateTime visitTime)
	{
		VisitTime = visitTime;
	}

	public void UpdateTimeVisit(DateTime visitTime)
	{
		IsTransferred = true;
		VisitTime = visitTime;
	}

	public static Result<Record> Create(
		Guid id,
		Guid? userId,
		string? carInfo,
		string description,
		DateTime createTime,
		DateTime? visitTime,
		Guid? dayRecordsId)
	{
		if (id == Guid.Empty)
			return Result.Failure<Record>("Id can't be empty");

		if (string.IsNullOrEmpty(description))
			return Result.Failure<Record>(
				"Описание заявки не может быть пустым");

		if (createTime == DateTime.MinValue)
			return Result.Failure<Record>(
				"Дата создания заявки не может быть пустой");

		if (visitTime == DateTime.MinValue)
			return Result.Failure<Record>(
				"Дата приема не может быть пустой");

		var record = new Record(
			id,
			userId,
			carInfo,
			description,
			createTime,
			visitTime,
			dayRecordsId
		);

		return Result.Success(record);
	}
}