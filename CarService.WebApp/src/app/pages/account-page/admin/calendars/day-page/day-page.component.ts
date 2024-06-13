import {AsyncPipe, DatePipe, JsonPipe, NgForOf, NgIf} from '@angular/common'
import {Component, TemplateRef} from '@angular/core'
import {MatIconButton} from '@angular/material/button'
import {MatIcon} from '@angular/material/icon'
import {ActivatedRoute} from '@angular/router'
import {ToastrService} from 'ngx-toastr'
import {Observable, firstValueFrom} from 'rxjs'
import {BTableComponent} from '../../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../../direcrives/b-template.directive'
import {DayRecord} from '../../../../../models/DayRecord.type'
import {TimeRecord} from '../../../../../models/TimeRecord.type'
import {AuthService} from "../../../../../services/auth.service";
import {DayRecordService} from '../../../../../services/day-record/day-record.service'
import {ModalService} from '../../../../../services/modal.service'

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
		DatePipe,
	],
	templateUrl: './day-page.component.html',
	styleUrl: './day-page.component.scss',
	providers: [DayRecordService, ModalService],
})
export class DayPageComponent {
	times$: Observable<TimeRecord[]>
	day$: Observable<DayRecord>

	private readonly _id: string

	roleId$: Observable<number>;

	constructor(
		private _dayRecordService: DayRecordService,
		private _router: ActivatedRoute,
		private _modalService: ModalService,
		private _toast: ToastrService,
		private _authService: AuthService
	) {
		const id = this._router.snapshot.paramMap.get('dayId')

		if (id === null) {
			return
		}

		this._id = id

		this.roleId$ = this._authService.getRoleId$()


		this.setItems()
	}

	setItems() {
		this.times$ = this._dayRecordService.getTimeRecord(this._id)

		this.day$ = this._dayRecordService.getDayRecord(this._id)
	}

	onRemoveClick(temlate: TemplateRef<any>, id: string) {
		this._modalService
			.open(temlate, {title: 'Вы действительно хотите удалить время?'})
			?.subscribe(isConfirm => {
				console.log(isConfirm)
				if (isConfirm)
					firstValueFrom(this._dayRecordService.deleteTimeRecord(id)).then(
						() => {
							this._toast.success('Время удалено')
							this.setItems()
						}
					)
			})
	}
}
