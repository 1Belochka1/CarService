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
	selector: 'app-form-record',
	standalone: true,
	imports: [
		CustomInputComponent,
		NgIf,
		ReactiveFormsModule
	],
	templateUrl: './form-record.component.html',
})
export class FormRecordComponent {
	@Output()
	submit = new EventEmitter<{
		name: string,
		phone: string,
		problemDescription: string,
		carDescription: string
	}>()

	requestForm: FormGroup

	constructor(private fb: FormBuilder) {
		this.requestForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(3)]],
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]],
			problemDescription: ['', [Validators.required, Validators.minLength(10)]],
			carDescription: ['', [Validators.required, Validators.minLength(5)]]
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			this.submit.emit({
				name: this.requestForm.get('name')?.value!,
				phone: this.requestForm.get('phone')?.value!,
				problemDescription: this.requestForm.get('problemDescription')?.value!,
				carDescription: this.requestForm.get('carDescription')?.value!
			})
		}
	}
}