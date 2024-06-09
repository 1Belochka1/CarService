import {inject, Injectable} from '@angular/core'
import {Observable, tap} from 'rxjs'
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

	create(
		email: string,
		description: string,
		carInfo: string,
		name?: string,
		phone?: string,
	) {
		const req = {
			email,
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

	getCompletedByMasterId(masterId: string) {
		return this.httpClient.get<RecordType[]>(apiUrls.records.getCompletedByMasterId + masterId, {
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	getActiveByMasterId(masterId: string) {
		return this.httpClient.get<RecordType[]>(apiUrls.records.getActiveByMasterId + masterId, {
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	getAll() {
		return this.httpClient.get<RecordType[]>(apiUrls.records.getAll, {
			withCredentials: true
		}).pipe(tap(d => console.log(d)))
	}

	delete(id: string) {
		return this.httpClient.delete(apiUrls.records.delete + id, {withCredentials: true})
	}
}
