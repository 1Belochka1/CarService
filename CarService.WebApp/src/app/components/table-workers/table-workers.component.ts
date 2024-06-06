import {Component, EventEmitter, Input, Output} from '@angular/core'
import {AsyncPipe} from '@angular/common'
import {BTableComponent} from '../b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {NotSpecifiedPipe} from '../../pipe/not-specified.pipe'
import {
	TableSortHeaderIconDirective
} from '../../direcrives/table-sort-header-icon.directive'
import {Router} from '@angular/router'

@Component({
	selector: 'app-table-workers',
	standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		NotSpecifiedPipe,
		TableSortHeaderIconDirective
	],
	templateUrl: './table-workers.component.html',
	styleUrl: './table-workers.component.scss'
})
export class TableWorkersComponent {

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value})
	items: any[]

	@Input() addButton: boolean = false

	@Output() addClick: EventEmitter<any> = new EventEmitter<any>()

	constructor(private _router: Router) {
	}

	goToWorker(id: string) {
		this._router.navigate(['account', 'worker', id]).then(() => {
		})
	}

	onAddClick() {
		this.addClick.emit()
	}
}
