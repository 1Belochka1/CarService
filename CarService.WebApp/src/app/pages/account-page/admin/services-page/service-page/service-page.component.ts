import {Component, TemplateRef} from '@angular/core'
import {
	ServicesService
} from '../../../../../services/services/services.service'
import {ActivatedRoute} from '@angular/router'
import {firstValueFrom, Observable, tap} from 'rxjs'
import {AsyncPipe, Location, NgIf} from '@angular/common'
import {SrcImagePipe} from '../../../../../pipe/src-image.pipe'
import {ModalService} from '../../../../../services/modal.service'
import {ImageService} from '../../../../../services/image.service'
import {ToastrService} from 'ngx-toastr'
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'

@Component({
	selector: 'app-service-page',
	standalone: true,
	imports: [
		NgIf,
		AsyncPipe,
		SrcImagePipe,
		ReactiveFormsModule
	],
	templateUrl: './service-page.component.html',
	styleUrl: './service-page.component.scss',
	providers: [ServicesService, ModalService]
})
export class ServicePageComponent {

	service: Observable<any>

	updateForm: FormGroup

	private readonly _id: string

	private _file?: File

	private _imageId: string

	constructor(private _servicesService: ServicesService,
							private fb: FormBuilder,
							private _route: ActivatedRoute,
							private _modalService: ModalService,
							private _imageService: ImageService,
							private _toastr: ToastrService,
							private _location: Location
	) {
		const id = this._route.snapshot.paramMap.get('serviceId')

		if (id === null) {
			return
		}

		this._id = id

		this.updateForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(3)]],
			description: ['', [Validators.required, Validators.minLength(10)]],
			isShowLending: [false],
		})

		this.setItem()
	}

	public setItem() {
		if (this._id && this._id != '') {
			this.service = this._servicesService.getServiceById(this._id).pipe(
				tap((service: any) => {
					this.updateForm.patchValue({
						name: service.name,
						description: service.description,
						isShowLending: service.isShowLending,
					})
					this._imageId = service.imageId
				})
			)
		}
	}

	openModal(templateRef: TemplateRef<any>, options?: any) {
		this._modalService.open(templateRef, options)?.subscribe(
			(isConfirm) => {
				if (isConfirm) {
					if (this._file) {
						const formData = new FormData()

						formData.append('imageId', this._imageId)
						formData.append('serviceId', this._id)

						if (this._imageId && this._imageId != '') {
							formData.append('newFile', this._file)
							firstValueFrom(this._imageService.update(formData))
							.then((v) => this.setItem())
						} else {
							formData.append('file', this._file)
							firstValueFrom(this._imageService.createImage(formData))
							.then((v) => this.setItem())
						}
						this._file = undefined
					} else {
						this.setItem()
					}
				}
			}
		)
	}

	onFileChange(event: Event) {
		const files = (event.target as HTMLInputElement).files

		if (files && files.length > 0) {
			this._file = files[0]
		}
	}

	updateSubmit() {
		firstValueFrom(this._servicesService.update(this._id,
			this.updateForm.get('name')?.value,
			this.updateForm.get('description')?.value,
			this.updateForm.get('isShowLending')?.value,
		)).then(() => {
			this._modalService.closeModal(
				true)
		})
	}

	openDeleteModal(templateRef: TemplateRef<any>) {
		this._modalService.open(templateRef, {
			title: 'Вы действительно хотите' +
				' удалить данные услуги'
		})?.subscribe((isConfirm) => {
			firstValueFrom(this._servicesService.delete(this._id)).then(() => {
				this._toastr.success("Данные услуги удалены")
				this._location.back()
			})
		})
	}
}
