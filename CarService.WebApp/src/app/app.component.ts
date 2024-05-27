import {Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {UsersService} from "./services/users/users.service";
import {AuthService} from './services/auth.service'

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
	constructor(private _authService: AuthService) {
	}

	ngOnInit(): void {
    }
}
