import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {apiUrls} from '../apiUrl'

@Injectable({
	providedIn: 'root'
})
export class CalendarRecordService {

	calendars: any

	constructor(private _http: HttpClient) {

	}

	getAll() {
		return this._http.get<any[]>(apiUrls.calendars.getAll)
	}
}
