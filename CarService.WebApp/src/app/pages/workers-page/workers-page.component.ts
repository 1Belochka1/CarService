import {Component, inject, OnInit} from '@angular/core'
import {FormsModule} from '@angular/forms'
import {SvgIconComponent} from 'angular-svg-icon'
import {
	AsyncPipe,
	NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {SearchComponent} from '../../components/search/search.component'
import {map, Observable} from 'rxjs'
import {
	IWorker,
	UsersService
} from '../../services/users.service'
import {
	GetWorkersParamsRequest,
	SortMastersProperty
} from '../../services/Requests/GetWorkersParamsRequest'
import {PaginationComponent} from '../../components/pagination/pagination.component'
import {
	IItem,
	SelectComponent
} from '../../components/select/select.component'
import {Router, RouterLink} from '@angular/router'
import {BTableComponent} from '../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'


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
	search: string = ''

	currentPage: number = 1
	pageSize: number = 15
	totalPages: number = 0
	totalCount: number = 0

	sortProperties: IItem<SortMastersProperty>[] = [
		{
			value: -1,
			name: 'Не выбрано'
		},
		{
			value: SortMastersProperty.EMAIL,
			name: 'Почта'
		},
		{
			value: SortMastersProperty.FULLNAME,
			name: 'ФИО'
		},
		{
			value: SortMastersProperty.ADDRESS,
			name: 'Адрес'
		},
		{
			value: SortMastersProperty.PHONE,
			name: 'Номер'
		},
	]

	sortProperty: SortMastersProperty | null = null
	sortDescending: boolean | null = null

	workers: Observable<IWorker[]>

	isLoading: boolean

	constructor(private _userService: UsersService, private _router: Router) {
	}

	// TODO перенести в клиента
	goToWorker(id: string) {
		this._router.navigate(['account','worker', id])
				.then(() => {
					console.log('success')
				})
				.catch((e) => {
					console.log(e)
					// TODO: добавить уведомления
				})
	}

	searchUpdate(search: string) {
		this.search = search
		this.updateWorkers()
	}

	toggleDescending() {
		this.sortDescending = !this.sortDescending
		this.updateWorkers()
	}

	sortChanged(event: number): void {
		console.log(event)

		if (event == 0) {
			this.sortDescending = null
			this.sortProperty = null
		} else {
			this.sortProperty = this.sortProperties[event].value as SortMastersProperty

			if (this.sortDescending == null)
				this.sortDescending = false

		}

		this.updateWorkers()
	}

	currentPageChanged(curPage: number) {
		this.currentPage = curPage
		this.updateWorkers()
	}

	updateWorkers() {
		this.isLoading = true

		this.workers = this._userService.getMasters(new GetWorkersParamsRequest(
			this.search,
			this.currentPage,
			this.pageSize,
			this.sortProperty,
			this.sortDescending
		)).pipe(map(
			(data: any) => {

				if (data.totalPages != null) {
					this.totalPages = data.totalPages
					this.currentPage = data.currentPage
				}

				this.totalCount = data.totalCount

				this.isLoading = false

				return data.users
			}
		))
	}
}
