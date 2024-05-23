import {Component, Input} from '@angular/core'
import {NgClass, NgIf} from '@angular/common'
import {SelectComponent} from '../select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {TableService} from '../../services/table.service'

@Component({
  selector: 'app-sort',
  standalone: true,
	imports: [
		NgIf,
		SelectComponent,
		SvgIconComponent,
		NgClass
	],
  templateUrl: './sort.component.html',
  styleUrl: './sort.component.scss'
})
export class SortComponent {
  @Input()
	service: TableService
}
