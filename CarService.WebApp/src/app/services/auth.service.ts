import {Injectable} from '@angular/core';

export type User = { password: string; idRole: number; login: string }

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	user: User = {
		login: 'belochka',
		password: 'belochka',
		idRole: 1
	}

	constructor() {
	}
}
