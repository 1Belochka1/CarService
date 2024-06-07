import {inject, Injectable} from '@angular/core'
import {apiUrls} from '../apiUrl'
import {HttpClient} from '@angular/common/http'

@Injectable({
	providedIn: 'root'
})
export class ServicesService {

	private _httpClient: HttpClient = inject(HttpClient)

	constructor() {
	}

	public getServicesLending() {
		return this._httpClient.get(apiUrls.services.getLending)
	}

	public getServices() {
		return this._httpClient.get<any>(apiUrls.services.getAll, {
			withCredentials: true,
		})
	}

	public getServiceById(id: string) {
		return this._httpClient.get(apiUrls.services.getById + id, {withCredentials: true})
	}

	public create(formData: FormData) {
		return this._httpClient.post(apiUrls.services.create, formData, {withCredentials: true})
	}

	public update(id: string, name?: string, description?: string, isShowLending?: boolean) {
		return this._httpClient.post(apiUrls.services.update, {
			id,
			name,
			description,
			isShowLending
		}, {withCredentials: true})
	}

	public delete(id: string) {
		return this._httpClient.delete(apiUrls.services.delete + id, {withCredentials: true})
	}

	public getForAutocomplete() {
		return this._httpClient.get<any[]>(apiUrls.services.getServicesAutocomplete, {withCredentials: true})
	}

}
