import {Component, Input} from '@angular/core'
import {NgClass, NgIf} from '@angular/common'

@Component({
  selector: 'app-card-service',
  standalone: true,
	imports: [
		NgIf,
		NgClass
	],
  templateUrl: './card-service.component.html',
  styleUrl: './card-service.component.scss'
})
export class CardServiceComponent {
	@Input() image?: string;
	@Input() title!: string;
	@Input() description!: string;
}
