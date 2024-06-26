import {NgIf} from '@angular/common'
import {Component, EventEmitter, Input, Output} from '@angular/core'
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms'
import {MatButton} from '@angular/material/button'
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field'
import {MatInput} from '@angular/material/input'
import {ToastrService} from "ngx-toastr";
import {firstValueFrom} from 'rxjs'
import {AuthService} from '../../services/auth.service'
import {passwordConfirmValidator} from "../../validators/password-confirm.validator";

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
	register = new EventEmitter<any>()
	requestForm: FormGroup

	@Input() headerText: string

	@Input() isAddWorker: boolean = false

	constructor(private fb: FormBuilder, private authService: AuthService, private _toastr: ToastrService) {
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
			{validators: [passwordConfirmValidator]}
		)
	}

	addWorker() {
		if (this.requestForm.valid)
			firstValueFrom(
				this.authService.register(
					this.requestForm.get('email')?.value!,
					this.requestForm.get('lastName')?.value!,
					this.requestForm.get('firstName')?.value!,
					this.requestForm.get('patronymic')?.value!,
					this.requestForm.get('address')?.value!,
					this.requestForm.get('phone')?.value!,
					this.requestForm.get('password')?.value!,
					this.isAddWorker
				)
			).then(() => {
				this.register.emit()
				this._toastr.success("Аккаунт зарегистрирован")
			})
	}
}
