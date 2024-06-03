using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Records;

public class TimeRecord
{
	private TimeRecord(
		Guid id,
		Guid dayRecordId,
		Guid? recordId,
		Guid? clientId,
		TimeOnly startTime,
		TimeOnly endTime,
		bool isBusy)
	{
		Id = id;
		DayRecordId = dayRecordId;
		RecordId = recordId;
		ClientId = clientId;
		StartTime = startTime;
		EndTime = endTime;
		IsBusy = isBusy;
	}

	public Guid Id { get; private set; }

	public Guid DayRecordId { get; private set; }

	public Guid? RecordId { get; private set; }

	public Guid? ClientId { get; private set; }

	public TimeOnly StartTime { get; private set; }

	public TimeOnly EndTime { get; private set; }

	public bool IsBusy { get; private set; }

	public DayRecord DayRecord { get; private set; }

	public Record? Record { get; private set; }

	public UserInfo? Client { get; private set; }

	public static Result<TimeRecord> Create(
		Guid id,
		Guid dayRecordId,
		Guid? recordId,
		Guid? clientId,
		TimeOnly startTime,
		TimeOnly endTime,
		bool isBusy)
	{
		return Result.Success(new TimeRecord(id, dayRecordId,
			recordId, clientId, startTime, endTime, isBusy));
	}

	public void Update(bool isBusy, Guid? clientId,
		Guid? recordId)
	{
		IsBusy = isBusy;
		ClientId = clientId;
		RecordId = recordId;
	}
}