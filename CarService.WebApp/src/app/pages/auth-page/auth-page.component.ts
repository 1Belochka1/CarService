import {NgClass, NgIf} from '@angular/common'
import {Component} from '@angular/core'
import {
	FormBuilder,
	FormControl,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {firstValueFrom} from 'rxjs'
import {FormInputComponent} from '../../components/form-input/form-input.component'
import {AuthService} from '../../services/auth.service'
import {Router} from '@angular/router'
import {CustomInputComponent} from '../../components/custom-input/custom-input.component'

@Component({
	selector: 'app-auth-page',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgClass,
		NgIf,
		FormInputComponent,
		CustomInputComponent
	],
	templateUrl: './auth-page.component.html',
	styleUrl: './auth-page.component.scss'
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

	formRegister = this._formBuilder.group(
		{
			email: new FormControl('', [Validators.required, Validators.email]),
			lastName: new FormControl('', [Validators.required]),
			firstName: new FormControl('', [Validators.required]),
			patronymic: new FormControl('', [Validators.required]),
			address: new FormControl('', [Validators.required]),
			phone: new FormControl('', [Validators.minLength(11), Validators.maxLength(11)]),
			password: new FormControl('', [Validators.minLength(11), Validators.maxLength(11)])
		}
	)

	activePage: 1 | 2 = 1

	constructor(private _formBuilder: FormBuilder, private _authService: AuthService, private _router: Router) {
	}

	authChanged(event: any) {
		this.activePage = event.target.value
	}

	login() {
		firstValueFrom(
			this._authService
					.login(this.formLogin.value.email!, this.formLogin.value.password!))
		.then(() => {
				console.log('succ')
				this._router.navigate(['account'])
			}
		)
	}

	register() {
		firstValueFrom(
			this._authService
					.register(
						this.formRegister.value.email!,
						this.formRegister.value.lastName!,
						this.formRegister.value.firstName!,
						this.formRegister.value.patronymic!,
						this.formRegister.value.address!,
						this.formRegister.value.phone!,
						this.formRegister.value.password!
					)
		).then(() => {
				console.log('succ')
			}
		)
	}
}
