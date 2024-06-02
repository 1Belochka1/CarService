import {Component, forwardRef} from '@angular/core'
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {
	CalendarComponent
} from '../../../components/calendar/calendar.component'
import {
	HeaderLendingComponent
} from '../../../components/header-lending/header-lending.component'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom, Observable} from 'rxjs'
import {DayRecord} from '../../../models/DayRecord.type'
import {
	DayRecordLendingService
} from '../../../services/day-record/day-record-lending.service'
import {CdkStepperModule} from '@angular/cdk/stepper'
import {StepperComponent} from '../../../components/stepper/stepper.component'
import {
	FormRecordComponent
} from '../../../components/form-record/form-record.component'
import {AuthService} from '../../../services/auth.service'

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
export class DayRecordLendingComponent {

	days: Observable<DayRecord[]>

	stepperSelected: number = 0

	isAuth: boolean

	constructor(
		private _dayRecordLendingService: DayRecordLendingService,
		private _authService: AuthService,
		private _router: ActivatedRoute
	) {
		const id = this._router.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		firstValueFrom(this._authService.getByCookie())
		.then(response => this.isAuth = response !== undefined)
		.catch(() => this.isAuth = false)

		this._dayRecordLendingService.startConnection()

		this._dayRecordLendingService.getDayRecordLending(id)

		this.days = _dayRecordLendingService.getDayRecords()

		this._dayRecordLendingService.listenUpdateRecord()

		this.isAuth = this._authService.user != undefined

	}

	public selectTime(id: string) {
		this.stepperSelected = 1
		this._dayRecordLendingService.bookTimeRecord(id)
		this._authService.logout()
	}

}
