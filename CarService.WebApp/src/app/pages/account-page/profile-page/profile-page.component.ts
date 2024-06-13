import {Component} from '@angular/core'
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field'
import {MatInput} from '@angular/material/input'
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule, ValidationErrors, ValidatorFn,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'
import {MatButton} from '@angular/material/button'
import {ToastrService} from "ngx-toastr";
import {firstValueFrom} from "rxjs";
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

	private _id?: string

	constructor(private fb: FormBuilder, private _authService: AuthService, private _toastr: ToastrService) {

	}

	ngOnInit() {
		this.profileForm = this.fb.group({
			firstName: ['', Validators.required],
			lastName: ['', Validators.required],
			middleName: [''],
			email: ['', [Validators.required, Validators.email]],
			phone: ['', [Validators.required,  Validators.pattern('^8\\d{10}$')]],
			address: ['']
		})

		this.passwordForm = this.fb.group({
			oldPassword: ['', Validators.required],
			newPassword: ['', [Validators.required, Validators.minLength(6)]],
			confirmPassword: ['', Validators.required]
		}, {validators: [this.checkPasswords]})

		this._authService.getByCookie().subscribe((user) => {

			this._id = user?.id

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
			firstValueFrom(this._authService.updateUser(
				this._id!,
				this.profileForm.get("email")?.value,
				this.profileForm.get("lastName")?.value,
				this.profileForm.get("firstName")?.value,
				this.profileForm.get("middleName")?.value,
				this.profileForm.get("address")?.value,
				this.profileForm.get("phone")?.value,
			)).then(() => {
				this._toastr.success("Профиль изменен")
			})
		}
	}

	onChangePassword() {
		if (this.passwordForm.valid) {
			console.log(this.passwordForm.value)
			firstValueFrom(this._authService.updatePassword(
				this._id!,
				this.passwordForm.get("newPassword")?.value,
				this.passwordForm.get("oldPassword")?.value,
			)).then(() => {
				this._toastr.success("Пароль изменен")
			})
		}
	}

	checkPasswords: ValidatorFn = (
		group: AbstractControl
	): ValidationErrors | null => {
		const newPassword = group.get('newPassword')?.value
		const confirmPassword = group.get('confirmPassword')?.value
		return newPassword === confirmPassword ? null : {passwordMismatch: true}
	}
}
