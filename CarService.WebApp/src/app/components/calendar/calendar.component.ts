import {ChangeDetectorRef, Component} from '@angular/core'
import {
	CalendarService,
	Day
} from '../../services/calendars/calendar.service'
import {DatePipe, NgClass, NgForOf} from '@angular/common'

@Component({
  selector: 'app-calendar',
  standalone: true,
	imports: [
		NgClass,
		NgForOf,
		DatePipe
	],
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent {
	currentDate: Date

	daysView: Day[][]

	constructor(public _calendarService: CalendarService, public _cdr: ChangeDetectorRef) {
		_calendarService.getViewMonth().subscribe((month) => {
			this.daysView = month
			this._cdr.markForCheck()
		})

		_calendarService.getCurrentDate().subscribe((d) => {
			this.currentDate = d
			console.log(this.currentDate)
			this._cdr.markForCheck()
		})
	}

	ngOnInit(): void {
	}

	nextMonth(): void {
		this._calendarService.nextMonth()
		this._cdr.detectChanges()
	}

	previousMonth(): void {
		this._calendarService.prevMonth()
		this._cdr.detectChanges()
	}

}
