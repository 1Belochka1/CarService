import {Component} from '@angular/core'
import {
	IWorker,
	UsersService
} from '../../../services/users.service'
import {ActivatedRoute, Router} from '@angular/router'
import {
	AsyncPipe,
	DatePipe,
	JsonPipe,
	NgForOf
} from '@angular/common'
import {BTableComponent} from '../../../components/b-table/b-table.component'
import {TabsComponent} from '../../../components/tabs/tabs.component'
import {TabsContentComponent} from '../../../components/tabs/tabs-content/tabs-content.component'
import {RecordsService} from '../../../services/records.service'
import {firstValueFrom, Observable} from 'rxjs'
import {BTemplateDirective} from '../../../direcrives/b-template.directive'
import {Priority} from '../../../enums/priority.enum'
import {TabsTableContentComponent} from '../../../components/tabs/tabs-table-content/tabs-table-content.component'

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
		NgForOf,
		DatePipe,
		TabsTableContentComponent
	],
	templateUrl: './worker-page.component.html',
	styleUrl: './worker-page.component.scss'
})
export class WorkerPageComponent {

	user: IWorker

	constructor(private _usersService: UsersService,
							private _router: ActivatedRoute,
							public recordsService: RecordsService
	) {
		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}

		firstValueFrom(_usersService.getWorker(id)).then((user) => this.user = user)

		recordsService.masterId = id

		this.recordsService.active = 'active'
		this.recordsService.update()
	}


	tabsChange(tab: TabsContentComponent) {
		switch (tab.type) {
			case 'active':
				console.log('active')
				this.recordsService.active = tab.type
				break
			case 'completed':
				this.recordsService.active = tab.type
				break
			default:
				return
		}
		this.recordsService.update()
	}

	protected readonly Priority = Priority
}
