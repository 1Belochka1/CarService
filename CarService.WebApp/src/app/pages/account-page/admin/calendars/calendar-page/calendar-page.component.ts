import {Component, TemplateRef} from '@angular/core'
import {ActivatedRoute, Router, RouterLink} from '@angular/router'
import {firstValueFrom, Observable} from 'rxjs'
import {AsyncPipe, DatePipe, Location, NgForOf, NgIf} from '@angular/common'
import {
	CalendarRecordService
} from '../../../../../services/calendars/calendar-record.service'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'
import {
	BTableSortDirective
} from '../../../../../direcrives/b-table-sort.directive'
import {ModalService} from '../../../../../services/modal.service'
import {FormGroup, ReactiveFormsModule} from '@angular/forms'
import {
	DateRangePickerComponent
} from '../../../../../components/date-picker/date-range-picker.component'
import {
	DayRecordService
} from '../../../../../services/day-record/day-record.service'
import {log} from '@angular-devkit/build-angular/src/builders/ssr-dev-server'
import {
	MatDatepickerToggle, MatDateRangeInput,
	MatDateRangePicker, MatEndDate, MatStartDate
} from '@angular/material/datepicker'
import {
	MatFormField,
	MatHint,
	MatLabel,
	MatSuffix
} from '@angular/material/form-field'
import {MatIcon} from '@angular/material/icon'
import {MatIconButton} from '@angular/material/button'

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
		BTemplateDirective,
		BTableSortDirective,
		ReactiveFormsModule,
		DateRangePickerComponent,
		MatDateRangePicker,
		MatDatepickerToggle,
		MatSuffix,
		MatHint,
		MatEndDate,
		MatStartDate,
		MatDateRangeInput,
		MatLabel,
		MatFormField,
		MatIcon,
		MatIconButton,
	],
	templateUrl: './calendar-page.component.html',
	styleUrl: './calendar-page.component.scss',
	providers: [CalendarRecordService, ModalService]
})
export class CalendarPageComponent {

	calendar$: Observable<any>
	HtmlInputElement = HTMLInputElement
	filter: FormGroup
	private readonly _id: string

	constructor(
		private _calendarRecordService: CalendarRecordService,
		private _route: ActivatedRoute,
		private _router: Router,
		private _location: Location,
		private _modalService: ModalService, private _dayRecord: DayRecordService
	) {
		const id = this._route.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		this._id = id

		this.setItem()
	}

	setItem() {
		this.calendar$ = this._calendarRecordService.getDayRecordsByCalendarId(this._id)
	}

	navigate(id: string) {
		this._router.navigate([id], {relativeTo: this._route})
	}

	convertEvent(event: Event) {
		return event.target as HTMLInputElement
	}

	delete(templateRef: TemplateRef<any>) {
		this._modalService.open(templateRef, {
			title: 'Вы дейстивтельно хотите' +
				' удалить расписание?'
		})
			?.subscribe(
				isConfirm => {
					if (isConfirm) {
						firstValueFrom(this._calendarRecordService.delete(this._id)).then(() => this._location.back())
					}
				}
			)
	}

	fill(templateRef: TemplateRef<any>) {
		this._modalService.open(templateRef, {
			title: '', actionVisible: false
		})?.subscribe()
	}

	submitFill(event: any) {
		console.log(event)
		firstValueFrom(this._dayRecord.fillDaysRecord(
			this._id,
			event.startDate,
			event.endDate,
			event.startTime,
			event.endTime,
			event.breakStartTime,
			event.breakEndTime,
			event.duration
		)).then(x => {
			this._modalService.closeModal(true)
			this
		})
	}
}
