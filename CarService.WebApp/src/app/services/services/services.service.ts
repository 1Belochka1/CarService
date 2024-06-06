import {Injectable} from '@angular/core'
import {TableService} from '../table.service'
import {
	GetListWithPageAndFilterRequest,
	GetListWithPageRequest
} from '../Requests/GetWorkersParamsRequest'
import {apiUrls} from '../apiUrl'
import {Observable} from 'rxjs'

@Injectable({
	providedIn: 'root'
})
export class ServicesService extends TableService {

	masterId: string

	serviceQuery: Observable<any>

	method: 'getAll' | 'getByMasterId' = 'getAll'

	constructor() {
		super()
	}

	public GetServicesLending() {
		return this.httpClient.get(apiUrls.services.getLending)
	}

	public GetService() {
		this.serviceQuery = this.httpClient.get<any>(apiUrls.services.get, {
			withCredentials: true,
			params: this.params.value
		})
	}

	public GetByMasterId() {
		this.query = this.httpClient.get<any>(apiUrls.services.get)
	}

	public override setQuery(): void {
		this.query = this.serviceQuery
	}

}
