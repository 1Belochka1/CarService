import {Injectable} from '@angular/core'
import {apiUrls} from './apiUrl'
import {HttpClient} from '@angular/common/http'
import {BehaviorSubject, catchError, Observable, of, Subject, tap} from 'rxjs'
import {UserInfo} from '../models/user-info.type'

@Injectable({
	providedIn: 'root'
})
export class AuthService {
	private _roleId: Subject<number> = new Subject()

	constructor(private http: HttpClient) {
		console.log(document.cookie)
	}

	public login(email: string, password: string) {
		return this.http.post(apiUrls.users.login, {
			email,
			password
		}, {withCredentials: true})
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
		return this.http.post(isMaster ? apiUrls.users.registerMaster : apiUrls.users.register, {
			Email: email,
			LastName: lastName,
			FirstName: firstName,
			Patronymic: patronymic,
			Address: address,
			Phone: phone,
			Password: password
		}, {withCredentials: true})
	}


	public getByCookie() {
		return this.http.get<UserInfo | null>(apiUrls.users.getByCookie,
			{withCredentials: true}).pipe(tap(x => {
			if (x) this._roleId.next(x?.userAuth.roleId)
		}))
	}

	public isAuth(): Observable<boolean> {
		return this.http.get<boolean>(apiUrls.users.getIsAuth,
			{withCredentials: true}).pipe(
			catchError(() => of(false))
		)
	}

	public logout() {
		return this.http.get(apiUrls.users.logout,
			{withCredentials: true}).pipe(
			catchError(() => of(false))
		)
	}

	getRoleId() {
		return this._roleId.asObservable()
	}
}
