import {Injectable} from '@angular/core'
import {IItem} from '../components/select/select.component'
import {
	GetClientsParamsRequest,
	SortClientProperty
} from './Requests/GetClientsParamsRequest'
import {TableService} from './table.service'
import {map} from 'rxjs'
import {ApiUrls} from './apiUrl'
import { GetListWithPageRequest } from './Requests/GetWorkersParamsRequest'

@Injectable({
	providedIn: 'root'
})
export class ClientsService extends TableService {
    public override setQuery(params: GetListWithPageRequest): void {
        throw new Error('Method not implemented.')
    }


	override sortProperties: IItem<SortClientProperty>[] = [
		{
			value: -1,
			name: 'Не выбрано'
		},
		{
			value: SortClientProperty.EMAIL,
			name: 'Почта'
		},
		{
			value: SortClientProperty.LASTNAME,
			name: 'Фамилия'
		},
		{
			value: SortClientProperty.FIRSTNAME,
			name: 'Имя'
		},
		{
			value: SortClientProperty.PATRONYMIC,
			name: 'Отчество'
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

	constructor() {
		super()
	}

	public override update(): void {
		this.isLoading = true

		const params = new GetClientsParamsRequest(
			this.search,
			this.currentPage,
			this.pageSize,
			this.sortProperty,
			this.sortDescending
		)

		this.items = this.httpClient.get<any>(ApiUrls.users.getClients, {
			params: params.value,
			withCredentials: true
		}).pipe(map(
			(data: any) => {

				if (data.totalPages != null) {
					this.totalPages = data.totalPages
					this.currentPage = data.currentPage
				}

				this.totalCount = data.totalCount

				this.isLoading = false

				return data.items
			}
		))
	}
}
