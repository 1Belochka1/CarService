import {Injectable} from '@angular/core'
import {apiUrls} from './apiUrl'
import {HttpClient} from '@angular/common/http'
import {UserType} from '../models/user.type'
import {catchError, Observable, of} from 'rxjs'

@Injectable({
	providedIn: 'root'
})
export class AuthService {

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
		lastName: string,
		firstName: string,
		patronymic: string,
		address: string,
		phone: string,
		password: string,
		isMaster: boolean = false
	) {
		return this.http.post(isMaster ? apiUrls.users.registerMaster : apiUrls.users.register, {
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
	}

	public isAuth(): Observable<boolean> {
		return this.http.get<boolean>(apiUrls.users.getIsAuth,
			{withCredentials: true}).pipe(
			catchError(() => of(false))
		)
	}

	public logout(): void {
	}

}
