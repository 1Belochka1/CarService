import {Component} from '@angular/core'
import {UsersService} from '../../../services/users.service'
import {ActivatedRoute, Router} from '@angular/router'
import {AsyncPipe, JsonPipe, NgForOf} from '@angular/common'
import {BTableComponent} from '../../../components/b-table/b-table.component'
import {TabsComponent} from '../../../components/tabs/tabs.component'
import {TabsContentComponent} from '../../../components/tabs/tabs-content/tabs-content.component'
import {RecordsService} from '../../../services/records.service'
import {Observable} from 'rxjs'
import {BTemplateDirective} from '../../../direcrives/b-template.directive'

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
		NgForOf
	],
	templateUrl: './worker-page.component.html',
	styleUrl: './worker-page.component.scss'
})
export class WorkerPageComponent {

	completedWorks: Observable<any>
	activeWorks: Observable<any>

	worker: {
		info: any,
		w_active: any,
		w_completed: any,
		services: any
	}

	constructor(private _usersService: UsersService,
							private _router: ActivatedRoute,
							private _recordsService: RecordsService
	) {
		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}


		this._usersService.getWorker(id).subscribe(
			(worker) => {
				const active = worker.works.find((s) => s.status == 1)
				const completed = worker.works.find((s) => s.status == 2)

				this.worker = {
					info: worker,
					services: worker.services,
					w_active: active,
					w_completed: completed
				}

				console.log(this.worker)
			}
		)

		this.activeWorks = _recordsService.getActiveByMasterId(id)
		this.completedWorks = _recordsService.getCompletedByMasterId(id)
	}
}
