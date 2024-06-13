import {NgIf} from "@angular/common";
import {Component, EventEmitter, Input, Output} from '@angular/core';
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	ValidationErrors,
	ValidatorFn,
	Validators
} from "@angular/forms";
import {MatButton} from "@angular/material/button";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {ToastrService} from "ngx-toastr";
import {firstValueFrom} from "rxjs";
import {UserInfo} from "../../models/user-info.type";
import {AuthService} from "../../services/auth.service";
import {passwordConfirmValidator} from "../../validators/password-confirm.validator";

@Component({
	selector: 'app-form-client-update',
	standalone: true,
	imports: [
		MatButton,
		MatError,
		MatFormField,
		MatInput,
		MatLabel,
		NgIf,
		ReactiveFormsModule
	],
	templateUrl: './form-client-update.component.html',
	styleUrl: './form-client-update.component.scss'
})
export class FormClientUpdateComponent {
	@Output()
	register = new EventEmitter<any>()

	@Input() headerText: string

	@Input() client: any

	requestForm: FormGroup


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

	patchValue() {

	}

	addWorker() {
		if (this.requestForm.valid)
			firstValueFrom(
				this.authService.register(
					this.requestForm.get('email')?.value!,
					this.requestForm.get('firstName')?.value!,
					this.requestForm.get('lastName')?.value!,
					this.requestForm.get('patronymic')?.value!,
					this.requestForm.get('address')?.value!,
					this.requestForm.get('phone')?.value!,
					this.requestForm.get('password')?.value!,
					true
				)
			).then(() => {
				this.register.emit()
				this._toastr.success("Аккаунт зарегистрирован")
			})
	}
}
