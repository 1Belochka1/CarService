import {Component, TemplateRef} from '@angular/core'
import {firstValueFrom, map, Observable} from 'rxjs'
import {AsyncPipe, NgForOf, NgIf} from '@angular/common'
import {Router, RouterLink} from '@angular/router'
import {
	CalendarRecordService
} from '../../../../../services/calendars/calendar-record.service'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'
import {ModalService} from '../../../../../services/modal.service'
import {PaginatorModule} from 'primeng/paginator'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {
	ServicesService
} from '../../../../../services/services/services.service'
import {
	AutocompleteComponent
} from '../../../../../components/autocomplete/autocomplete.component'

@Component({
	selector: 'app-calendars-page',
	standalone: true,
	imports: [
		NgForOf,
		AsyncPipe,
		RouterLink,
		BTableComponent,
		BTemplateDirective,
		NgIf,
		PaginatorModule,
		ReactiveFormsModule,
		AutocompleteComponent
	],
	templateUrl: './calendars-page.component.html',
	styleUrl: './calendars-page.component.scss',
	providers: [CalendarRecordService, ModalService],
})
export class CalendarsPageComponent {
	calendars: Observable<any>

	requestForm: FormGroup

	services$: Observable<any>

	constructor(private _fb: FormBuilder,
							private _calendarRecordService: CalendarRecordService,
							private _router: Router,
							private _modalService: ModalService,
							private _servicesService: ServicesService
	) {
		this.setItems()
		this.services$ = this._servicesService.getForAutocomplete()
												 .pipe(map(
													x => x.map(v => {
														return {n: v.item2, v: v.item1}
													})))

		this.requestForm = this._fb.group({
			name: ['', Validators.required],
			description: ['', Validators.required],
			serviceId: ['', Validators.required],
		})
	}

	setItems() {
		this.calendars = this._calendarRecordService.getAll()
	}

	onAdd(templateRef: TemplateRef<any>) {
		this._modalService.open(templateRef, {actionVisible: false})
	}

	navigate(param: (string | any)[]) {
		this._router.navigate(param).then()
	}

	onSubmit() {
		if (this.requestForm.valid) {
			firstValueFrom(this._calendarRecordService.create(
				this.requestForm.get('name')?.value,
				this.requestForm.get('description')?.value,
				this.requestForm.get('serviceId')?.value
			)).then(() => {
					this._modalService.closeModal(true)
					this.setItems()
				}
			)
		}
	}

	serviceSelect($event: { v: string; n: string }) {
		this.requestForm.patchValue({serviceId: $event.v})
	}
}
