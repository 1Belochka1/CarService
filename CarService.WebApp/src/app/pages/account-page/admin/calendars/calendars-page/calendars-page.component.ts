import {AsyncPipe, NgForOf, NgIf} from '@angular/common'
import {Component, TemplateRef} from '@angular/core'
import {
	FormBuilder,
	FormGroup, FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms'
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatButton, MatIconButton} from '@angular/material/button'
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatIcon} from '@angular/material/icon'
import {MatInput} from "@angular/material/input";
import {Router, RouterLink} from '@angular/router'
import {Observable, firstValueFrom, map} from 'rxjs'
import {AutocompleteComponent} from '../../../../../components/autocomplete/autocomplete.component'
import {BTableComponent} from '../../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../../direcrives/b-template.directive'
import {AuthService} from "../../../../../services/auth.service";
import {CalendarRecordService} from '../../../../../services/calendars/calendar-record.service'
import {ModalService} from '../../../../../services/modal.service'
import {ServicesService} from '../../../../../services/services/services.service'

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
		ReactiveFormsModule,
		AutocompleteComponent,
		MatIcon,
		MatIconButton,
		MatButton,
		MatAutocomplete,
		MatAutocompleteTrigger,
		MatFormField,
		MatInput,
		MatLabel,
		MatError,
		MatOption,
		FormsModule,
	],
	templateUrl: './calendars-page.component.html',
	styleUrl: './calendars-page.component.scss',
	providers: [CalendarRecordService, ModalService],
})
export class CalendarsPageComponent {
	calendars: Observable<any>

	requestForm: FormGroup

	services$: Observable<any>

	roleId$: Observable<number>

	constructor(
		private _fb: FormBuilder,
		private _calendarRecordService: CalendarRecordService,
		private _router: Router,
		private _modalService: ModalService,
		private _servicesService: ServicesService,
		private _authService: AuthService
	) {
		this.setItems()
		this.services$ = this._servicesService.getForAutocomplete().pipe(
			map(x =>
				x.map(v => {
					return {n: v.item2, v: v.item1}
				})
			)
		)

		this.requestForm = this._fb.group({
			name: ['', Validators.required],
			description: ['', Validators.required],
			service: [{n: 0, v: 0}, Validators.required],
		})

		this.roleId$ = this._authService.getRoleId$()

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
			console.log(this.requestForm)
			firstValueFrom(
				this._calendarRecordService.create(
					this.requestForm.get('name')?.value,
					this.requestForm.get('description')?.value,
					this.requestForm.get('service')?.value.v
				)
			).then(() => {
				this._modalService.closeModal(true)
				this.setItems()
			})
		}
	}


	displayFn(item: { n: string, v: string }): string {
		return item && item.n ? item.n : '';
	}
}
