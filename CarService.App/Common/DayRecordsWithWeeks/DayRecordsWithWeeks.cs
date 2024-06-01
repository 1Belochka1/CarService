using CarService.Core.Records;

namespace CarService.App.Common.DayRecordsWithWeeks;

public class DayRecordsWithWeeks
{
	public int TotalWeek { get; set; }

	public int CurrentWeek { get; set; }

	public List<DayRecord> Days { get; set; }
}