import {Component} from '@angular/core'
import {RouterOutlet} from '@angular/router'
import {AuthService} from './services/auth.service'
import {
	HeaderLendingComponent
} from './components/header-lending/header-lending.component'

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet, HeaderLendingComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss'
})
export class AppComponent {
	constructor(private _authService: AuthService) {
	}
}
