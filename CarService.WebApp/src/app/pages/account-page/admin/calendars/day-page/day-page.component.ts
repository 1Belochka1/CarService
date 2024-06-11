import {Component, EventEmitter, Output, TemplateRef} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {ToastrService} from "ngx-toastr";
import {firstValueFrom, Observable} from 'rxjs'
import {DayRecord} from "../../../../../models/DayRecord.type";
import {TimeRecord} from '../../../../../models/TimeRecord.type'
import {AsyncPipe, DatePipe, JsonPipe, NgForOf, NgIf} from '@angular/common'
import {
	DayRecordService
} from '../../../../../services/day-record/day-record.service'
import {
	BTableComponent
} from '../../../../../components/b-table/b-table.component'
import {
	BTemplateDirective
} from '../../../../../direcrives/b-template.directive'
import {MatIcon} from '@angular/material/icon'
import {MatIconButton} from '@angular/material/button'
import {ModalService} from "../../../../../services/modal.service";

@Component({
	selector: 'app-day-page',
	standalone: true,
	imports: [
		AsyncPipe,
		JsonPipe,
		NgForOf,
		BTableComponent,
		BTemplateDirective,
		MatIcon,
		MatIconButton,
		NgIf,
		DatePipe
	],
	templateUrl: './day-page.component.html',
	styleUrl: './day-page.component.scss',
	providers: [DayRecordService, ModalService]
})
export class DayPageComponent {

	times$: Observable<TimeRecord[]>
	day$: Observable<DayRecord>;

	private readonly _id: string

	constructor(
		private _dayRecordService: DayRecordService,
		private _router: ActivatedRoute,
		private _modalService: ModalService,
		private _toast: ToastrService
	) {
		const id = this._router.snapshot.paramMap.get('dayId')

		if (id === null) {
			return
		}

		this._id = id

		this.setItems()

	}

	setItems() {
		this.times$ = this._dayRecordService.getTimeRecord(this._id)

		this.day$ = this._dayRecordService.getDayRecord(this._id)
	}

	onRemoveClick(temlate: TemplateRef<any>, id: string) {
		this._modalService.open(temlate, {title: "Вы дейстивтельно хотите удалить время?"})
			?.subscribe((isConfirm) => {
				if (isConfirm)
					firstValueFrom(this._dayRecordService.deleteTimeRecord(id))
						.then(() => {
								this._toast.success("Время удалено")
							}
						)
			})

	}
}
