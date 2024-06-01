import {Component} from '@angular/core'
import {CalendarService} from '../../../../../services/calendars/calendar.service'
import {Observable} from 'rxjs'
import {AsyncPipe, NgForOf} from '@angular/common'
import {RouterLink} from '@angular/router'

@Component({
	selector: 'app-calendars-page',
	standalone: true,
	imports: [
		NgForOf,
		AsyncPipe,
		RouterLink
	],
	templateUrl: './calendars-page.component.html',
	styleUrl: './calendars-page.component.scss',
	providers: [CalendarService]
})
export class CalendarsPageComponent {
	calendars: Observable<any>

	constructor(private _calendarService: CalendarService) {
	}

}
