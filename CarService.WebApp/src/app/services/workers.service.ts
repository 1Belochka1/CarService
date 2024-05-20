import {Injectable} from '@angular/core'
import {
	GetListWithPageRequest,
	SortMastersProperty
} from './Requests/GetWorkersParamsRequest'
import {TableService} from './table.service'
import {ApiUrls} from './apiUrl'
import {IItem} from '../components/select/select.component'
import {SortClientProperty} from './Requests/GetClientsParamsRequest'

@Injectable({
	providedIn: 'root'
})
export class WorkersService extends TableService {

	override sortProperties: IItem<SortMastersProperty>[] = [
		{
			value: -1,
			name: 'Не выбрано'
		},
		{
			value: SortMastersProperty.EMAIL,
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
			value: SortMastersProperty.ADDRESS,
			name: 'Адрес'
		},
		{
			value: SortMastersProperty.PHONE,
			name: 'Номер'
		},
	]

	constructor() {
		super()
	}

	override setQuery(params: GetListWithPageRequest) {
		this.query = this.httpClient.get<any>(ApiUrls.users.getWorkers, {
				params: params.value,
				withCredentials: true
			}
		)
	}

}
