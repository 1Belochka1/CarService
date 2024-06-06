import {Component, TemplateRef} from '@angular/core'
import {Router} from '@angular/router'
import {AsyncPipe, DatePipe} from '@angular/common'
import {BTableComponent} from '../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../direcrives/b-template.directive'
import {BTableSortDirective} from '../../../direcrives/b-table-sort.directive'
import {
	TabsTableContentComponent
} from '../../../components/tabs/tabs-table-content/tabs-table-content.component'
import {Priority} from '../../../enums/priority.enum'
import {Status} from '../../../enums/status.enum'
import {ModalService} from '../../../services/modal.service'
import {
	FormAddRecordComponent
} from '../../../components/form-add-record/form-add-record.component'
import {RecordsService} from '../../../services/records/records.service'
import {RecordType} from '../../../models/record.type'
import {Observable} from 'rxjs'
import {
	TableSortHeaderIconDirective
} from '../../../direcrives/table-sort-header-icon.directive'
import {
	TableRecordsComponent
} from '../../../components/table-records/table-records.component'

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
		FormAddRecordComponent,
		TableSortHeaderIconDirective,
		TableRecordsComponent
	],
	providers: [RecordsService, ModalService],
	templateUrl: './records-page.component.html',
	styleUrl: './records-page.component.scss',
})
export class RecordsPageComponent {

	items: Observable<RecordType[]>

	constructor(private _router: Router, private _modalService: ModalService, private _recordService: RecordsService) {
		this.items = this._recordService.getAll()
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
