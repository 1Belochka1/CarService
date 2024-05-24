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

	selectItems: IItem<Priority>[] =
		[
			{value: Priority['Низкий'], name: 'Низкий'},
			{value: Priority['Средний'], name: 'Средний'},
			{value: Priority['Высокий'], name: 'Высокий'},
			{value: Priority['Очень высокий'], name: 'Очень высокий'}
		]

	record: RecordType
	protected readonly Priority = Priority

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
		})

	}

	updateOpen(updatePriority: TemplateRef<any>, context: any) {
		this.m.open(updatePriority, context)
	}
}
