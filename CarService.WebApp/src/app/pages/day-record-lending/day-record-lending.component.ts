import {Component, forwardRef, TemplateRef} from '@angular/core'
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {
	CalendarComponent
} from '../../components/calendar/calendar.component'
import {
	HeaderLendingComponent
} from '../../components/header-lending/header-lending.component'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom, Observable} from 'rxjs'
import {DayRecord} from '../../models/DayRecord.type'
import {
	DayRecordLendingService
} from '../../services/day-record/day-record-lending.service'
import {CdkStepperModule} from '@angular/cdk/stepper'
import {StepperComponent} from '../../components/stepper/stepper.component'
import {
	FormRecordComponent
} from '../../components/form-record/form-record.component'
import {AuthService} from '../../services/auth.service'
import {ModalService} from '../../services/modal.service'
import {
	FormTimeRecordComponent
} from '../../components/form-time-record/form-time-record.component'

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
		FormRecordComponent,
		FormTimeRecordComponent
	],
	templateUrl: './day-record-lending.component.html',
	styleUrl: './day-record-lending.component.scss',
	providers: [DayRecordLendingService, ModalService]
})
export class DayRecordLendingComponent {

	days: Observable<DayRecord[]>

	isAuth: boolean

	name: string

	phone: string

	constructor(
		private _dayRecordLendingService: DayRecordLendingService,
		private _authService: AuthService,
		private _router: ActivatedRoute,
		private _modalService: ModalService
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


	}

	public openModal(template: TemplateRef<any>, id: string) {
		this._dayRecordLendingService.bookTimeRecord(id)

		this._modalService.open(template, {actionVisible: false})?.subscribe(
			({isCancel, isConfirm}) => {
				if (isConfirm) {
					this._dayRecordLendingService.updateRecord(id, this.phone, this.name)
				}
				if (isCancel) {
					this._dayRecordLendingService.cancelBooking()
				}
			}
		)
	}

	public submit(event: { name: string, phone: string }) {
		this.name = event.name
		this.phone = event.phone
		this._modalService.closeModal({isConfirm: true, isCancel: false})
	}

}
