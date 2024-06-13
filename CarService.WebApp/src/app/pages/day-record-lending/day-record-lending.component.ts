import {Component, forwardRef, TemplateRef} from '@angular/core'
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {
	HeaderLendingComponent
} from '../../components/header-lending/header-lending.component'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom, Observable} from 'rxjs'
import {DayRecord} from '../../models/DayRecord.type'
import {DayRecordService} from '../../services/day-record/day-record.service'
import {CdkStepperModule} from '@angular/cdk/stepper'
import {StepperComponent} from '../../components/stepper/stepper.component'
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
		HeaderLendingComponent,
		forwardRef(() => StepperComponent),
		CdkStepperModule,
		FormTimeRecordComponent
	],
	templateUrl: './day-record-lending.component.html',
	styleUrl: './day-record-lending.component.scss',
	providers: [DayRecordService, ModalService]
})
export class DayRecordLendingComponent {

	days: Observable<DayRecord[]>

	isAuth: boolean

	private name: string
	private phone: string
	private email: string;

	constructor(
		private _dayRecordLendingService: DayRecordService,
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

		this._dayRecordLendingService.createHub()

		this._dayRecordLendingService.startConnection()

		this._dayRecordLendingService.getDayRecordLending(id)

		this.days = _dayRecordLendingService.getDayRecords()

		this._dayRecordLendingService.listenUpdateRecord()

	}

	public openModal(template: TemplateRef<any>, id: string) {
		this._dayRecordLendingService.bookTimeRecord(id)

		this._modalService.open(template, {actionVisible: false})?.subscribe(
			(isConfirm) => {
				if (isConfirm) {
					this._dayRecordLendingService.updateRecord(id, this.email, this.phone, this.name)
				} else {
					this._dayRecordLendingService.cancelBooking()
				}
			}
		)
	}

	public submit(event: any) {
		this.name = event.name
		this.phone = event.phone
		this.email = event.email
		this._modalService.closeModal(true)
	}

}
