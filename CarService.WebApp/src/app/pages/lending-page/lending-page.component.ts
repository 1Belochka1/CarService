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
import {
	CalendarRecordService
} from '../../services/calendars/calendar-record.service'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	StepperLendingRecordComponent
} from '../../components/stepper-lending-record/stepper-lending-record.component'
import {CdkStep} from '@angular/cdk/stepper'
import {
	FormRecordComponent
} from '../../components/form-record/form-record.component'
import {apiUrls} from '../../services/apiUrl'
import {
	CardLendingComponent
} from '../../components/card-lending/card-lending.component'
import {
	CardBookingComponent
} from '../../components/card-booking/card-booking.component'
import {RecordsService} from '../../services/records/records.service'
import {SrcImagePipe} from '../../pipe/src-image.pipe'

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
		FormRecordComponent,
		NgIf,
		JsonPipe,
		CardLendingComponent,
		CardBookingComponent,
		SrcImagePipe,
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
	protected readonly apiUrls = apiUrls

	constructor(private _servicesService: ServicesService, private _calendarService: CalendarRecordService, private _recordService: RecordsService) {
		firstValueFrom(_servicesService.getServicesLending())
		.then((d: any) => this.services = d)
	}

	ngOnInit(): void {
		firstValueFrom(this._calendarService.getAll())
		.then(calendarsRecord => {
			this.calendars = calendarsRecord
		})

	}

	createRecord($event: {
		name: string;
		phone: string;
		problemDescription: string;
		carDescription: string
	}) {
		firstValueFrom(this._recordService.create($event.name, $event.phone, $event.problemDescription, $event.carDescription))
		.then(() => console.log('create'))
	}
}
