import {Component, EventEmitter, Input, Output} from '@angular/core'
import {Priority} from '../../enums/priority.enum'
import {Status} from '../../enums/status.enum'
import {AsyncPipe, DatePipe} from '@angular/common'
import {BTableComponent} from '../b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	TableSortHeaderIconDirective
} from '../../direcrives/table-sort-header-icon.directive'
import {Router} from '@angular/router'

@Component({
  selector: 'app-table-records',
  standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		DatePipe,
		TableSortHeaderIconDirective
	],
  templateUrl: './table-records.component.html',
  styleUrl: './table-records.component.scss'
})
export class TableRecordsComponent {

	protected readonly priority = Priority
	protected readonly status = Status

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value})
	items: any[]

	@Input() addButton: boolean = false

	@Output() addClick: EventEmitter<any> = new EventEmitter<any>()

	constructor(private _router: Router) {
	}

	navigateRecord(id: string) {
		this._router.navigate(['account', 'record', id]).then(() => {
		})
	}

	onAddClick() {
		this.addClick.emit()
	}
}
