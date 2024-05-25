import {
	Component,
	Injector,
	TemplateRef
} from '@angular/core'
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
import {Dialog} from '@angular/cdk/dialog'

@Component({
	selector: 'app-record-page',
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

	record: RecordType

	priority: number
	status: number

	constructor(
		private dialog: Dialog,
		private injector: Injector,
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
			this.priority = data.priority
			this.status = data.status
		})

	}

	dialogOpen(template: TemplateRef<any>, context: any) {
		this.m.open(template, {
			title: 'Приоритет',
			context: context
		})?.subscribe((result) => {
			if (result.isConfirm)
				firstValueFrom(
					this.recordsService.update(this.record.id, this.priority, this.status)
				).then(() => {
					if (this.priority)
						this.record.priority = this.priority

					if (this.status)
						this.record.status = this.status
				})
		})
	}

	priorityUpdate(priority: number) {
		this.priority = priority
	}

}
