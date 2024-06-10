import {Component, TemplateRef} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom, Observable} from 'rxjs'
import {RecordsService} from '../../../../services/records/records.service'
import {RecordType} from '../../../../models/record.type'
import {AsyncPipe, DatePipe, JsonPipe, Location, NgIf} from '@angular/common'
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {ModalService} from '../../../../services/modal.service'
import {
	IItem,
	SelectComponent
} from '../../../../components/select/select.component'
import {Priority} from '../../../../enums/priority.enum'
import {Status} from '../../../../enums/status.enum'
import {AboutComponent} from '../../../../components/about/about.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {PaginatorModule} from 'primeng/paginator'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {
	AutocompleteComponent
} from '../../../../components/autocomplete/autocomplete.component'
import {UsersService} from '../../../../services/users/users.service'
import {
	TableWorkersComponent
} from '../../../../components/table-workers/table-workers.component'
import {ToastrService} from 'ngx-toastr'
import {AuthService} from '../../../../services/auth.service'

@Component({
	selector: 'app-day-record-lending-page',
	standalone: true,
	imports: [
		JsonPipe,
		DatePipe,
		FullNamePipe,
		NgIf,
		SelectComponent,
		AboutComponent,
		BTemplateDirective,
		PaginatorModule,
		ReactiveFormsModule,
		AutocompleteComponent,
		TableWorkersComponent,
		AsyncPipe,
	],
	templateUrl: './record-page.component.html',
	styleUrl: './record-page.component.scss',
	providers: [ModalService, UsersService, RecordsService]
})
export class RecordPageComponent {

	roleId$: Observable<number>

	priorityItems: IItem<Priority>[] =
		[
			{value: Priority['Низкий'], name: 'Низкий'},
			{value: Priority['Средний'], name: 'Средний'},
			{value: Priority['Высокий'], name: 'Высокий'},
			{
				value: Priority['Очень высокий'],
				name: 'Очень высокий'
			}
		]
	statusItems: IItem<Status>[] =
		[
			{value: Status['Новая'], name: 'Новая'},
			{value: Status['В обработке'], name: 'В обработке'},
			{value: Status['В ожидании'], name: 'В ожидании'},
			{value: Status['В работе'], name: 'В работе'},
			{value: Status['Выполнена'], name: 'Выполнена'}
		]
	record: RecordType
	requestForm: FormGroup
	prioritySelect?: number
	statusSelect?: number
	optionsMaster: { v: string, n: string }[]
	protected readonly status = Status
	protected readonly priority = Priority

	private idMaster: string

	constructor(
		private _router: ActivatedRoute,
		private _authService: AuthService,
		public recordsService: RecordsService,
		public modalService: ModalService,
		private _userService: UsersService,
		private fb: FormBuilder,
		private location: Location,
		private toastr: ToastrService
	) {

		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}

		this.getRecord(id)

		this.requestForm = this.fb.group({
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]]
		})



		this.roleId$ = this._authService.getRoleId()

	}

	getRecord(id: string) {
		firstValueFrom(this.recordsService.getById(id)).then((data) => {
			this.record = data
			this.prioritySelect = data.priority
			this.statusSelect = data.status
			console.log(this.record)
		})
	}

	dialogOpen(template: TemplateRef<any>, context: any, title: string) {
		this.modalService.open(template, {
			title: title,
			context: context
		})?.subscribe((result) => {

			if (result) {
				firstValueFrom(
					this.recordsService.update(this.record.id, this.prioritySelect, this.statusSelect)
				).then(() => {
					console.log(this.record, this.prioritySelect, this.statusSelect)

					if (this.prioritySelect != undefined) {
						console.log('зашел')
						this.record.priority = this.prioritySelect
					}

					if (this.statusSelect != undefined) {
						console.log('зашел')
						this.record.status = this.statusSelect
					}

					console.log(this.record, this.prioritySelect, this.statusSelect)

				})
			}
			if (!result) {
				console.log('cancel')
				this.prioritySelect = undefined
				this.statusSelect = undefined
			}
		})
	}

	dialogAddMasterOpen(template: TemplateRef<any>) {
		firstValueFrom(this._userService.getMasterAutocomplete())
		.then((x: any[]) => {
			this.optionsMaster = x.map(item => {
				return {n: item.item2, v: item.item1}
			})
		})

		this.modalService.open(template, {
			title: 'Добавление мастера',
		})?.subscribe((isConfirm) => {
			if (isConfirm && this.idMaster) {
				firstValueFrom(this.recordsService.addMaster(this.record.id, this.idMaster))
				.then(() => {
					this.getRecord(this.record.id)
				})
			}
		})
	}

	dialogDeleteConfirmOpen(template: TemplateRef<any>) {
		this.modalService.open(template, {
			title: 'Вы дейтвительно хотите удалить заявку?',
		})?.subscribe((isConfirm) => {
			if (isConfirm) {
				firstValueFrom(this.recordsService.delete(this.record.id))
				.then(() => {
					this.location.back()
					this.toastr.success('Заявка удалена', undefined,
						{timeOut: 1000})
				})
			}
		})
	}

	priorityUpdate(priority: number) {
		this.prioritySelect = priority
	}

	statusUpdate(status: number) {
		this.statusSelect = status
	}

	onAddMasterSubmit(event: { v: string, n: string }) {
		this.idMaster = event.v
	}
}
