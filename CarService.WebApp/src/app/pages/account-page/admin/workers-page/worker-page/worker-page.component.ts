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
	RecordsTableService
} from '../../../../../services/records/records-table.service'
import {
	IWorker,
	UsersService
} from '../../../../../services/users/users.service'
import {firstValueFrom} from 'rxjs'
import {Priority} from '../../../../../enums/priority.enum'
import {NotSpecifiedPipe} from '../../../../../pipe/not-specified.pipe'
import {ModalService} from '../../../../../services/modal.service'
import {ToastrService} from 'ngx-toastr'


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
		NotSpecifiedPipe
	],
	providers: [RecordsTableService, ModalService],
	templateUrl: './worker-page.component.html',
	styleUrl: './worker-page.component.scss',

})
export class WorkerPageComponent {

	user: IWorker
	protected readonly Priority = Priority

	constructor(private _usersService: UsersService,
							private _router: ActivatedRoute,
							public recordsService: RecordsTableService,
							private _modalService: ModalService,
							private _location: Location,
							private toastr: ToastrService
	) {
		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}

		firstValueFrom(_usersService.getWorker(id)).then((user) => this.user = user)

		recordsService.masterId = id

		this.recordsService.getActiveByMasterId()
	}

	tabsChange(tab: TabsContentComponent) {
		switch (tab.type) {
			case 'active':
				console.log('active')
				this.recordsService.getActiveByMasterId()
				break
			case 'completed':
				this.recordsService.getCompletedByMasterId()
				break
			default:
				break
		}

		this.recordsService.update()
	}

	openDismissModal(updateFormModal: TemplateRef<any>) {
		this._modalService.open(updateFormModal, {
			title: 'Вы действительно' +
				' хотите понизить роль пользователя до клиента?'
		})?.subscribe(({isConfirm}) => {
			if (isConfirm) {
				firstValueFrom(this._usersService.dismissById(this.recordsService.masterId))
				.then(() => {
					this._location.back()
					this.toastr.success("Операция прошла успешно", "Успешно", {progressBar: true})
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
				firstValueFrom(this._usersService.delete(this.recordsService.masterId))
				.then(() => {
					this._location.back()
					this.toastr.success("Операция прошла успешно", "Успешно", {progressBar: true})
				})
			}
		})
	}
}
