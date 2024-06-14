import {
	Component,
	CUSTOM_ELEMENTS_SCHEMA,
	ElementRef,
	OnInit,
	ViewChild
} from '@angular/core'
import {Navigation, Pagination} from 'swiper/modules'
import {SwiperOptions} from 'swiper/types'
import {SwiperContainer} from 'swiper/element'
import {
	FormAddRecordComponent
} from '../form-add-record/form-add-record.component'
import {firstValueFrom} from 'rxjs'
import {
	CalendarRecordService
} from '../../services/calendars/calendar-record.service'
import {SrcImagePipe} from '../../pipe/src-image.pipe'
import {CardBookingComponent} from '../card-booking/card-booking.component'
import {NgForOf} from '@angular/common'

@Component({
	selector: 'app-swiper',
	standalone: true,
	imports: [
		FormAddRecordComponent,
		SrcImagePipe,
		CardBookingComponent,
		NgForOf
	],
	templateUrl: './swiper.component.html',
	styleUrl: './swiper.component.scss',
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SwiperComponent implements OnInit {

	@ViewChild('swiper', {static: true}) swiper: ElementRef<SwiperContainer>

	calendars: any[]

	constructor(private _calendarService: CalendarRecordService,) {
	}

	ngOnInit(): void {
		firstValueFrom(this._calendarService.getAll())
		.then(calendarsRecord => {
			this.calendars = calendarsRecord

			const params: SwiperOptions = {
				autoHeight: true,
				slidesPerView: 1,
				modules: [Navigation, Pagination,],

				injectStylesUrls: [
					'/assets/modules/navigation-element.min.css',
					'/assets/modules/pagination-element.min.css',
				],
				breakpoints: {
					768: {
						// slidesPerView: 3
					}
				}
			}

			Object.assign(this.swiper.nativeElement, params)

			this.swiper.nativeElement.initialize()
		})
	}

	init() {

	}

}
