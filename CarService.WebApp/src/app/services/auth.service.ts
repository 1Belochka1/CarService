import {Injectable} from '@angular/core'
import {apiUrls} from './apiUrl'
import {HttpClient} from '@angular/common/http'
import {UserType} from '../models/user.type'
import {BehaviorSubject, tap} from 'rxjs'

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	user?: UserType

	isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)

	constructor(private http: HttpClient) {
	}

	public login(email: string, password: string) {
		return this.http.post(apiUrls.users.login, {
			email,
			password
		}, {withCredentials: true})
							 .pipe(tap(() => {
								 this.setLoggedIn(true)
							 }))
	}

	public register(
		email: string,
		lastName: string,
		firstName: string,
		patronymic: string,
		address: string,
		phone: string,
		password: string
	) {
		return this.http.post(apiUrls.users.register, {
			email,
			lastName,
			firstName,
			patronymic,
			address,
			phone,
			password
		}, {withCredentials: true})
	}

	public getByCookie() {
		return this.http.get<UserType>(apiUrls.users.getByCookie,
			{withCredentials: true})
							 .pipe(tap((user) => {
									 this.setLoggedIn(true)
								 })
							 )
	}

	public logout(): void {
		this.setLoggedIn(false)
	}

	public getLoggedIn() {
		return this.isLoggedIn.asObservable()
	}

	public setLoggedIn(value: boolean) {
		this.isLoggedIn.next(value)
	}
}
