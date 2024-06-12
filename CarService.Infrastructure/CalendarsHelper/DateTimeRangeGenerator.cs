using CarService.App.Interfaces.CalendarsHelper;

namespace CarService.Infrastructure.CalendarsHelper;

// Класс DateTimeRangeGenerator реализует интерфейс IDateTimeRangeGenerator и 
// предназначен для генерации списка дат или времени в заданном диапазоне.
public class DateTimeRangeGenerator : IDateTimeRangeGenerator
{
	// Метод GenerateDays генерирует список дат от start до end включительно.
	public List<DateTime> GenerateDays(DateTime start, DateTime end)
	{
		// Создаем пустой список для хранения дат.
		var days = new List<DateTime>();

		// Цикл for, который начинается с начальной даты (start) и заканчивается конечной датой (end).
		// На каждой итерации добавляется один день к текущей дате.
		for (var date = start; date <= end; date = date.AddDays(1))
		{
			// Добавляем текущую дату в список.
			days.Add(date);
		}

		// Возвращаем заполненный список дат.
		return days;
	}

	// Метод GenerateTimes генерирует список времени от start до end включительно,
	// с шагом в один час.
	public List<TimeOnly> GenerateTimes(TimeOnly start, TimeOnly end)
	{
		// Создаем пустой список для хранения времени.
		var times = new List<TimeOnly>();

		// Цикл for, который начинается с начального времени (start) и заканчивается конечным временем (end).
		// На каждой итерации добавляется один час к текущему времени.
		for (var time = start; time <= end; time = time.AddHours(1))
		{
			// Добавляем текущее время в список.
			times.Add(time);
		}

		// Возвращаем заполненный список времени.
		return times;
	}
}