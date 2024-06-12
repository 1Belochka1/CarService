import {NgClass, NgIf} from '@angular/common'
import {Component, ViewEncapsulation} from '@angular/core'
import {
	FormBuilder,
	FormControl,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {firstValueFrom} from 'rxjs'
import {register} from "swiper/element";
import {AuthService} from '../../services/auth.service'
import {Router} from '@angular/router'
import {
	FormRegisterComponent
} from '../../components/form-register/form-register.component'
import {ToastrService} from 'ngx-toastr'
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field'
import {MatInput} from '@angular/material/input'
import {MatButton} from '@angular/material/button'

@Component({
	selector: 'app-auth-page',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgClass,
		NgIf,
		FormRegisterComponent,
		MatFormField,
		MatInput,
		MatLabel,
		MatError,
		MatButton
	],
	templateUrl: './auth-page.component.html',
	styleUrl: './auth-page.component.scss',
})
export class AuthPageComponent {

	formLogin = this._formBuilder.group(
		{
			email: new FormControl('', [Validators.required, Validators.email]),
			password: new FormControl('', [Validators.required]),
		},
		{
			updateOn: 'blur'
		}
	)

	activePage: 1 | 2 = 1

	constructor(private _formBuilder: FormBuilder, private _authService: AuthService, private _router: Router, private _toastr: ToastrService) {
	}


	login() {
		firstValueFrom(
			this._authService
					.login(this.formLogin.value.email!, this.formLogin.value.password!))
		.then(() => {
				this._toastr.success('Выполнен вход')
				this._router.navigate(['account'])
			}
		)

	}

	registerSub() {
		this.activePage = 1
	}
}
