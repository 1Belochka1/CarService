import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {apiUrls} from '../apiUrl'
import {Observable, tap} from 'rxjs'
import {listWithPage, TableService} from '../table.service'
import {GetListWithPageRequest} from '../Requests/GetWorkersParamsRequest'
import {RecordType} from '../../models/record.type'
import {list} from 'postcss'

@Injectable({
	providedIn: 'root'
})
export class RecordsTableService extends TableService {

	masterId: string

	method: 'active' | 'completed' | 'all'

	constructor(private http: HttpClient) {
		super()
	}

	getCompletedByMasterId() {
		this.method = 'completed'
		this.setParams()
		this.query = this.http.get<listWithPage<RecordType>>(apiUrls.records.getCompletedByMasterId + this.masterId, {
			params: this.params.value,
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	getActiveByMasterId() {
		this.method = 'active'
		this.setParams()
		this.query = this.http.get<listWithPage<RecordType>>(apiUrls.records.getActiveByMasterId + this.masterId, {
			params: this.params.value,
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	getAll() {
		this.method = 'all'
		this.setParams()
		this.query = this.http.get<listWithPage<RecordType>>(apiUrls.records.getAll, {
			params: this.params.value,
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	public override setQuery(): void {
		switch (this.method) {
			case 'active':
				this.getActiveByMasterId()
				break
			case 'completed':
				this.getCompletedByMasterId()
				break
			case 'all':
				this.getAll()
				break
			default:
				break
		}
	}

}
