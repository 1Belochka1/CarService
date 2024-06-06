import {Component, inject} from '@angular/core'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {AsyncPipe, JsonPipe, NgForOf} from '@angular/common'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {ServicesService} from '../../../../services/services/services.service'
import {Observable} from 'rxjs'
import {
	CardServiceComponent
} from '../../../../components/card-service/card-service.component'
import {SrcImagePipe} from '../../../../pipe/src-image.pipe'

@Component({
	selector: 'app-services-page',
	standalone: true,
	imports: [
		BTableComponent,
		AsyncPipe,
		BTemplateDirective,
		JsonPipe,
		CardServiceComponent,
		NgForOf,
		SrcImagePipe
	],
	templateUrl: './services-page.component.html',
	styleUrl: './services-page.component.scss'
})
export class ServicesPageComponent {

	items$: Observable<any>

	constructor(private servicesService: ServicesService ) {
		this.items$ = servicesService.GetService()
	}


}
