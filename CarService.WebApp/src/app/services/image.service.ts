import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {apiUrls} from './apiUrl'

@Injectable({
	providedIn: 'root'
})
export class ImageService {

	constructor(private httpClient: HttpClient) {
	}

	public createImage(formData: FormData) {
		return this.httpClient.post<string>(apiUrls.images.upload, formData, {withCredentials: true})
	}

	public update(formData: FormData){
		return this.httpClient.post<string>(apiUrls.images.update, formData, {withCredentials: true})
	}

	public delete(formData: FormData) {
		return this.httpClient.post(apiUrls.images.delete, formData, {withCredentials: true})
	}
}
