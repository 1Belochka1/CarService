import {Injectable} from '@angular/core'
import {IItem} from '../../components/select/select.component'
import {
	GetClientsParamsRequest,
	SortClientProperty
} from '../Requests/GetClientsParamsRequest'
import {TableService} from '../table.service'
import {map} from 'rxjs'
import {apiUrls} from '../apiUrl'
import {GetListWithPageRequest} from '../Requests/GetWorkersParamsRequest'

@Injectable({
	providedIn: 'root'
})
export class ClientsService extends TableService {


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

	public override setQuery(): void {
		this.setParams()

		this.query = this.httpClient.get<any>(apiUrls.users.getClients, {
			params: this.params.value,
			withCredentials: true
		})
	}
}
