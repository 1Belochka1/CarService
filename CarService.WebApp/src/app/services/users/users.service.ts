import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {BehaviorSubject, Observable} from 'rxjs'
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
	lastName: string
	firstName: string
	patronymic: string
	address: string
	phone: string
	lastRecord: Date
	isAuth: boolean
}

@Injectable({
	providedIn: 'root'
})
export class UsersService {

	currentUser: BehaviorSubject<IWorker | null> = new BehaviorSubject<IWorker | null>(null)

	constructor(private http: HttpClient) {

	}


	getClients(): Observable<IClient[]> {
		return this.http.get<IClient[]>(apiUrls.users.getClients, {
			withCredentials: true
		})
	}

	public getWorkers() {
		return this.http.get<any>(apiUrls.users.getWorkers, {
				withCredentials: true
			}
		)
	}

	getWorker(id: string): Observable<IWorker> {
		return this.http.get<any>(apiUrls.users.getWorker + id, {withCredentials: true})
	}

	getClient(id: string): Observable<IWorker> {
		return this.http.get<any>(apiUrls.users.getClient + id, {withCredentials: true})
	}

	updateByPhone(phone: string) {
		return this.http.post(apiUrls.users.updateByPhone, {phone}, {withCredentials: true})
	}

	dismissById(id: string) {
		return this.http.delete(apiUrls.users.dismissById + id, {withCredentials: true})
	}

	delete(id: string) {
		return this.http.delete(apiUrls.users.delete + id, {withCredentials: true})
	}

	getMasterAutocomplete() {
		return this.http.get<any[]>(apiUrls.users.getWorkersAutocomplete, {withCredentials: true})
	}
}
