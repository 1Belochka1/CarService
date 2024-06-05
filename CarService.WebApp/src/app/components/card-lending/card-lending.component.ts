import {Component, Input} from '@angular/core'
import {NgClass, NgIf} from '@angular/common'

@Component({
  selector: 'app-card-lending',
  standalone: true,
	imports: [
		NgClass,
		NgIf
	],
  templateUrl: './card-lending.component.html',
  styleUrl: './card-lending.component.scss'
})
export class CardLendingComponent {
	@Input() image?: string;
	@Input() title!: string;
	@Input() description!: string;
}
