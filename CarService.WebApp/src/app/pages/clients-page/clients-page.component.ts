import {Component} from '@angular/core'
import {
	IClient,
	IWorker,
	UsersService
} from '../../services/users.service'
import {
	GetWorkersParamsRequest,
	SortMastersProperty
} from '../../services/Requests/GetWorkersParamsRequest'
import {map, Observable} from 'rxjs'
import {
	AsyncPipe,
	DatePipe, NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {
	PaginationComponent
} from '../../components/pagination/pagination.component'
import {
	SearchComponent
} from '../../components/search/search.component'
import {
	IItem,
	SelectComponent
} from '../../components/select/select.component'
import {
	GetClientsParamsRequest,
	SortClientProperty
} from '../../services/Requests/GetClientsParamsRequest'
import {SvgIconComponent} from 'angular-svg-icon'
import {BTableComponent} from '../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'

@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [AsyncPipe, NgForOf, NgIf, PaginationComponent, SearchComponent, SelectComponent, DatePipe, SvgIconComponent, NgClass, BTableComponent, BTemplateDirective],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss'
})
export class ClientsPageComponent {
	search: string = ''

	currentPage: number = 1
	pageSize: number = 15
	totalPages: number = 0
	totalCount: number = 0

	sortProperties: IItem<SortClientProperty>[] = [
		{
			value: -1,
			name: 'Не выбрано'
		},
		{
			value: SortClientProperty.EMAIL,
			name: 'Почта'
		},
		{
			value: SortClientProperty.FULLNAME,
			name: 'ФИО'
		},
		{
			value: SortClientProperty.ADDRESS,
			name: 'Адрес'
		},
		{
			value: SortClientProperty.PHONE,
			name: 'Номер'
		},

		{
			value: SortClientProperty.LASTRECORD,
			name: 'Последняя заявка'
		},
	]

	sortProperty: SortClientProperty | null = null
	sortDescending: boolean | null = null

	workers: Observable<IClient[]>

	isLoading: boolean

	constructor(private _userService: UsersService) {
		console.log(SortMastersProperty)
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
			this.sortProperty = this.sortProperties[event].value as SortClientProperty

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

		this.workers = this._userService.getClients(new GetClientsParamsRequest(
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
