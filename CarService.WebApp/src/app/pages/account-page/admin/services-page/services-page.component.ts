import {Component, inject} from '@angular/core'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {AsyncPipe, JsonPipe} from '@angular/common'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {ServicesService} from '../../../../services/services/services.service'

@Component({
	selector: 'app-services-page',
	standalone: true,
	imports: [
		BTableComponent,
		AsyncPipe,
		BTemplateDirective,
		JsonPipe
	],
	templateUrl: './services-page.component.html',
	styleUrl: './services-page.component.scss'
})
export class ServicesPageComponent {
	servicesService: ServicesService = inject(ServicesService)

	constructor() {
		this.servicesService.GetService()
		this.servicesService.update()
	}


}
