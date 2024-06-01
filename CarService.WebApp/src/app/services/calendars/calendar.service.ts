import {Injectable} from '@angular/core'
import {
	BehaviorSubject,
	firstValueFrom,
	Observable,
	Subject
} from 'rxjs'
import {HttpClient} from '@angular/common/http'
import {apiUrls} from '../apiUrl'
import {DayRecord} from '../../models/DayRecord.type'

export type Day = {
	date: Date;
	dayRecord?: DayRecord;
	month: number;
	year: number;
	dayOfWeek: number;
}

@Injectable({
	providedIn: 'root'
})
export class CalendarService {

	private _currentDate: BehaviorSubject<Date> = new BehaviorSubject<Date>(new Date())

	private _viewMonth: Subject<Day[][]> = new Subject<Day[][]>()

	private _calendarId: string

	private _dayRecords: DayRecord[] = []

	constructor(private _http: HttpClient) {
		firstValueFrom(this._http.get(apiUrls.calendars.getAll))
		.then((calendars: any) => {
				this._calendarId = calendars[0].id
				this.fillCurrentMonth()
			}
		)
	}

	public getCurrentDate(): Observable<Date> {
		return this._currentDate.asObservable()
	}

	public getViewMonth(): Observable<Day[][]> {
		return this._viewMonth.asObservable()
	}

	// Метод для заполнения массива днями текущего месяца
	public fillCurrentMonth(): void {
		const date = new Date()
		date.setDate(1)
		this._currentDate.next(date)
		this.fillMonth(this._currentDate.value.getMonth() + 1, this._currentDate.value.getFullYear())
	}

	// Метод для заполнения массива днями заданного месяца и
	// года
	public fillMonth(month: number, year: number): void {
		const daysInMonth = this.getDaysInMonth(month, year)
		const weeks: Day[][] = []
		let currentWeek: Day[] = []

		// Найти первый день месяца и его день недели
		const firstDate = new Date(year, month - 1, 1)
		let firstDayOfWeek = (firstDate.getDay() + 6) % 7 // Преобразуем
																											// день
		// недели (0 -
		// Понедельник, ..., 6 -
		// Воскресенье)

		// Добавляем дни из предыдущего месяца до понедельника
		if (firstDayOfWeek !== 0) {
			const prevMonthDays = this.getDaysInMonth(month - 1, year)
			for (let i = firstDayOfWeek - 1; i >= 0; i--) {
				const date = new Date(year, month - 2, prevMonthDays - i)
				const dayOfWeek = (date.getDay() + 6) % 7
				const dayObject: Day = {
					date: date,
					month: month - 1,
					year: year,
					dayOfWeek: dayOfWeek
				}
				currentWeek.push(dayObject)
			}
		}

		// Добавляем дни текущего месяца
		for (let day = 1; day <= daysInMonth; day++) {
			const date = new Date(year, month - 1, day)
			const dayOfWeek = (date.getDay() + 6) % 7

			const dayObject: Day = {
				date: date,
				month: month,
				year: year,
				dayOfWeek: dayOfWeek
			}
			currentWeek.push(dayObject)

			// Если текущий день воскресенье, то завершаем неделю
			if (dayOfWeek === 6) {
				weeks.push(currentWeek)
				currentWeek = []
			}
		}

		// Добавляем дни из следующего месяца, чтобы завершить
		// последнюю неделю
		const lastDate = new Date(year, month - 1, daysInMonth)
		let lastDayOfWeek = (lastDate.getDay() + 6) % 7
		if (lastDayOfWeek !== 6) {
			for (let i = 1; lastDayOfWeek < 6; i++, lastDayOfWeek++) {
				const date = new Date(year, month, i)
				const dayOfWeek = (date.getDay() + 6) % 7
				const dayObject: Day = {
					date: date,
					month: month + 1,
					year: year,
					dayOfWeek: dayOfWeek
				}
				currentWeek.push(dayObject)
			}
			weeks.push(currentWeek)
		}

		this.getDayRecords(weeks, month, year)
	}

	// Метод для перехода к следующему месяцу
	public nextMonth(): void {
		this._currentDate.value.setMonth(this._currentDate.value.getMonth() + 1)
		this._currentDate.next(this._currentDate.value)
		this.fillMonth(this._currentDate.value.getMonth() + 1, this._currentDate.value.getFullYear())
	}

	// Метод для перехода к предыдущему месяцу
	public prevMonth(): void {
		this._currentDate.value.setMonth(this._currentDate.value.getMonth() - 1)
		this._currentDate.next(this._currentDate.value)
		this.fillMonth(this._currentDate.value.getMonth() + 1, this._currentDate.value.getFullYear())
	}

	private getDayRecords(weeks: Day[][], month: number, year: number) {
		firstValueFrom(this._http.get<DayRecord[]>(apiUrls.dayRecords.getByCalendarId
			+ this._calendarId + '&'
			+ (this._currentDate.value.getMonth() + 1)
			+ '&' + this._currentDate.value.getFullYear(),
			{withCredentials: true}
		)).then(data => {
			for (const week of weeks) {
				for (const day of week) {
					day.dayRecord = data.find(record => this.areSameDates(new Date(record.date), new Date(day.date)))
				}
			}
			this._viewMonth.next(weeks)
		})
	}

	private areSameDates(date1: Date, date2: Date): boolean {
		return (
			date1.getFullYear() === date2.getFullYear() &&
			date1.getMonth() === date2.getMonth() &&
			date1.getDate() === date2.getDate()
		)
	}

	private getDaysInMonth(month: number, year: number): number {
		if (month < 1) {
			month = 12
			year--
		} else if (month > 12) {
			month = 1
			year++
		}
		return new Date(year, month, 0).getDate()
	}
}
