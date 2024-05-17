import {Component} from '@angular/core';
import {InputTextModule} from 'primeng/inputtext'

@Component({
	selector: 'app-auth-page',
	standalone: true,
	imports: [
		InputTextModule
	],
	templateUrl: './auth-page.component.html',
	styleUrl: './auth-page.component.scss'
})
export class AuthPageComponent {
}
