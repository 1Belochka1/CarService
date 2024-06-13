import {Component, EventEmitter, Output} from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'
import {MatButton} from "@angular/material/button";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

@Component({
	selector: 'app-form-time-record',
	standalone: true,
	imports: [
		NgIf,
		ReactiveFormsModule,
		MatButton,
		MatError,
		MatInput,
		MatLabel,
		MatFormField
	],
	templateUrl: './form-time-record.component.html',
})
export class FormTimeRecordComponent {
	@Output()
	submit = new EventEmitter<{
		name: string,
		email: string,
		phone: string
	}>()

	requestForm: FormGroup

	constructor(private fb: FormBuilder) {
		this.requestForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(3)]],
			email: ['', [Validators.required, Validators.email]],
			phone: ['', [Validators.required, Validators.pattern('^8\\d{10}$')]],
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			this.submit.emit({
				name: this.requestForm.get('name')?.value!,
				email: this.requestForm.get('email')?.value!,
				phone: this.requestForm.get('phone')?.value!
			})
		}
	}
}
