import {Injectable} from '@angular/core'
import {apiUrls} from '../apiUrl'
import {HttpClient} from '@angular/common/http'

@Injectable({
	providedIn: 'root'
})
export class WorkersService {

	constructor(private httpClient: HttpClient) {
	}

	public getWorkers() {
		return this.httpClient.get<any>(apiUrls.users.getWorkers, {
				withCredentials: true
			}
		)
	}

}
