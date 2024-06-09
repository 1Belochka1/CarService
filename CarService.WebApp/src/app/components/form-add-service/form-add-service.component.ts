import {Component, EventEmitter, Output} from '@angular/core'
import {NgIf} from '@angular/common'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {ServicesService} from '../../services/services/services.service'

@Component({
	selector: 'app-form-add-service',
	standalone: true,
	imports: [
		NgIf,
		ReactiveFormsModule
	],
	templateUrl: './form-add-service.component.html',
	styleUrl: './form-add-service.component.scss'
})
export class FormAddServiceComponent {
	@Output()
	submit = new EventEmitter<boolean>()

	requestForm: FormGroup

	private _file?: File

	constructor(private _fb: FormBuilder,
							private _servicesService: ServicesService
	) {
		this.requestForm = this._fb.group({
			name: ['', Validators.required],
			description: ['', Validators.required],
			isShowLanding: [false, Validators.required],
			image: [null]
		})
	}

	async onSubmit() {
		if (this.requestForm.valid) {
			const formData = new FormData()
			formData.append('Name', this.requestForm.get('name')!.value)
			formData.append('Description', this.requestForm.get('description')!.value)
			formData.append('IsShowLanding', this.requestForm.get('isShowLanding')!.value)

			if (this._file)
				formData.append('File', this._file)

			this._servicesService.create(formData)
					.subscribe({
						next: (response) => {
							console.log(response)
							this.submit.emit(true)
						},
						error: err => {
							this.submit.emit(false)
						}
					})
		}

	}

	fileUpload($event: Event) {

		const files = ($event.target as HTMLInputElement).files

		if (files && files.length > 0) {

			this._file = files[0]
			this.requestForm.patchValue({image: true})
		}
	}
}
