import {Component} from '@angular/core'
import {InputTextModule} from 'primeng/inputtext'
import {
	FormBuilder,
	FormControl,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgClass, NgIf} from '@angular/common'
import {FormInputComponent} from '../../components/form-input/form-input.component'
import {UsersService} from '../../services/users/users.service'
import {firstValueFrom} from 'rxjs'

@Component({
	selector: 'app-auth-page',
	standalone: true,
	imports: [
		InputTextModule,
		ReactiveFormsModule,
		NgClass,
		NgIf,
		FormInputComponent
	],
	templateUrl: './auth-page.component.html',
	styleUrl: './auth-page.component.scss'
})
export class AuthPageComponent {

	form = this._formBuilder.group(
		{
			email: new FormControl('', [Validators.required, Validators.email]),
			password: new FormControl('', [Validators.required]),
		},
		{
			updateOn: 'blur'
		}
	)

	activePage: 1 | 2 = 1

	constructor(private _formBuilder: FormBuilder, private _userService: UsersService) {
		this.form.statusChanges.subscribe((d) => {
			console.log(d)
		})
	}

	authChanged(event: any) {
		this.activePage = event.target.value
	}

	login() {
		firstValueFrom(
			this._userService
					.login(this.form.value.email!, this.form.value.password!))
		.then(r => console.log(r)
		)
	}
}
