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
	}

	public login(email: string, password: string) {
		return this.http.post(apiUrls.users.login, {
			email,
			password
		}, {withCredentials: true})
							 .pipe(tap(() => {
								 firstValueFrom(this.http.get<UserType>(apiUrls.users.getByCookie)).then((data) => this.user = data)
							 }))
	}
}
