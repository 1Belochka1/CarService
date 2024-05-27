import {Injectable} from '@angular/core'
import {apiUrls} from './apiUrl'
import {HttpClient} from '@angular/common/http'
import {UserType} from '../models/user.type'
import {firstValueFrom, tap} from 'rxjs'

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	user?: UserType

	constructor(private http: HttpClient) {
		this.getByCookie()
	}

	public login(email: string, password: string) {
		return this.http.post(apiUrls.users.login, {
			email,
			password
		}, {withCredentials: true})
							 .pipe(tap(() => {
								 this.getByCookie()
							 }))
	}

	public register(email: string,
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
		firstValueFrom(
			this.http.get<UserType>(apiUrls.users.getByCookie, {withCredentials: true}))
		.then((data) => this.user = data)
	}
}
