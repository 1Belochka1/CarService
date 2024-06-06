import {inject, Injectable} from '@angular/core'
import {Observable} from 'rxjs'
import {apiUrls} from '../apiUrl'
import {RecordType} from '../../models/record.type'
import {HttpClient} from '@angular/common/http'

@Injectable({
	providedIn: 'root'
})
export class RecordsService {

	httpClient: HttpClient = inject(HttpClient)

	constructor() {
	}

	getById(id: string): Observable<RecordType> {
		return this.httpClient.get<RecordType>(apiUrls.records.getById + id, {withCredentials: true})
	}

	update(id: string, priority?: number, status?: number) {
		return this.httpClient.post(apiUrls.records.update, {
			id,
			priority,
			status
		}, {withCredentials: true})
	}

	create(name: string,
				 phone: string,
				 description: string,
				 carInfo: string
	) {
		const req = {
			phone,
			firstName: name,
			carInfo,
			description,
		}

		return this.httpClient.post(apiUrls.records.createWithOutAuth, req)
	}

	addMaster(id: string, masterId: string) {
		return this.httpClient
							 .post(apiUrls.records.addMaster + id, [masterId],
								 {withCredentials: true})
	}
}
