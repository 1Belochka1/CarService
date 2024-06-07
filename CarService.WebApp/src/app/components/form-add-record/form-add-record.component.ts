import {Component, EventEmitter} from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgForOf, NgIf} from '@angular/common'
import {IItem, SelectComponent} from '../select/select.component'
import {Priority} from '../../enums/priority.enum'
import {RecordsService} from '../../services/records/records.service'
import {firstValueFrom} from 'rxjs'

@Component({
	selector: 'app-form-add-record',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgForOf,
		NgIf,
		SelectComponent
	],
	templateUrl: './form-add-record.component.html',
	styleUrl: './form-add-record.component.scss'
})
export class FormAddRecordComponent {

	submit: EventEmitter<any> = new EventEmitter<any>()

	requestForm: FormGroup

	priorities: IItem<Priority>[] = [
		{
			value: Priority.Низкий,
			name: Priority[Priority.Низкий]
		},
		{
			value: Priority.Средний,
			name: Priority[Priority.Средний]
		},
		{
			value: Priority.Высокий,
			name: Priority[Priority.Высокий]
		},
		{
			value: Priority['Очень высокий'],
			name: Priority[Priority['Очень высокий']]
		},
	]

	constructor(private fb: FormBuilder,
							private _recordService: RecordsService
	) {
		this.requestForm = this.fb.group({
			name: ['', [Validators.minLength(2)]],
			phone: ['', [Validators.pattern(/^\+?\d{10,15}$/)]],
			problemDescription: ['', [Validators.required, Validators.minLength(10)]],
			carDescription: ['', [Validators.minLength(5)]],
		})

		console.log(this.priorities)
	}

	onSubmit() {
		if (this.requestForm.valid) {
			firstValueFrom(this._recordService.create(
				this.requestForm.get('name')?.value,
				this.requestForm.get('phone')?.value,
				this.requestForm.get('problemDescription')?.value,
				this.requestForm.get('carDescription')?.value,
			)).then(() => {
				this.submit.emit()
			})
		}
	}


}
