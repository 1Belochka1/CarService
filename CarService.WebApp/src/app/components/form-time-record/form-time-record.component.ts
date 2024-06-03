import {Component, EventEmitter, Output} from '@angular/core'
import {CustomInputComponent} from '../custom-input/custom-input.component'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'

@Component({
	selector: 'app-form-time-record',
	standalone: true,
	imports: [
		CustomInputComponent,
		NgIf,
		ReactiveFormsModule
	],
	templateUrl: './form-time-record.component.html',
	styleUrl: './form-time-record.component.scss'
})
export class FormTimeRecordComponent {
	@Output()
	submit: EventEmitter<{name: string, phone: string}> = new EventEmitter<{name: string, phone: string}>()

	requestForm: FormGroup

	constructor(private fb: FormBuilder) {
		this.requestForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(3)]],
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]]
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			this.submit.emit({name: this.requestForm.get('name')?.value!, phone: this.requestForm.get('phone')?.value!})
		}
	}
}
