import {Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {MenuComponent} from "./components/menu/menu.component";
import {AuthService} from "./services/auth.service";

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet, MenuComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss'
})
export class AppComponent {
	constructor() {
		const authService = inject(AuthService);

		authService.user.idRole = 10;
	}
}
