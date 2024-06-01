import {Component} from '@angular/core'
import {ActivatedRoute, RouterLink} from '@angular/router'
import {CalendarService} from '../../../../../services/calendars/calendar.service'
import {Observable} from 'rxjs'
import {AsyncPipe, DatePipe, NgForOf} from '@angular/common'
import {DayRecord} from '../../../../../models/DayRecord.type'

@Component({
	selector: 'app-calendar-page',
	standalone: true,
	imports: [
		NgForOf,
		AsyncPipe,
		DatePipe,
		RouterLink
	],
	templateUrl: './calendar-page.component.html',
	styleUrl: './calendar-page.component.scss',
	providers: [CalendarService]
})
export class CalendarPageComponent {

	dayRecords: Observable<DayRecord[]>

	constructor(
		private _calendarService: CalendarService,
		private _router: ActivatedRoute,
	) {
		const id = this._router.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		// _calendarService.getDayRecordsByCalendarId(id)
	}
}
