import {Injectable} from '@angular/core'
import {HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr'
import {ToastrService} from 'ngx-toastr'
import {firstValueFrom} from 'rxjs'
import {apiHubUrls} from './apiUrl'
import {AuthService} from './auth.service'

@Injectable({
	providedIn: 'root'
})
export class NotifyService {
	private _hubConnection: HubConnection

	constructor(private _toastr: ToastrService, private _authService: AuthService) {

	}

	createHub() {
		this._hubConnection =
			new HubConnectionBuilder()
				.withUrl(apiHubUrls.notify, {
					withCredentials: true,
					transport: HttpTransportType.WebSockets
				})
				.withStatefulReconnect()
				.withAutomaticReconnect()
				.withServerTimeout(60000)
				.withKeepAliveInterval(60000)
				.configureLogging(LogLevel.Debug)
				.build()

		this._hubConnection.on('SuccessRequest', (message: string) => {
			this._toastr.success(message)
		})
	}

	startConnection() {
		this._hubConnection.start()
			.then(() => firstValueFrom(this._authService.getByCookie()).then(user => {
				if (user?.userAuth.roleId == 2) {
					this.onNewRequestForYou()
				}
			}))
			.catch(e => console.error(e))
	}

	accessDenied() {
		this._toastr.error('')
	}

	onNewRequestForYou() {
		console.log('слушаем')
		this._hubConnection.on('NewRequestForYou', (id) => {
			this._toastr.info('У вас новая заявка, вы можете посмотреть ее в' +
				' таблице')
			console.log(id)
		})
	}
}
