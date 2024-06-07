import {Component} from '@angular/core'
import {ActivatedRoute, Router, RouterLink} from '@angular/router'
import {
	CalendarService
} from '../../../../../services/calendars/calendar.service'
import {Observable} from 'rxjs'
import {AsyncPipe, DatePipe, NgForOf, NgIf} from '@angular/common'
import {
	CalendarRecordService
} from '../../../../../services/calendars/calendar-record.service'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'

@Component({
	selector: 'app-calendar-page',
	standalone: true,
	imports: [
		NgForOf,
		AsyncPipe,
		DatePipe,
		RouterLink,
		NgIf,
		BTableComponent,
		BTemplateDirective
	],
	templateUrl: './calendar-page.component.html',
	styleUrl: './calendar-page.component.scss',
	providers: [CalendarService]
})
export class CalendarPageComponent {

	calendar$: Observable<any>

	constructor(
		private _calendarRecordService: CalendarRecordService,
		private _route: ActivatedRoute,
		private _router: Router,
	) {
		const id = this._route.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		this.calendar$ = this._calendarRecordService.getDayRecordsByCalendarId(id)
	}

	navigate(id: string) {
		this._router.navigate([id], {relativeTo: this._route})
	}
}
