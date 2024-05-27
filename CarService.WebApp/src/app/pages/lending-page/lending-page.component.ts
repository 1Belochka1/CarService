import {Component} from '@angular/core'
import {
	NgClass,
	NgForOf,
	NgOptimizedImage
} from '@angular/common'
import {ServicesService} from '../../services/services/services.service'
import {firstValueFrom} from 'rxjs'
import {RouterLink, RouterOutlet} from '@angular/router'

@Component({
	selector: 'app-lending-page',
	standalone: true,
	imports: [
		NgOptimizedImage,
		NgForOf,
		RouterOutlet,
		RouterLink,
		NgClass
	],
	templateUrl: './lending-page.component.html',
	styleUrl: './lending-page.component.scss'
})
export class LendingPageComponent {

	services: any[]

	constructor(private _servicesService: ServicesService) {
		firstValueFrom(_servicesService.GetServicesLending())
		.then((d: any) => this.services = d)
	}

}
