import {Injectable} from '@angular/core'
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr'
import {apiHubUrls} from './apiUrl'
import {ToastrService} from 'ngx-toastr'

@Injectable({
	providedIn: 'root'
})
export class NotifyService {
	private _hubConnection: HubConnection

	constructor(private _toastr: ToastrService) {
	}

	createHub() {
		this._hubConnection =
			new HubConnectionBuilder()
			.withUrl(apiHubUrls.notify, {
				withCredentials: true
			})
			.withAutomaticReconnect()
			.withServerTimeout(60000)
			.withKeepAliveInterval(60000)
			.configureLogging(LogLevel.Debug)
			.build()

		this._hubConnection.on("SuccessRequest", (message: string) => {
			this._toastr.success(message)
		})
	}

	startConnection() {
		this._hubConnection.start().catch(e => console.error(e))
	}

	accessDenied() {
		this._toastr.error("")
	}
}
