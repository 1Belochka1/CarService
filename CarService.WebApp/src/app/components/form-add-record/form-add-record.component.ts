import {Component, EventEmitter, ViewEncapsulation} from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {NgForOf, NgIf} from '@angular/common'
import {IItem, SelectComponent} from '../select/select.component'
import {Priority} from '../../enums/priority.enum'
import {RecordsService} from '../../services/records/records.service'
import {firstValueFrom} from 'rxjs'
import {AuthService} from '../../services/auth.service'
import {ToastrService} from 'ngx-toastr'
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field'
import {MatInput} from '@angular/material/input'

@Component({
	selector: 'app-form-add-record',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgForOf,
		NgIf,
		SelectComponent,
		MatError,
		MatLabel,
		MatFormField,
		MatInput
	],
	templateUrl: './form-add-record.component.html',
	styleUrl: './form-add-record.component.scss',
	encapsulation: ViewEncapsulation.None
})
export class FormAddRecordComponent {

	submit: EventEmitter<any> = new EventEmitter<any>()

	requestForm: FormGroup

	isAuth: boolean = false

	priorities: IItem<Priority>[] = [
		{
			value: Priority.Низкий,
			name: Priority[Priority.Низкий]
		},
		{
			value: Priority.Средний,
			name: Priority[Priority.Средний]
		},
		{
			value: Priority.Высокий,
			name: Priority[Priority.Высокий]
		},
		{
			value: Priority['Очень высокий'],
			name: Priority[Priority['Очень высокий']]
		},
	]

	constructor(private fb: FormBuilder,
							private _recordService: RecordsService,
							private _authService: AuthService,
							private _toastr: ToastrService
	) {
		firstValueFrom(_authService.getByCookie()).then((user) => {
			if (user) {
				console.log(user)
				if (user.userAuth.roleId == 1) {
					this.isAuth = false
					this.setForUnAuth()
				} else {
					this.isAuth = true
					this.setForAuth(user.email)
				}
			} else {
				this.setForUnAuth()
			}
		})


		console.log(this.priorities)
	}

	setForAuth(email: string) {
		this.requestForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
			problemDescription: ['', [Validators.required, Validators.minLength(10)]],
			carDescription: ['', [Validators.minLength(5)]],
		})

		this.requestForm.patchValue({email: email})
	}

	setForUnAuth() {
		this.requestForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(2)]],
			email: ['', [Validators.required, Validators.email]],
			phone: ['', [Validators.required, Validators.pattern('^8\\d{10}$')]],
			problemDescription: ['', [Validators.required, Validators.minLength(10)]],
			carDescription: ['', [Validators.minLength(5)]],
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			firstValueFrom(this._recordService.create(
				this.requestForm.get('email')?.value,
				this.requestForm.get('problemDescription')?.value,
				this.requestForm.get('carDescription')?.value,
				this.requestForm.get('name')?.value,
				this.requestForm.get('phone')?.value,
			)).then(() => {
				this.submit.emit()
				this._toastr.success('Заявка успешно создана')
			})
		}
	}


}
