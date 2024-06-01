import {Injectable} from '@angular/core'
import {
	GetListWithPageRequest,
	SortMastersProperty
} from '../Requests/GetWorkersParamsRequest'
import {TableService} from '../table.service'
import {apiUrls} from '../apiUrl'
import {IItem} from '../../components/select/select.component'
import {SortClientProperty} from '../Requests/GetClientsParamsRequest'

@Injectable({
	providedIn: 'root'
})
export class WorkersService extends TableService {

	headItems: IItem<SortMastersProperty>[] = [
		{
			value: -1,
			name: 'Не выбрано'
		},
		{
			value: SortMastersProperty.LASTNAME,
			name: 'Фио'
		},
		{
			value: SortMastersProperty.PHONE,
			name: 'Номер'
		},
		{
			value: SortMastersProperty.ADDRESS,
			name: 'Адрес'
		},
	]

	constructor() {
		super()
	}

	override setQuery() {
		this.setParams()

		this.query = this.httpClient.get<any>(apiUrls.users.getWorkers, {
				params: this.params.value,
				withCredentials: true
			}
		)
	}

}
