import {Component, inject} from '@angular/core'
import {FormsModule} from '@angular/forms'
import {SvgIconComponent} from 'angular-svg-icon'
import {
	AsyncPipe,
	NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {SearchComponent} from '../../../../components/search/search.component'
import {PaginationComponent} from '../../../../components/pagination/pagination.component'
import {SelectComponent} from '../../../../components/select/select.component'
import {Router, RouterLink} from '@angular/router'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {AboutComponent} from '../../../../components/about/about.component'
import {BTableSortDirective} from '../../../../direcrives/b-table-sort.directive'
import {UsersService} from '../../../../services/users/users.service'
import {WorkersService} from '../../../../services/users/workers.service'
import {SortMastersProperty} from '../../../../services/Requests/GetWorkersParamsRequest'



@Component({
	selector: 'app-workers-page',
	standalone: true,
	imports: [
		FormsModule,
		SvgIconComponent,
		NgForOf,
		SearchComponent,
		AsyncPipe,
		NgClass,
		PaginationComponent,
		NgIf,
		SelectComponent,
		RouterLink,
		BTableComponent,
		BTemplateDirective,
		AboutComponent,
		BTableSortDirective,
	],
	templateUrl: './workers-page.component.html',
	styleUrl: './workers-page.component.scss',
})
export class WorkersPageComponent {
	workersService: WorkersService = inject(WorkersService)

	constructor(private _userService: UsersService, private _router: Router) {
		this.workersService.update()
	}

	// TODO перенести в клиента
	goToWorker(id: string) {
		this._router.navigate(['account', 'worker', id])
				.then(() => {
					console.log('success')
				})
				.catch((e) => {
					console.log(e)
					// TODO: добавить уведомления
				})
	}

	protected readonly WorkersService = WorkersService
	protected readonly SortMastersProperty = SortMastersProperty
}
