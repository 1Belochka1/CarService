import {Component} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {Observable} from 'rxjs'
import {TimeRecord} from '../../../../../models/TimeRecord.type'
import {AsyncPipe, JsonPipe, NgForOf} from '@angular/common'
import {
	DayRecordService
} from '../../../../../services/day-record/day-record.service'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'

@Component({
	selector: 'app-day-page',
	standalone: true,
	imports: [
		AsyncPipe,
		JsonPipe,
		NgForOf,
		BTableComponent,
		BTemplateDirective
	],
	templateUrl: './day-page.component.html',
	styleUrl: './day-page.component.scss',
	providers: [DayRecordService]
})
export class DayPageComponent {

	times$: Observable<TimeRecord[]>

	constructor(
		private _dayRecordService: DayRecordService,
		private _router: ActivatedRoute,
	) {
		const id = this._router.snapshot.paramMap.get('dayId')

		if (id === null) {
			return
		}

		this.times$ = _dayRecordService.getTimeRecord(id)
	}
}
