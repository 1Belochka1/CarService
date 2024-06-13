import {HttpClient} from '@angular/common/http'
import {Injectable} from '@angular/core'
import {BehaviorSubject, catchError, firstValueFrom, map, of, tap} from 'rxjs'
import {UserInfo} from '../models/user-info.type'
import {apiUrls} from './apiUrl'

interface IRegister {
	Email: string,
	LastName: string,
	FirstName: string,
	Patronymic?: string,
	Address?: string,
	Phone: string,
	Password: string
}


@Injectable({
	providedIn: 'root',
})
export class AuthService {
	private _roleId: BehaviorSubject<number> = new BehaviorSubject(0)

	constructor(private http: HttpClient) {
		firstValueFrom(this.getRoleId()).then()
	}

	public login(email: string, password: string) {
		return this.http.post(
			apiUrls.users.login,
			{
				email,
				password,
			},
			{withCredentials: true}
		)
	}

	public register(
		email: string,
		lastName: string,
		firstName: string,
		patronymic: string,
		address: string,
		phone: string,
		password: string,
		isMaster: boolean = false
	) {
		const req: IRegister = {
			Email: email,
			LastName: lastName,
			FirstName: firstName,
			Patronymic: patronymic,
			Address: address,
			Phone: phone,
			Password: password,
		}

		return this.http.post(
			isMaster ? apiUrls.users.registerMaster : apiUrls.users.register,
			{
				email,
				lastName,
				firstName,
				patronymic,
				address,
				phone,
				password,
			},
			{withCredentials: true}
		)
	}

	updateUser(
		id: string,
		email: string,
		lastName: string,
		firstName: string,
		patronymic: string,
		address: string,
		phone: string
	) {
		console.log(id,
			email,
			lastName,
			firstName,
			patronymic,
			address,
			phone)
		return this.http.post(apiUrls.users.update, {
			id,
			email,
			firstName,
			LastName: lastName,
			patronymic,
			address,
			phone
		}, {
			withCredentials: true
		})
	}

	updatePassword(id: string, newPassword: string, oldPassword: string) {
		return this.http.post(apiUrls.users.updatePassword, {
			id, oldPassword, newPassword
		}, {
			withCredentials: true
		})
	}

	getByCookie() {
		return this.http.get<UserInfo | null>(apiUrls.users.getByCookie, {
			withCredentials: true,
		})
	}


	logout() {
		return this.http
			.get(apiUrls.users.logout, {withCredentials: true})
			.pipe(catchError(() => of(false)))
	}


	getRoleId() {
		return this.http
			.get<number>(apiUrls.users.getRoleId, {withCredentials: true})
			.pipe(map(roleId => (roleId == 0 ? 1 : roleId)))
			.pipe(tap(roleId => this._roleId.next(roleId)))
	}


	getRoleId$() {
		return this._roleId
			.asObservable()
			.pipe(map(roleId => (roleId == 0 ? 1 : roleId)))
	}
}
