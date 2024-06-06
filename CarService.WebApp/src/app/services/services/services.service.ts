import {inject, Injectable} from '@angular/core'
import {apiUrls} from '../apiUrl'
import {HttpClient} from '@angular/common/http'

@Injectable({
	providedIn: 'root'
})
export class ServicesService {

	httpClient: HttpClient = inject(HttpClient)

	masterId: string

	constructor() {
	}

	public GetServicesLending() {
		return this.httpClient.get(apiUrls.services.getLending)
	}

	public GetService() {
		return this.httpClient.get<any>(apiUrls.services.get, {
			withCredentials: true,
		})
	}


}
