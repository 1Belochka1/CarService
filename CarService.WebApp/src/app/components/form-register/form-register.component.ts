import {Component, EventEmitter, Output} from '@angular/core'
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	ValidationErrors,
	ValidatorFn,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'

@Component({
	selector: 'app-form-register',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgIf
	],
	templateUrl: './form-register.component.html',
})
export class FormRegisterComponent {

	@Output()
	submit = new EventEmitter<{
		phone: string,
		firstName: string,
		lastName: string,
		patronymic: string,
		address: string,
		password: string
	}>()

	requestForm: FormGroup

	constructor(private fb: FormBuilder) {
		this.requestForm = this.fb.group({
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]],
			firstName: ['', [Validators.required, Validators.minLength(2)]],
			lastName: [''],
			patronymic: [''],
			address: [''],
			password: ['', [Validators.required, Validators.minLength(6)]],
			confirmPassword: ['', [Validators.required]]
		}, {validators: [this.passwordConfirmValidator]})


	}

	passwordConfirmValidator: ValidatorFn = (
		group: AbstractControl
	): ValidationErrors | null => {
		const password = group.get('password')
		const confirmPassword = group.get('confirmPassword')

		if (password == null || confirmPassword == null) return null

		if (password.value == '' || confirmPassword.value == '') return null

		if (password?.value === confirmPassword?.value) {
			password.setErrors(null)
			confirmPassword.setErrors(null)
			return null
		}

		if (password?.errors || confirmPassword?.errors) return null

		const error = {noConfirmPassword: true}

		password.setErrors(error)

		confirmPassword.setErrors(error)

		return error
	}

	onSubmit() {
		if (this.requestForm.valid) {
			this.submit.emit({
				phone: this.requestForm.get('phone')?.value!,
				firstName: this.requestForm.get('firstName')?.value!,
				lastName: this.requestForm.get('lastName')?.value!,
				patronymic: this.requestForm.get('patronymic')?.value!,
				address: this.requestForm.get('address')?.value!,
				password: this.requestForm.get('password')?.value!
			})
		}
	}
}
