import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core'
import {
	NgClass,
	NgForOf,
	NgOptimizedImage,
	NgTemplateOutlet
} from '@angular/common'
import {ServicesService} from '../../services/services/services.service'
import {firstValueFrom} from 'rxjs'
import {RouterLink, RouterOutlet} from '@angular/router'
import {
	HeaderLendingComponent
} from '../../components/header-lending/header-lending.component'
import {SliderComponent} from '../../components/slider/slider.component'
import {SliderItem} from '../../components/slider/slider.types'
import {
	CalendarRecordService
} from '../../services/calendars/calendar-record.service'
import {BTemplateDirective} from '../../direcrives/b-template.directive'

@Component({
	selector: 'app-lending-page',
	standalone: true,
	imports: [
		NgOptimizedImage,
		NgForOf,
		RouterOutlet,
		RouterLink,
		NgClass,
		HeaderLendingComponent,
		SliderComponent,
		BTemplateDirective,
		NgTemplateOutlet
	],
	templateUrl: './lending-page.component.html',
	styleUrl: './lending-page.component.scss',
})
export class LendingPageComponent implements OnInit {

	calendars: any[] = []

	services: any[]

	@ViewChild('calendar', {static: true})
	calendar: TemplateRef<any>

	@ViewChild('defaultRecord', {static: true})
	defaultRecord: TemplateRef<any>

	constructor(private _servicesService: ServicesService, private _calendarService: CalendarRecordService) {

		firstValueFrom(_servicesService.GetServicesLending())
		.then((d: any) => this.services = d)
	}

	ngOnInit(): void {
		firstValueFrom(this._calendarService.getAll())
		.then(calendarsRecord => {
			this.calendars = calendarsRecord
		})

	}
}
