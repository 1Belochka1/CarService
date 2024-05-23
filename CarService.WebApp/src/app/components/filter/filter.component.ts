import {Component, Input} from '@angular/core'
import {SelectComponent} from '../select/select.component'
import {TableService} from '../../services/table.service'

@Component({
  selector: 'app-filter',
  standalone: true,
	imports: [
		SelectComponent
	],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.scss'
})
export class FilterComponent {
	@Input()
	service: TableService
}
