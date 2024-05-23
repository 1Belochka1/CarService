import {Injectable} from '@angular/core'
import {RequestService} from '../request.service'
import {Observable} from 'rxjs'
import {apiUrls} from '../apiUrl'
import {RecordType} from '../../models/record.type'

@Injectable({
	providedIn: 'root'
})
export class RecordsService extends RequestService {

	constructor() {
		super()
	}

	getById(id: string): Observable<any> {
		return this.httpClient.get<RecordType>(apiUrls.records.getById + id, {withCredentials: true})
	}

	update(id: string) {

	}
}
