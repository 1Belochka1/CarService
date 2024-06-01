namespace CarService.App.Interfaces.CalendarsHelper;

public interface ICalendarHelper
{
	List<DateTime> GenerateDays(DateTime start, DateTime end);

	List<TimeOnly> GenerateTimes(TimeOnly start,
		TimeOnly end);
}