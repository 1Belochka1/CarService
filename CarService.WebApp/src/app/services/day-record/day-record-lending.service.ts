import {inject, Injectable, OnDestroy} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {BehaviorSubject, firstValueFrom} from 'rxjs'
import {DayRecord} from '../../models/DayRecord.type'
import {apiHubUrls, apiUrls} from '../apiUrl'
import {
	HttpTransportType,
	HubConnection,
	HubConnectionBuilder,
	LogLevel
} from '@microsoft/signalr'
import {TimeRecord} from '../../models/TimeRecord.type'

@Injectable({
	providedIn: 'root'
})
export class DayRecordLendingService implements OnDestroy {

	private _dayRecords: BehaviorSubject<DayRecord[]> = new BehaviorSubject<DayRecord[]>([])

	private _http = inject(HttpClient)

	private _hubConnection: HubConnection

	private _selectTimeId: string

	constructor() {

		this._hubConnection =
			new HubConnectionBuilder()
			.withUrl(apiHubUrls.timeRecords, {
				withCredentials: true
			})
			.withAutomaticReconnect()
			.withServerTimeout(60000)
			.withKeepAliveInterval(60000)
			.configureLogging(LogLevel.Debug)
			.build()
	}

	startConnection() {
		this._hubConnection.start().catch(e => console.error(e))
	}

	listenUpdateRecord() {
		this._hubConnection.on('UpdateTimeRecord', (data: TimeRecord) => {
			console.log(data)
			this._dayRecords.value.forEach((day) => {
				day.timeRecords.forEach((time) => {
					if (time.id === data.id) {
						time.isBusy = data.isBusy
					}
				})
			})

			this._dayRecords.next(this._dayRecords.value)
		})
	}

	bookTimeRecord(timeRecordId: string) {
		if (this._selectTimeId !== undefined) {
			this.cancelBooking()
		}

		this._selectTimeId = timeRecordId

		this._hubConnection.invoke('BookTimeRecord', timeRecordId).catch(err => console.error(err))
	}

	cancelBooking() {
		this._hubConnection.invoke('CancelBooking').catch(err => console.error(err))
	}

	getDayRecordLending(calendarId: string): void {
		firstValueFrom(this._http.get<DayRecord[]>(apiUrls.dayRecords.getByCalendarId + calendarId))
		.then(dayRecords => {
			this._dayRecords.next(dayRecords)
		})
	}

	getDayRecords() {
		return this._dayRecords.asObservable()
	}

	ngOnDestroy(): void {
		this._hubConnection.stop().catch(e => console.error(e))
	}


}
