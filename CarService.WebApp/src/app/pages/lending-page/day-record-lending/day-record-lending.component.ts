import {Component, forwardRef, OnDestroy} from '@angular/core'
import {CalendarService} from '../../../services/calendars/calendar.service'
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {
	CalendarComponent
} from '../../../components/calendar/calendar.component'
import {
	HeaderLendingComponent
} from '../../../components/header-lending/header-lending.component'
import {ActivatedRoute} from '@angular/router'
import {Observable} from 'rxjs'
import {DayRecord} from '../../../models/DayRecord.type'
import {
	DayRecordLendingService
} from '../../../services/day-record/day-record-lending.service'
import {CdkStepperModule} from '@angular/cdk/stepper'
import {StepperComponent} from '../../../components/stepper/stepper.component'
import {
	FormRecordComponent
} from '../../../components/form-record/form-record.component'

@Component({
	selector: 'app-day-record-lending',
	standalone: true,
	imports: [
		NgForOf,
		AsyncPipe,
		NgClass,
		NgIf,
		DatePipe,
		CalendarComponent,
		HeaderLendingComponent,
		forwardRef(() => StepperComponent),
		CdkStepperModule,
		FormRecordComponent
	],
	templateUrl: './day-record-lending.component.html',
	styleUrl: './day-record-lending.component.scss',
	providers: [DayRecordLendingService]
})
export class DayRecordLendingComponent  {

	days: Observable<DayRecord[]>

	constructor(
		private _dayRecordLendingService: DayRecordLendingService,
		private _router: ActivatedRoute
	) {
		const id = this._router.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		this._dayRecordLendingService.startConnection()

		this._dayRecordLendingService.getDayRecordLending(id)

		this.days = _dayRecordLendingService.getDayRecords()

		this._dayRecordLendingService.listenUpdateRecord()
	}

	public selectTime(id: string) {
		this._dayRecordLendingService.bookTimeRecord(id)
	}

}
