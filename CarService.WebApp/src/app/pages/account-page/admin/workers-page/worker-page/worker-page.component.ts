import {Component} from '@angular/core'
import {ActivatedRoute, Router} from '@angular/router'
import {
	AsyncPipe,
	DatePipe,
	JsonPipe,
	NgForOf, NgIf
} from '@angular/common'
import { TabsComponent } from '../../../../../components/tabs/tabs.component'
import {BTableComponent} from '../../../../../components/b-table/b-table.component'
import {TabsContentComponent} from '../../../../../components/tabs/tabs-content/tabs-content.component'
import {BTemplateDirective} from '../../../../../direcrives/b-template.directive'
import {AboutComponent} from '../../../../../components/about/about.component'
import {TabsTableContentComponent} from '../../../../../components/tabs/tabs-table-content/tabs-table-content.component'
import {RecordsTableService} from '../../../../../services/records/records-table.service'
import {
	IWorker,
	UsersService
} from '../../../../../services/users/users.service'
import {firstValueFrom} from 'rxjs'
import {Priority} from '../../../../../enums/priority.enum'


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
		AboutComponent
	],
	providers: [RecordsTableService],
	templateUrl: './worker-page.component.html',
	styleUrl: './worker-page.component.scss',

})
export class WorkerPageComponent {

	user: IWorker

	constructor(private _usersService: UsersService,
							private _router: ActivatedRoute,
							public recordsService: RecordsTableService
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

	protected readonly Priority = Priority
}
