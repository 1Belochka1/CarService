import {
	AfterContentInit,
	Component,
	ContentChildren,
	Input,
	OnInit,
	QueryList,
	TemplateRef
} from '@angular/core'
import {SliderItem, SliderSettings} from './slider.types'
import {NgClass, NgForOf, NgIf, NgTemplateOutlet} from '@angular/common'
import {SvgIconComponent} from 'angular-svg-icon'
import {BTemplateDirective} from '../../direcrives/b-template.directive'

@Component({
	selector: 'app-slider',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		NgForOf,
		NgIf,
		SvgIconComponent,
		NgClass
	],
	templateUrl: './slider.component.html',
	styleUrl: './slider.component.scss'
})
export class SliderComponent implements OnInit, AfterContentInit {
	@Input()
	templateItem?: TemplateRef<any>

	@Input()
	sliderItems: SliderItem[]

	@Input()
	sliderSettings: SliderSettings

	selectedIndex: number = 0

	@ContentChildren(BTemplateDirective)
	templates: QueryList<BTemplateDirective>

	constructor() {
	}

	ngAfterContentInit(): void {
	}

	ngOnInit(): void {
	}

	getSelectItem() {
		return this.sliderItems[this.selectedIndex]
	}

	sliderNext() {
		if (this.sliderItems.length == this.selectedIndex + 1) {
			return
		}

		this.selectedIndex++
	}

	sliderPrev() {
		if (this.selectedIndex == 0) {
			return
		}

		this.selectedIndex--
	}
}
