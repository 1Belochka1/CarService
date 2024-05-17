using CarService.Core.Services;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Records;

public class Record
{
	private Record(Guid id, Guid clientId, string description, DateTime time)
	{
		Id = id;
		ClientId = clientId;
		Description = description;
		Time = time;
	}

	public Guid Id { get; private set; }

	public Guid ClientId { get; private set; }

	public UserAuth Client { get; private set; } = null!;

	public string Description { get; private set; }

	public DateTime Time { get; private set; }

	public RecordPriority Priority { get; private set; } = RecordPriority.normal;

	public RecordStatus Status { get; private set; } = RecordStatus.New;

	public virtual ICollection<Service> Services { get; private set; } = [];

	public virtual ICollection<UserAuth> Masters { get; private set; } = [];

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

	public static Result<Record> Create(Guid id, Guid userId, string description, DateTime time)
	{
		if (id == Guid.Empty)
			return Result.Failure<Record>("Id can't be empty");

		if (userId == Guid.Empty)
			return Result.Failure<Record>("UserId can't be empty");

		if (string.IsNullOrEmpty(description))
			return Result.Failure<Record>("Description can't be empty");

		var record = new Record(id, userId, description, time);

		return Result.Success(record);
	}
}