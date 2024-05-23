import {HttpClient} from '@angular/common/http'
import {inject} from '@angular/core'

export class RequestService {

	httpClient: HttpClient = inject(HttpClient)

	isLoading: boolean = false
	notFound: boolean = false

}