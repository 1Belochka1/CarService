import {Component} from '@angular/core'
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field'
import {MatInput} from '@angular/material/input'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'
import {MatButton} from '@angular/material/button'
import {AuthService} from '../../../services/auth.service'

@Component({
	selector: 'app-profile-page',
	standalone: true,
	imports: [
		MatLabel,
		MatError,
		MatInput,
		ReactiveFormsModule,
		MatFormField,
		NgIf,
		MatButton
	],
	templateUrl: './profile-page.component.html',
	styleUrl: './profile-page.component.scss'
})
export class ProfilePageComponent {
	profileForm: FormGroup
	passwordForm: FormGroup

	constructor(private fb: FormBuilder, private _authService: AuthService) {
	}

	ngOnInit() {
		this.profileForm = this.fb.group({
			firstName: ['', Validators.required],
			lastName: ['', Validators.required],
			middleName: [''],
			email: ['', [Validators.required, Validators.email]],
			phone: ['', [Validators.required, Validators.pattern('^\\d{10,12}$')]],
			address: ['']
		})

		this.passwordForm = this.fb.group({
			oldPassword: ['', Validators.required],
			newPassword: ['', [Validators.required, Validators.minLength(6)]],
			confirmPassword: ['', Validators.required]
		}, {validator: this.checkPasswords})

		this._authService.getByCookie().subscribe((user) => {
			this.profileForm.patchValue({
				firstName: user?.firstName,
				lastName: user?.lastName,
				middleName: user?.patronymic,
				email: user?.email,
				phone: user?.phone,
				address: user?.address,
			})
		})
	}

	onSubmit() {
		if (this.profileForm.valid) {
			console.log(this.profileForm.value)
			// Отправка данных профиля на сервер
		}
	}

	onChangePassword() {
		if (this.passwordForm.valid) {
			console.log(this.passwordForm.value)
			// Отправка нового пароля на сервер
		}
	}

	checkPasswords(group: FormGroup) {
		const newPassword = group.get('newPassword')?.value
		const confirmPassword = group.get('confirmPassword')?.value
		return newPassword === confirmPassword ? null : {passwordMismatch: true}
	}
}
