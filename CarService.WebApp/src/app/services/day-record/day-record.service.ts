import {HttpClient} from '@angular/common/http'
import {inject, Injectable, OnDestroy} from '@angular/core'
import {
	HubConnection,
	HubConnectionBuilder,
	LogLevel,
} from '@microsoft/signalr'
import {ToastrService} from 'ngx-toastr'
import {BehaviorSubject, firstValueFrom} from 'rxjs'
import {DayRecord} from '../../models/DayRecord.type'
import {TimeRecord} from '../../models/TimeRecord.type'
import {apiHubUrls, apiUrls} from '../apiUrl'

@Injectable({
	providedIn: 'root',
})
export class DayRecordService implements OnDestroy {
	private _dayRecords: BehaviorSubject<DayRecord[]> = new BehaviorSubject<
		DayRecord[]
	>([])

	private _http = inject(HttpClient)

	private _hubConnection: HubConnection

	private _selectTimeId: string

	constructor(private _toastr: ToastrService) {
	}

	createHub() {
		this._hubConnection = new HubConnectionBuilder()
		.withUrl(apiHubUrls.timeRecords, {
			withCredentials: true,
		})
		.withAutomaticReconnect()
		.configureLogging(LogLevel.Debug)
		.build()

	}

	startConnection() {
		this._hubConnection.start().catch(e => console.error(e))
	}

	updateRecord(id: string, email: string, phone: string, name: string) {
		this._hubConnection.invoke('RecordTimeRecord', [id, true, email, phone, name])
				.then(() => {
					this._toastr.success('Вы записаны')
				})
		// return this._http.post(apiUrls.timeRecords.update, {
		// 	id, email, phone, name
		// })
	}

	listenUpdateRecord() {
		this._hubConnection.on('UpdateTimeRecord', (data: TimeRecord) => {
			console.log(data)
			this._dayRecords.value.forEach(day => {
				day.timeRecords.forEach(time => {
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

		this._hubConnection
				.invoke('BookTimeRecord', timeRecordId)
				.catch(err => console.error(err))
	}

	cancelBooking() {
		this._hubConnection.invoke('CancelBooking').catch(err => console.error(err))
	}

	getDayRecordLending(calendarId: string): void {
		firstValueFrom(
			this._http.get<DayRecord[]>(
				apiUrls.dayRecords.getByCalendarId + calendarId
			)
		).then(dayRecords => {
			this._dayRecords.next(dayRecords)
		})
	}

	getDayRecords() {
		return this._dayRecords.asObservable()
	}

	getDayRecord(id: string) {
		return this._http.get<DayRecord>(apiUrls.dayRecords.getById + id, {withCredentials: true})
	}

	getTimeRecord(id: string) {
		return this._http.get<TimeRecord[]>(
			apiUrls.timeRecords.getAllByDayRecordId + id,
			{
				withCredentials: true,
			}
		)
	}

	deleteDayRecord(id: string) {
		return this._http.delete(apiUrls.dayRecords.delete + id, {
			withCredentials: true
		})
	}

	deleteTimeRecord(id: string) {
		return this._http.delete(apiUrls.timeRecords.delete + id, {
			withCredentials: true
		})
	}

	onDeleteTimeRecord() {
		this._hubConnection.on('deletetimerecord', () => {
			console.log('delete')
		})
	}

	fillDaysRecord(
		id: string,
		startDate: Date,
		endDate: Date,
		timeStart: string,
		timeEnd: string,
		breakStartTime: string,
		breakEndTime: string,
		duration: number
	) {
		console.log(id)
		return this._http.post(
			apiUrls.dayRecords.fill,
			{
				CalendarId: id,
				StartDate: startDate,
				EndDate: endDate,
				StartTime: timeStart,
				EndTime: timeEnd,
				BreakStartTime: breakStartTime,
				BreakEndTime: breakEndTime,
				Duration: duration,
				Offset: 0,
			},
			{withCredentials: true}
		)
	}

	ngOnDestroy(): void {
		if (this._hubConnection)
			this._hubConnection.stop().catch(e => console.error(e))
	}
}
