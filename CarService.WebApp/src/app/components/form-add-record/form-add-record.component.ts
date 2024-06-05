import {Component} from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgForOf, NgIf} from '@angular/common'

@Component({
	selector: 'app-form-add-record',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgForOf,
		NgIf
	],
	templateUrl: './form-add-record.component.html',
	styleUrl: './form-add-record.component.scss'
})
export class FormAddRecordComponent {
	requestForm: FormGroup
	priorities = ['Высокий', 'Средний', 'Низкий']

	constructor(private fb: FormBuilder) {
		this.requestForm = this.fb.group({
			name: ['', [Validators.minLength(2)]],
			phone: ['', [Validators.pattern(/^\+?\d{10,15}$/)]],
			problemDescription: ['', [Validators.required, Validators.minLength(10)]],
			carDescription: ['', [Validators.required, Validators.minLength(5)]],
			priority: ['', [Validators.required]]
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			console.log(this.requestForm.value)
		}
	}
}
