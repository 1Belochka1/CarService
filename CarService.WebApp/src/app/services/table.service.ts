import {map, Observable} from 'rxjs'
import {HttpClient} from '@angular/common/http'
import {inject} from '@angular/core'
import {IItem} from '../components/select/select.component'
import {GetListWithPageRequest} from './Requests/GetWorkersParamsRequest'

export abstract class TableService {
	httpClient: HttpClient = inject(HttpClient)

	isLoading: boolean = false
	notFound: boolean = false

	search: string = ''

	currentPage: number = 1
	pageSize: number = 15
	totalPages: number = 0
	totalCount: number = 0

	sortProperty: any = null
	sortProperties: IItem<any>[]
	sortDescending: boolean | null = null

	query: Observable<any>
	params: GetListWithPageRequest

	items: Observable<any[]>


	public searchChange(search: string) {
		console.log(search)
		this.search = search
		this.update()
	}

	public toggleDescending() {
		this.sortDescending = !this.sortDescending
		this.update()
	}

	public currentPageChanged(curPage: number) {
		this.currentPage = curPage
		this.update()
	}


	public sortChanged(event: number): void {
		if (event == 0) {
			this.sortDescending = null
			this.sortProperty = null
		} else {
			this.sortProperty = this.sortProperties[event].value

			if (this.sortDescending == null)
				this.sortDescending = false
		}

		this.update()
	}

	public update(): void {

		this.isLoading = true

		this.params = new GetListWithPageRequest(
			this.search,
			this.currentPage,
			this.pageSize,
			this.sortProperty,
			this.sortDescending
		)

		this.setQuery(this.params)

		this.items = this.query.pipe(map(
				(data: any) => {

					console.log(data)

					if (data.totalPages != null) {
						this.totalPages = data.totalPages
						this.currentPage = data.currentPage
					}

					this.notFound = data.totalItems == 0

					this.totalCount = data.totalItems


					this.isLoading = false

					return data.items
				}
			)
		)
	}

	/*
	*  Метод для указания http запроса на получени данных
	*/
	public abstract setQuery(params: GetListWithPageRequest): void

}