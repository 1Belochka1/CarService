import {Component, TemplateRef} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom} from 'rxjs'
import {RecordsService} from '../../../../services/records/records.service'
import {RecordType} from '../../../../models/record.type'
import {DatePipe, JsonPipe, NgIf} from '@angular/common'
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {ModalService} from '../../../../services/modal.service'
import {
	IItem,
	SelectComponent
} from '../../../../components/select/select.component'
import {Priority} from '../../../../enums/priority.enum'
import {Status} from '../../../../enums/status.enum'

@Component({
	selector: 'app-day-record-lending-page',
	standalone: true,
	imports: [
		JsonPipe,
		DatePipe,
		FullNamePipe,
		NgIf,
		SelectComponent,
	],
	templateUrl: './record-page.component.html',
	styleUrl: './record-page.component.scss',
	providers: [ModalService]
})
export class RecordPageComponent {

	priorityItems: IItem<Priority>[] =
		[
			{value: Priority['Низкий'], name: 'Низкий'},
			{value: Priority['Средний'], name: 'Средний'},
			{value: Priority['Высокий'], name: 'Высокий'},
			{
				value: Priority['Очень высокий'],
				name: 'Очень высокий'
			}
		]

	statusItems: IItem<Status>[] =
		[
			{value: Status['Новая'], name: 'Новая'},
			{value: Status['В обработке'], name: 'В обработке'},
			{value: Status['В ожидании'], name: 'В ожидании'},
			{value: Status['В работе'], name: 'В работе'},
			{value: Status['Выполнена'], name: 'Выполнена'}
		]

	record: RecordType

	prioritySelect?: number
	statusSelect?: number

	protected readonly status = Status
	protected readonly priority = Priority

	constructor(
		private _router: ActivatedRoute,
		public recordsService: RecordsService,
		public m: ModalService
	) {

		const id = this._router.snapshot.paramMap.get('id')

		if (id === null) {
			return
		}

		firstValueFrom(this.recordsService.getById(id)).then((data) => {
			this.record = data
			this.prioritySelect = data.priority
			this.statusSelect = data.status
			console.log(this.record)
		})

	}

	dialogOpen(template: TemplateRef<any>, context: any) {
		this.m.open(template, {
			title: 'Приоритет',
			context: context
		})?.subscribe((result) => {

			if (result.isConfirm) {
				firstValueFrom(
					this.recordsService.update(this.record.id, this.prioritySelect, this.statusSelect)
				).then(() => {
					console.log(this.record, this.prioritySelect, this.statusSelect)

					if (this.prioritySelect != undefined) {
						console.log('зашел')
						this.record.priority = this.prioritySelect
					}

					if (this.statusSelect != undefined) {
						console.log('зашел')
						this.record.status = this.statusSelect
					}

					console.log(this.record, this.prioritySelect, this.statusSelect)

				})
			}

			if (result.isCancel) {
				console.log('cancel')
				this.prioritySelect = undefined
				this.statusSelect = undefined
			}
		})
	}

	priorityUpdate(priority: number) {
		console.log(priority)
		this.prioritySelect = priority
	}

	statusUpdate(status: number) {
		this.statusSelect = status
	}
}
