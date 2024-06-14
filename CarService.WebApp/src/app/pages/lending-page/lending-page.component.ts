import {
	Component,
	CUSTOM_ELEMENTS_SCHEMA,
	OnInit,
	TemplateRef,
	ViewChild
} from '@angular/core'
import {
	JsonPipe,
	NgClass,
	NgForOf,
	NgIf,
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
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	StepperLendingRecordComponent
} from '../../components/stepper-lending-record/stepper-lending-record.component'
import {CdkStep} from '@angular/cdk/stepper'
import {apiUrls} from '../../services/apiUrl'
import {
	CardLendingComponent
} from '../../components/card-lending/card-lending.component'
import {
	CardBookingComponent
} from '../../components/card-booking/card-booking.component'
import {RecordsService} from '../../services/records/records.service'
import {SrcImagePipe} from '../../pipe/src-image.pipe'
import {
	FormAddRecordComponent
} from '../../components/form-add-record/form-add-record.component'
import {SwiperComponent} from '../../components/swiper/swiper.component'
import {
	MatCard,
	MatCardActions,
	MatCardContent,
	MatCardHeader,
	MatCardImage,
	MatCardTitle
} from '@angular/material/card'
import {MatButton} from '@angular/material/button'
import {
	CalendarRecordService
} from '../../services/calendars/calendar-record.service'

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
		NgTemplateOutlet,
		StepperLendingRecordComponent,
		CdkStep,
		NgIf,
		JsonPipe,
		CardLendingComponent,
		CardBookingComponent,
		SrcImagePipe,
		FormAddRecordComponent,
		SwiperComponent,
		MatCard,
		MatCardActions,
		MatCardContent,
		MatCardHeader,
		MatCardTitle,
		MatCardImage,
		MatButton,
	],
	templateUrl: './lending-page.component.html',
	styleUrl: './lending-page.component.scss',
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LendingPageComponent implements OnInit {

	calendars: any[] = []

	services: any[]

	@ViewChild('calendar', {static: true})
	calendar: TemplateRef<any>

	@ViewChild('defaultRecord', {static: true})
	defaultRecord: TemplateRef<any>
	hoveredService: any = null
	protected readonly apiUrls = apiUrls

	constructor(private _servicesService: ServicesService,
							private _recordService: RecordsService,
							private _calendarService: CalendarRecordService
	) {
		firstValueFrom(_servicesService.getServicesLending())
		.then((d: any) => this.services = d)
		firstValueFrom(this._calendarService.getAll())
		.then((calendarsRecord: any) => {
			this.calendars = calendarsRecord
		})
	}

	ngOnInit(): void {
	}

	hover(service: any) {
		this.hoveredService = service
	}

	unhover(service: any) {
		if (this.hoveredService === service) {
			this.hoveredService = null
		}
	}

	isHovered(service: any): boolean {
		return this.hoveredService === service
	}
}
