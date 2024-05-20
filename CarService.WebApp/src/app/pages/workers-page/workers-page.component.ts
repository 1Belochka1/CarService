import {Component, inject} from '@angular/core'
import {FormsModule} from '@angular/forms'
import {SvgIconComponent} from 'angular-svg-icon'
import {
	AsyncPipe,
	NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {SearchComponent} from '../../components/search/search.component'
import {UsersService} from '../../services/users.service'
import {PaginationComponent} from '../../components/pagination/pagination.component'
import {SelectComponent} from '../../components/select/select.component'
import {Router, RouterLink} from '@angular/router'
import {BTableComponent} from '../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {WorkersService} from '../../services/workers.service'


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
}
