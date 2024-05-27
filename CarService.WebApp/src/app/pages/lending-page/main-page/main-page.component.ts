import { Component } from '@angular/core';
import {NgForOf, NgOptimizedImage} from '@angular/common'
import {ServicesService} from '../../../services/services/services.service'
import {firstValueFrom} from 'rxjs'
import {RouterLink} from '@angular/router'

@Component({
  selector: 'app-main-page',
  standalone: true,
	imports: [
		NgForOf,
		NgOptimizedImage,
		RouterLink
	],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
	services: any[]

	constructor(private _servicesService: ServicesService) {
		firstValueFrom(_servicesService.GetServicesLending())
		.then((d: any) => this.services = d)
	}
}
