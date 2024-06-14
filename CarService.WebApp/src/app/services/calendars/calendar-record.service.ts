import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {apiUrls} from '../apiUrl'

@Injectable({
	providedIn: 'root'
})
export class CalendarRecordService {

	constructor(private _http: HttpClient) {
	}

	getAll() {
		return this._http.get<any[]>(apiUrls.calendars.getAll)
	}

	create(name: string, description: string, serviceId: string) {
		return this._http.post<any[]>(apiUrls.calendars.create, {
			name,
			description,
			serviceId
		}, {withCredentials: true})
	}

	getDayRecordsByCalendarId(id: string) {
		return this._http.get<any>(apiUrls.calendars.getById + id, {withCredentials: true})
	}

	delete(id: string) {
		return this._http.delete(apiUrls.calendars.delete + id, {withCredentials: true})
	}

	updateCalendar(id: string, name: string, description: string) {
		return this._http.post(apiUrls.calendars.update, {
			id, name, description
		}, {withCredentials: true})
	}
}
