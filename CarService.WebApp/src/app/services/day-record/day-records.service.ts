import {ChangeDetectorRef, Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {apiHubUrls, apiUrls} from '../apiUrl'
import {TimeRecord} from '../../models/TimeRecord.type'
import {DayRecord} from '../../models/DayRecord.type'
import {
	HubConnection,
	HubConnectionBuilder, LogLevel
} from '@microsoft/signalr'
import {Observable} from 'rxjs'

@Injectable({
	providedIn: 'root'
})
export class DayRecordsService {


	dayRecords: DayRecord[]

	private _hubConnection: HubConnection

	constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {
		this._hubConnection =
			new HubConnectionBuilder()
			.withUrl(apiHubUrls.timeRecords)
			.withAutomaticReconnect()
			.withServerTimeout(60000)
			.withKeepAliveInterval(60000)
			.configureLogging(LogLevel.Debug)
			.build()

		this._hubConnection.start().then()

		this._hubConnection.on('UpdateTimeRecord', (data: TimeRecord) => {
			console.log(data)
			this.dayRecords.forEach((day) => {
				day.timeRecords.forEach((time) => {
					if (time.id === data.id) {
						time.isBusy = data.isBusy;
					}
				})
			})
			this.cdr.detectChanges()
		})
	}

	bookTimeRecord(timeRecordId: string) {
		this._hubConnection.invoke('BookTimeRecord', timeRecordId).catch(err => console.error(err));
	}

	getCalendars(): Observable<any> {
		return this.http.get(apiUrls.calendars.getAll, {withCredentials: true})
	}

	getDayRecordsByCalendarId(id: string) {
		// const request =
		// 	this.http.get<DayRecord[]>(apiUrls.dayRecords.getAllByCalendarId + id, {withCredentials: true})
		// 			.subscribe((data) => {
		// 				console.log(data)
		// 				this.dayRecords = data
		// 			})
	}
}
