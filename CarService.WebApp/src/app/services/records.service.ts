import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import {ApiUrls} from './apiUrl'
import {tap} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class RecordsService {

	constructor(private http: HttpClient) { }

	getCompletedByMasterId(masterId: string) {
		return this.http.get(ApiUrls.records.getCompletedByMasterId + masterId, {withCredentials: true})
							 .pipe(tap(d => console.log(d)))
	}

	getActiveByMasterId(masterId: string) {
		return this.http.get(ApiUrls.records.getActiveByMasterId + masterId, {withCredentials: true})
							 .pipe(tap(d => console.log(d)))
	}
}
