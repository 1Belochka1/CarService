import {Component} from '@angular/core'
import {RouterOutlet} from '@angular/router'
import {firstValueFrom} from "rxjs";
import {AuthService} from './services/auth.service'
import {
	HeaderLendingComponent
} from './components/header-lending/header-lending.component'
import {NotifyService} from './services/notify.service'

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet, HeaderLendingComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
	providers: [NotifyService],
})
export class AppComponent {
	constructor(private _authService: AuthService, private _notifyService: NotifyService) {
		firstValueFrom(this._authService.getByCookie()).then().catch()
		this._notifyService.createHub()
		this._notifyService.startConnection()
	}
}
