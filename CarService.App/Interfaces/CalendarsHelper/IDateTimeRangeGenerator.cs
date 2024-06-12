namespace CarService.App.Interfaces.CalendarsHelper;

public interface IDateTimeRangeGenerator
{
	List<DateTime> GenerateDays(DateTime start, DateTime end);

	List<TimeOnly> GenerateTimes(TimeOnly start,
		TimeOnly end);
}