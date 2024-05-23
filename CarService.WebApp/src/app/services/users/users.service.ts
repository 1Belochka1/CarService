import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {BehaviorSubject, Observable, tap} from 'rxjs'
import {apiUrls} from '../apiUrl'
import {GetListWithPageRequest} from '../Requests/GetWorkersParamsRequest'

export interface IWorker {
	id: string
	email: string
	lastName: string
	firstName: string
	patronymic: string
	address: string
	phone: string
	role: number
	works: any[]
	services: any[]
}

export interface IClient {
	id: string
	email: string
	fullName: string
	address: string
	phone: string
	lastRecord: Date
}

@Injectable({
	providedIn: 'root'
})
export class UsersService {

	currentUser: BehaviorSubject<IWorker | null> = new BehaviorSubject<IWorker | null>(null)

	constructor(private http: HttpClient) {
		http.get<IWorker>(apiUrls.users.getByCookie, {
			withCredentials: true,
		}).pipe(tap(
			user => this.currentUser.next(user)
		)).subscribe(
			() => {
				console.log(this.currentUser.value)
			}
		)
	}

	public login(email: string, password: string) {
		return this.http.post(apiUrls.users.login, {
			email,
			password
		}, {withCredentials: true})
	}

	getClients(params: GetListWithPageRequest): Observable<IClient[]> {
		return this.http.get<any>(apiUrls.users.getClients, {
			params: params.value,
			withCredentials: true
		})
	}

	getWorker(id: string): Observable<IWorker> {
		return this.http.get<any>(apiUrls.users.getWorker + id, {withCredentials: true})
	}

	getClient(id: string): Observable<IWorker> {
		return this.http.get<any>(apiUrls.users.getClient + id, {withCredentials: true})
	}


}
