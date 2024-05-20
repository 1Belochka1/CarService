import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {ApiUrls} from './apiUrl'
import {Observable, tap} from 'rxjs'
import {TableService} from './table.service'
import {GetListWithPageRequest} from './Requests/GetWorkersParamsRequest'

@Injectable({
	providedIn: 'root'
})
export class RecordsService extends TableService {

	masterId: string

	recordQuery: Observable<any>

	active: string

	constructor(private http: HttpClient) {
		super()
	}

	getCompletedByMasterId() {
		this.recordQuery = this.http.get(ApiUrls.records.getCompletedByMasterId + this.masterId, {
			params: this.params.value,
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	getActiveByMasterId() {
		this.recordQuery = this.http.get(ApiUrls.records.getActiveByMasterId + this.masterId, {
			params: this.params.value,
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	public override setQuery(params: GetListWithPageRequest): void {

		switch (this.active) {
			case "active":
				this.getActiveByMasterId()
				break
			case "completed":
				this.getCompletedByMasterId()
		}

		this.query = this.recordQuery
	}

}
