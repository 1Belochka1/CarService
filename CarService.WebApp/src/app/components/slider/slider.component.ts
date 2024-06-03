import {
	Component,
	CUSTOM_ELEMENTS_SCHEMA,
	ElementRef,
	Input,
	OnInit,
	TemplateRef,
	ViewChild,
	ViewEncapsulation
} from '@angular/core'
import {NgClass, NgForOf, NgIf, NgTemplateOutlet} from '@angular/common'
import {SwiperContainer} from 'swiper/element'
import {Navigation, Pagination} from 'swiper/modules'
import {SwiperOptions} from 'swiper/types'
import {SvgIconComponent} from 'angular-svg-icon'

@Component({
	selector: 'app-slider',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		NgForOf,
		NgIf,
		NgClass,
		SvgIconComponent
	],
	templateUrl: './slider.component.html',
	styleUrl: './slider.component.scss',
	providers: [],
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SliderComponent implements OnInit {

	@Input()
	slides: { template: TemplateRef<any>, context: string }[] = []

	currentIndex = 0
	@ViewChild('swiper', {static: true})
	carousel: ElementRef<SwiperContainer>

	ngOnInit(): void {
		this.carousel.nativeElement.injectStylesUrls = [
			'swiper/modules/navigation-element.min.css'
		]

		const params: SwiperOptions = {
			modules: [Navigation, Pagination],
			breakpoints: {
				768: {
					slidesPerView: 1,
				},
				1280: {
					slidesPerView: 3,
					centeredSlides: false
				},
			},
			injectStylesUrls: [
				'assets/modules/navigation-element.min.css',
				'assets/modules/pagination-element.min.css',
			],
			// navigation: {
			// 	prevEl: '.swiper-btn-prev',
			// 	nextEl: '.swiper-btn-next',
			// }
		}

		Object.assign(this.carousel.nativeElement, params)

		this.carousel.nativeElement.initialize()
	}

}
