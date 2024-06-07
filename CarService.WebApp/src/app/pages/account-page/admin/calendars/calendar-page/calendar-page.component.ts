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
		BTableSortDirective
	],
	templateUrl: './calendar-page.component.html',
	styleUrl: './calendar-page.component.scss',
	providers: [CalendarRecordService, ModalService]
})
export class CalendarPageComponent {

	calendar$: Observable<any>
	HtmlInputElement = HTMLInputElement
	private readonly _id: string

	constructor(
		private _calendarRecordService: CalendarRecordService,
		private _route: ActivatedRoute,
		private _router: Router,
		private _location: Location,
		private _modalService: ModalService
	) {
		const id = this._route.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}

		this._id = id

		this.calendar$ = this._calendarRecordService.getDayRecordsByCalendarId(id)
	}

	navigate(id: string) {
		this._router.navigate([id], {relativeTo: this._route})
	}

	convertEvent(event: Event) {
		return event.target as HTMLInputElement
	}

	openModal(templateRef: TemplateRef<any>) {
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
}
