import {Component, inject} from '@angular/core'
import {Router} from '@angular/router'
import {AsyncPipe, DatePipe} from '@angular/common'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {BTableSortDirective} from '../../../../direcrives/b-table-sort.directive'
import {TabsTableContentComponent} from '../../../../components/tabs/tabs-table-content/tabs-table-content.component'
import {RecordsTableService} from '../../../../services/records/records-table.service'
import { Priority } from '../../../../enums/priority.enum'
import {Status} from '../../../../enums/status.enum'

@Component({
  selector: 'app-requests-page',
  standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		BTableSortDirective,
		DatePipe,
		TabsTableContentComponent
	],
	providers: [RecordsTableService],
  templateUrl: './requests-page.component.html',
  styleUrl: './requests-page.component.scss'
})
export class RequestsPageComponent {
	recordsService: RecordsTableService = inject(RecordsTableService)

	constructor(private _router: Router) {
		this.recordsService.pageSize = 10
		this.recordsService.method = "all"
		this.recordsService.update()
	}

	protected readonly priority = Priority
	protected readonly status = Status
}
