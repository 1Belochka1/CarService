import {
	AfterContentInit,
	Component, ContentChild,
	ContentChildren,
	Input,
	QueryList
} from '@angular/core'
import {NgIf, NgTemplateOutlet} from '@angular/common'
import {RouterLink} from '@angular/router'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	MatCard,
	MatCardActions,
	MatCardContent, MatCardHeader,
	MatCardTitle
} from '@angular/material/card'

@Component({
	selector: 'app-card-booking',
	standalone: true,
	imports: [
		NgIf,
		RouterLink,
		NgTemplateOutlet,
		BTemplateDirective,
		MatCardActions,
		MatCardContent,
		MatCardTitle,
		MatCardHeader,
		MatCard
	],
	templateUrl: './card-booking.component.html',
	styleUrl: './card-booking.component.scss'
})
export class CardBookingComponent implements AfterContentInit {
	@Input() image?: string | null
	@Input() title?: string
	@Input() description?: string
	@Input() link?: string[]


	@ContentChild(BTemplateDirective)
	templateContent?: BTemplateDirective

	ngAfterContentInit(): void {
	}

}
