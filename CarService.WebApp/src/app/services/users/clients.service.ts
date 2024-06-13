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
export class ClientsService {

	constructor() {
	}

}
