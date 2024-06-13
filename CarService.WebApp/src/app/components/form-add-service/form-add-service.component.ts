import {Component, EventEmitter, Output} from '@angular/core'
import {NgIf} from '@angular/common'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {MatButton} from "@angular/material/button";
import {MatCheckbox} from "@angular/material/checkbox";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {ServicesService} from '../../services/services/services.service'

@Component({
	selector: 'app-form-add-service',
	standalone: true,
	imports: [
		NgIf,
		ReactiveFormsModule,
		MatCheckbox,
		MatError,
		MatLabel,
		MatFormField,
		MatInput,
		MatButton
	],
	templateUrl: './form-add-service.component.html',
	styleUrl: './form-add-service.component.scss'
})
export class FormAddServiceComponent {
	@Output()
	onAddSubmit = new EventEmitter<boolean>()

	requestForm: FormGroup

	private _file?: File

	btnText: string = 'Изображение';

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
						this.onAddSubmit.emit(true)
					},
					error: err => {
						this.onAddSubmit.emit(false)
					}
				})
		}

	}

	fileUpload($event: Event) {

		const files = ($event.target as HTMLInputElement).files

		if (files && files.length > 0) {

			this._file = files[0]
			this.requestForm.patchValue({image: true})
			this.btnText = this._file.name
		}
	}
}
