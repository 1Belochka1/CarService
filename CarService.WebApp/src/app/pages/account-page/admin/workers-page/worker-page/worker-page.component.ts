import {Component, TemplateRef} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {
	AsyncPipe,
	DatePipe,
	JsonPipe,
	Location,
	NgForOf,
	NgIf
} from '@angular/common'
import {TabsComponent} from '../../../../../components/tabs/tabs.component'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	TabsContentComponent
} from '../../../../../components/tabs/tabs-content/tabs-content.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'
import {AboutComponent} from '../../../../../components/about/about.component'
import {
	TabsTableContentComponent
} from '../../../../../components/tabs/tabs-table-content/tabs-table-content.component'
import {
	IWorker,
	UsersService
} from '../../../../../services/users/users.service'
import {firstValueFrom, Observable} from 'rxjs'
import {Priority} from '../../../../../enums/priority.enum'
import {NotSpecifiedPipe} from '../../../../../pipe/not-specified.pipe'
import {ModalService} from '../../../../../services/modal.service'
import {ToastrService} from 'ngx-toastr'
import {RecordsService} from '../../../../../services/records/records.service'
import {RecordType} from '../../../../../models/record.type'
import {
	TableRecordsComponent
} from '../../../../../components/table-records/table-records.component'


@Component({
	selector: 'app-worker-page',
	standalone: true,
	imports: [
		AsyncPipe,
		JsonPipe,
		BTableComponent,
		TabsComponent,
		TabsContentComponent,
		BTemplateDirective,
		AboutComponent,
		NgForOf,
		DatePipe,
		TabsTableContentComponent,
		NgIf,
		AboutComponent,
		NotSpecifiedPipe,
		TableRecordsComponent
	],
	providers: [RecordsService, ModalService],
	templateUrl: './worker-page.component.html',
	styleUrl: './worker-page.component.scss',

})
export class WorkerPageComponent {

	workerId: string
	worker: IWorker
	activeWorks: Observable<RecordType[]>
	completedWorks: Observable<RecordType[]>
	protected readonly Priority = Priority

	constructor(private _usersService: UsersService,
							private _router: ActivatedRoute,
							private _modalService: ModalService,
							private _recordsService: RecordsService,
							private _location: Location,
							private _toastr: ToastrService
	) {
		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}

		this.workerId = id

		firstValueFrom(_usersService.getWorker(id)).then((user) => this.worker = user)

		this.activeWorks = _recordsService.getActiveByMasterId(id)
		this.completedWorks = _recordsService.getCompletedByMasterId(id)
	}

	openDismissModal(updateFormModal: TemplateRef<any>) {
		this._modalService.open(updateFormModal, {
			title: 'Вы действительно' +
				' хотите понизить роль пользователя до клиента?'
		})?.subscribe(({isConfirm}) => {
			if (isConfirm) {
				firstValueFrom(this._usersService.dismissById(this.workerId))
				.then(() => {
					this._location.back()
					this._toastr.success('Операция прошла успешно', 'Успешно', {progressBar: true})
				})
			}
		})
	}

	openDeleteModal(updateFormModal: TemplateRef<any>) {
		this._modalService.open(updateFormModal, {
			title: 'Вы действительно' +
				' хотите удалить аккаунт пользователя?'
		})?.subscribe(({isConfirm}) => {
			if (isConfirm) {
				firstValueFrom(this._usersService.delete(this.workerId))
				.then(() => {
					this._location.back()
					this._toastr.success('Операция прошла успешно', 'Успешно', {progressBar: true})
				})
			}
		})
	}
}
