import {HttpClient} from '@angular/common/http'
import {Injectable} from '@angular/core'
import {BehaviorSubject, catchError, firstValueFrom, map, of, Subject, tap} from 'rxjs'
import {UserInfo} from '../models/user-info.type'
import {apiUrls} from './apiUrl'

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
		const req = {
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
			req,
			{
				withCredentials: true
			}
		)
	}

	public getByCookie() {
		return this.http.get<UserInfo | null>(apiUrls.users.getByCookie, {
			withCredentials: true,
		})
	}

	public logout() {
		return this.http
			.get(apiUrls.users.logout, {withCredentials: true})
			.pipe(catchError(() => of(false)))
	}

	public getRoleId() {
		return this.http
			.get<number>(apiUrls.users.getRoleId, {withCredentials: true})
			.pipe(map(roleId => (roleId == 0 ? 1 : roleId)))
			.pipe(tap(roleId => this._roleId.next(roleId)))
	}

	public getRoleId$() {
		return this._roleId.asObservable().pipe(map(roleId => (roleId == 0 ? 1 : roleId)))
	}
}
