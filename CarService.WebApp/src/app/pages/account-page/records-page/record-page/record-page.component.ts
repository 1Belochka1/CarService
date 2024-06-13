import {AsyncPipe, DatePipe, JsonPipe, Location, NgForOf, NgIf} from '@angular/common'
import {Component, TemplateRef} from '@angular/core'
import {
	FormBuilder,
	FormGroup, FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms'
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatSelect} from "@angular/material/select";
import {ActivatedRoute} from '@angular/router'
import {ToastrService} from 'ngx-toastr'
import {Observable, firstValueFrom} from 'rxjs'
import {AboutComponent} from '../../../../components/about/about.component'
import {AutocompleteComponent} from '../../../../components/autocomplete/autocomplete.component'
import {
	IItem,
	SelectComponent,
} from '../../../../components/select/select.component'
import {TableWorkersComponent} from '../../../../components/table-workers/table-workers.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {Priority} from '../../../../enums/priority.enum'
import {Status} from '../../../../enums/status.enum'
import {RecordType} from '../../../../models/record.type'
import {UserInfo} from "../../../../models/user-info.type";
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {AuthService} from '../../../../services/auth.service'
import {ModalService} from '../../../../services/modal.service'
import {RecordsService} from '../../../../services/records/records.service'
import {UsersService} from '../../../../services/users/users.service'

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
		ReactiveFormsModule,
		AutocompleteComponent,
		TableWorkersComponent,
		AsyncPipe,
		MatFormField,
		MatLabel,
		MatOption,
		MatSelect,
		NgForOf,
		MatAutocomplete,
		MatAutocompleteTrigger,
		MatInput,
		FormsModule,
	],
	templateUrl: './record-page.component.html',
	styleUrl: './record-page.component.scss',
	providers: [ModalService, UsersService, RecordsService],
})
export class RecordPageComponent {
	roleId$: Observable<number>

	priorityItems: IItem<Priority>[] = [
		{value: Priority['Низкий'], name: 'Низкий'},
		{value: Priority['Средний'], name: 'Средний'},
		{value: Priority['Высокий'], name: 'Высокий'},
		{
			value: Priority['Очень высокий'],
			name: 'Очень высокий',
		},
	]
	statusItems: IItem<Status>[] = [
		{value: Status['Новая'], name: 'Новая'},
		{value: Status['В обработке'], name: 'В обработке'},
		{value: Status['В ожидании'], name: 'В ожидании'},
		{value: Status['В работе'], name: 'В работе'},
		{value: Status['Выполнена'], name: 'Выполнена'},
	]

	record: RecordType
	requestForm: FormGroup
	prioritySelect?: number
	statusSelect?: number
	optionsMaster: { v: string; n: string }[]
	protected readonly status = Status
	protected readonly priority = Priority

	master: any
	private id: string

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

		this.id = id

		this.getRecord(this.id)

		this.requestForm = this.fb.group({
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]],
		})

		this.roleId$ = this._authService.getRoleId$()
	}

	getRecord(id: string) {
		firstValueFrom(this.recordsService.getById(id)).then(data => {
			this.record = data
			this.prioritySelect = data.priority
			this.statusSelect = data.status
			console.log(this.record)
		})
	}

	dialogStatusOpen(template: TemplateRef<any>, title: string) {
		this.modalService.open(template, {
			title: title,
		})?.subscribe((isConfirm) => {
			if (isConfirm)
				firstValueFrom(this.recordsService.update(
					this.record.id,
					this.record.priority,
					this.statusSelect
				)).then(() => {
					this.getRecord(this.id)
				})
		})
	}

	dialogPriorityOpen(template: TemplateRef<any>, title: string) {
		this.modalService.open(template, {
			title: title,
		})?.subscribe((isConfirm) => {
			if (isConfirm)
				firstValueFrom(this.recordsService.update(
					this.record.id,
					this.prioritySelect,
					this.record.status
				)).then(() => {
					this.getRecord(this.id)
				})
		})
	}


	dialogAddMasterOpen(template: TemplateRef<any>) {
		firstValueFrom(this._userService.getMasterAutocomplete()).then(
			(x: any[]) => {
				this.optionsMaster = x.map(item => {
					return {n: item.item2, v: item.item1}
				})
			}
		)

		this.modalService
			.open(template, {
				title: 'Добавление мастера',
			})
			?.subscribe(isConfirm => {
				if (isConfirm && this.master) {
					firstValueFrom(
						this.recordsService.addMaster(this.record.id, this.master.v)
					).then(() => {
						this.getRecord(this.record.id)
						this.toastr.success('Мастер добавлен')
					})
				}
			})
	}

	dialogDeleteConfirmOpen(template: TemplateRef<any>) {
		this.modalService
			.open(template, {
				title: 'Вы дейтвительно хотите удалить заявку?',
			})
			?.subscribe(isConfirm => {
				if (isConfirm) {
					firstValueFrom(this.recordsService.delete(this.record.id)).then(
						() => {
							this.location.back()
							this.toastr.success('Заявка удалена', undefined, {
								timeOut: 1000,
							})
						}
					)
				}
			})
	}


	displayFn(item: { n: string, v: string }): string {
		return item && item.n ? item.n : '';
	}

	removeWorker(masterId: string) {
		firstValueFrom(this.recordsService.deleteMaster(this.id, masterId))
			.then(() => {
				this.getRecord(this.id)
				this.toastr.success("Мастер удален")
			})
	}
}
