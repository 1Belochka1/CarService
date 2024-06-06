import {catchError, map, Observable} from 'rxjs'
import {HttpClient} from '@angular/common/http'
import {inject} from '@angular/core'
import {IItem} from '../components/select/select.component'
import {
	GetListWithPageAndFilterRequest,
	GetListWithPageRequest
} from './Requests/GetWorkersParamsRequest'

export type listWithPage<T> = {
	totalItems: number
	totalPages: number
	currentPage: number
	items: T[]
}


export abstract class TableService {
	httpClient: HttpClient = inject(HttpClient)

	isLoading: boolean = false
	notFound: boolean = false

	search: string = ''

	currentPage: number = 1
	pageSize: number = 15
	totalPages: number = 0
	totalCount: number = 0

	sortProperty: IItem<any> | null = null
	sortProperties: IItem<any>[]
	sortDescending: boolean | null = null

	query: Observable<any>

	params: GetListWithPageRequest | GetListWithPageAndFilterRequest

	items$: Observable<any[]>

	protected constructor() {
		this.setParams()
	}

	public searchChange(search: string) {
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
			this.sortProperty = this.sortProperties[event]

			if (this.sortDescending == null)
				this.sortDescending = false
		}

		this.update()
	}

	public setParams() {
		this.params = new GetListWithPageRequest(
			this.search,
			this.currentPage,
			this.pageSize,
			this.sortProperty?.value,
			this.sortDescending
		)
	}

	public update(): void {

		this.isLoading = true

		this.setQuery()

		this.items$ = this.query.pipe(
			catchError(err => {
				this.isLoading = false
				this.notFound = true
				return new Observable()
			}),
			map(
				(data: listWithPage<any>) => {

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
	public abstract setQuery(): void

}