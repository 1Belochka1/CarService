import {Component, inject, TemplateRef} from '@angular/core'
import {Router} from '@angular/router'
import {AsyncPipe, DatePipe} from '@angular/common'
import {BTableComponent} from '../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../direcrives/b-template.directive'
import {BTableSortDirective} from '../../../direcrives/b-table-sort.directive'
import {TabsTableContentComponent} from '../../../components/tabs/tabs-table-content/tabs-table-content.component'
import {RecordsTableService} from '../../../services/records/records-table.service'
import {Priority} from '../../../enums/priority.enum'
import {Status} from '../../../enums/status.enum'
import {ModalService} from '../../../services/modal.service'
import {
	FormAddRecordComponent
} from '../../../components/form-add-record/form-add-record.component'

@Component({
	selector: 'app-records-page',
	standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		BTableSortDirective,
		DatePipe,
		TabsTableContentComponent,
		FormAddRecordComponent
	],
	providers: [RecordsTableService, ModalService],
	templateUrl: './records-page.component.html',
	styleUrl: './records-page.component.scss',
})
export class RecordsPageComponent {
	recordsService: RecordsTableService = inject(RecordsTableService)

	protected readonly priority = Priority
	protected readonly status = Status

	constructor(private _router: Router, private _modalService: ModalService) {
		this.recordsService.pageSize = 10
		this.recordsService.method = 'all'
		this.recordsService.update()
	}

	navigateRecord(id: string) {
		this._router.navigate(['account','record', id]).then(
			() => console.log('navigateRecord success')
		)
	}

	addRecord() {

	}

	openModalAddRecord(templateRef: TemplateRef<any>) {
		this._modalService.open(templateRef, {})?.subscribe()
	}
}
