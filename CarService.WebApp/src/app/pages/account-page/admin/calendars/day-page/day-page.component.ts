import { Component } from '@angular/core';
import {CalendarService} from '../../../../../services/calendars/calendar.service'
import {ActivatedRoute} from '@angular/router'
import {DayRecordsService} from '../../../../../services/day-record/day-records.service'
import {Observable} from 'rxjs'
import {TimeRecord} from '../../../../../models/TimeRecord.type'
import {AsyncPipe, JsonPipe, NgForOf} from '@angular/common'

@Component({
  selector: 'app-day-page',
  standalone: true,
	imports: [
		AsyncPipe,
		JsonPipe,
		NgForOf
	],
  templateUrl: './day-page.component.html',
  styleUrl: './day-page.component.scss',
	providers: [DayRecordsService]
})
export class DayPageComponent {

	times$: Observable<TimeRecord[]>

	// constructor(
	// 	private _dayRecordsService: DayRecordsService,
	// 	private _router: ActivatedRoute,
	// ) {
	// 	const id = this._router.snapshot.paramMap.get('dayId')
	//
	// 	if (id === null) {
	// 		return
	// 	}
	//
	// 	this.times$ = _dayRecordsService.getTimeRecordsByDayRecordId(id)
	// }
}
