import { NgIf } from '@angular/common'
import { Component, EventEmitter, Input, Output } from '@angular/core'
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms'
import { MatButton } from '@angular/material/button'
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field'
import { MatInput } from '@angular/material/input'
import { firstValueFrom } from 'rxjs'
import { AuthService } from '../../services/auth.service'

@Component({
	selector: 'app-form-register',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgIf,
		MatFormField,
		MatInput,
		MatButton,
		MatLabel,
		MatError,
	],
	templateUrl: './form-register.component.html',
	styleUrl: './form-register.component.scss',
})
export class FormRegisterComponent {
	@Output()
	submit = new EventEmitter<any>()
	requestForm: FormGroup

	@Input() title: string

	@Input() isAddWorker: boolean = false

	constructor(private fb: FormBuilder, private authService: AuthService) {
		this.requestForm = this.fb.group(
			{
				email: ['', [Validators.required, Validators.email]],
				phone: ['', [Validators.required, Validators.pattern('^8\\d{10}$')]],
				firstName: ['', [Validators.required, Validators.minLength(2)]],
				lastName: ['', [Validators.required, Validators.minLength(2)]],
				patronymic: [''],
				address: [''],
				password: ['', [Validators.required, Validators.minLength(6)]],
				confirmPassword: ['', [Validators.required]],
			},
			{ validators: [this.passwordConfirmValidator] }
		)
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

		const error = { noConfirmPassword: true }

		password.setErrors(error)

		confirmPassword.setErrors(error)

		return error
	}

	addWorker() {
		firstValueFrom(
			this.authService.register(
				this.requestForm.get('email')?.value!,
				this.requestForm.get('firstName')?.value!,
				this.requestForm.get('lastName')?.value!,
				this.requestForm.get('patronymic')?.value!,
				this.requestForm.get('address')?.value!,
				this.requestForm.get('phone')?.value!,
				this.requestForm.get('password')?.value!,
				this.isAddWorker
			)
		).then(() => {
			this.submit.emit()
		})
	}
}
