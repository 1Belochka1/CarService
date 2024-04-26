import {Injectable} from '@angular/core';

export interface IUser {
	email: string
	password: string
	userInfo: {
		lastname: string
		firstname: string
		patronymic: string
		address: string
		phone: string
	}
	idRole: number
	role: {
		idRole: number
		name: string
	}
}

export interface IUserTable {
	email: string
	fullname: string
	address: string
	phone: string
	role: string
}


@Injectable({
	providedIn: 'root'
})
export class AuthService {

	user: IUser = {
		email: 'belochka@gmail.com',
		password: 'belochka',
		userInfo: {
			lastname: 'Иванов',
			firstname: 'Иван',
			patronymic: 'Иванович',
			address: 'Москва',
			phone: '8-999-999-99-99'
		},
		idRole: 1,
		role: {
			idRole: 1,
			name: 'Администратор'
		},
	} as IUser;

	constructor() {
	}
}
