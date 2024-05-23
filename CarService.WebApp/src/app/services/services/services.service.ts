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

	public override setParams() {

		// this.filterProperty = "ServiceTypes.Name"
		// this.filterValue = "Ремонт двигателя"

		this.params = new GetListWithPageAndFilterRequest(
			this.search,
			this.currentPage,
			this.pageSize,
			this.sortProperty?.value,
			this.sortDescending,
			this.filterProperty,
			this.filterValue
		)
	}


}
