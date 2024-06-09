namespace CarService.Core.Requests;

public class DayRecord
{
	public DayRecord()
	{
	}

	private DayRecord(
		Guid id,
		Guid calendarId,
		DateTime date,
		TimeOnly startTime,
		TimeOnly endTime,
		short offset,
		bool isWeekend)
	{
		Id = id;
		CalendarId = calendarId;
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		Offset = offset;
		IsWeekend = isWeekend;
	}

	public Guid Id { get; private set; }

	public Guid CalendarId { get; private set; }

	public DateTime Date { get; private set; }

	public TimeOnly StartTime { get; private set; }

	public TimeOnly EndTime { get; private set; }

	public short Offset { get; private set; }

	public bool IsWeekend { get; private set; }

	public Record? Calendar { get; private set; }

	public List<TimeRecord> TimeRecords { get; private set; }

	public static DayRecord Create(
		Guid id,
		Guid calendarId,
		DateTime date,
		TimeOnly startTime,
		TimeOnly endTime,
		short offset, bool isWeekend = false)
	{
		return new DayRecord(id, calendarId, date, startTime,
			endTime, offset, isWeekend);
	}
}