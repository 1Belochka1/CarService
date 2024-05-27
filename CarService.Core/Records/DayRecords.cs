﻿namespace CarService.Core.Records;

public class DayRecords
{
	private DayRecords(
		Guid id,
		Guid calendarId,
		TimeOnly startTime,
		TimeOnly endTime,
		short offset,
		short duration)
	{
		Id = id;
		CalendarId = calendarId;
		StartTime = startTime;
		EndTime = endTime;
		Offset = offset;
		Duration = duration;
	}

	public Guid Id { get; private set; }

	public Guid CalendarId { get; private set; }

	public TimeOnly StartTime { get; private set; }

	public TimeOnly EndTime { get; private set; }

	public short Offset { get; private set; }

	public short Duration { get; private set; }

	public CalendarRecords? Calendar { get; private set; }


	public static DayRecords Create(
		Guid id,
		Guid calendarId,
		TimeOnly startTime,
		TimeOnly endTime,
		short offset,
		short duration)
	{
		return new DayRecords(id, calendarId, startTime,
			endTime, offset, duration);
	}
}