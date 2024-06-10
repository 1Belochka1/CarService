using CarService.App.Interfaces.CalendarsHelper;

namespace CarService.Infrastructure.CalendarsHelper;

public class DateTimeRangeGenerator : IDateTimeRangeGenerator
{
	public List<DateTime> GenerateDays(DateTime start,
		DateTime end)
	{
		var days = new List<DateTime>();

		for (var date = start;
		     date <= end;
		     date = date.AddDays(1))
		{
			days.Add(date);
		}

		return days;
	}

	public List<TimeOnly> GenerateTimes(TimeOnly start,
		TimeOnly end)
	{
		var times = new List<TimeOnly>();

		for (var time = start;
		     time <= end;
		     time = time.AddHours(1))
		{
			times.Add(time);
		}

		return times;
	}
}