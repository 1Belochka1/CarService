import {Component, TemplateRef} from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import {SvgIconComponent} from 'angular-svg-icon'
import {
	AsyncPipe,
	NgClass,
	NgForOf,
	NgIf,
	NgTemplateOutlet
} from '@angular/common'
import {SearchComponent} from '../../../../components/search/search.component'
import {
	PaginationComponent
} from '../../../../components/pagination/pagination.component'
import {SelectComponent} from '../../../../components/select/select.component'
import {Router, RouterLink} from '@angular/router'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {AboutComponent} from '../../../../components/about/about.component'
import {
	BTableSortDirective
} from '../../../../direcrives/b-table-sort.directive'
import {UsersService} from '../../../../services/users/users.service'
import {WorkersService} from '../../../../services/users/workers.service'
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {ModalService} from '../../../../services/modal.service'
import {AuthService} from '../../../../services/auth.service'
import {
	FormRegisterComponent
} from '../../../../components/form-register/form-register.component'
import {firstValueFrom, Observable} from 'rxjs'
import {CdkStep, CdkStepLabel} from '@angular/cdk/stepper'
import {
	StepperComponent
} from '../../../../components/stepper/stepper.component'
import {NotSpecifiedPipe} from '../../../../pipe/not-specified.pipe'
import {
	TableSortHeaderIconDirective
} from '../../../../direcrives/table-sort-header-icon.directive'
import {
	TableWorkersComponent
} from '../../../../components/table-workers/table-workers.component'


@Component({
	selector: 'app-workers-page',
	standalone: true,
	imports: [
		FormsModule,
		SvgIconComponent,
		NgForOf,
		SearchComponent,
		AsyncPipe,
		NgClass,
		PaginationComponent,
		NgIf,
		SelectComponent,
		RouterLink,
		BTableComponent,
		BTemplateDirective,
		AboutComponent,
		BTableSortDirective,
		FullNamePipe,
		FormRegisterComponent,
		CdkStep,
		StepperComponent,
		NgTemplateOutlet,
		CdkStepLabel,
		ReactiveFormsModule,
		NotSpecifiedPipe,
		TableSortHeaderIconDirective,
		TableWorkersComponent,
	],
	templateUrl: './workers-page.component.html',
	styleUrl: './workers-page.component.scss',
	providers: [ModalService]
})
export class WorkersPageComponent {
	selectedIndexAddWorker: number
	requestForm: FormGroup

	items$: Observable<any>

	constructor(private _userService: UsersService,
							private _router: Router,
							private _modalService: ModalService,
							private _authService: AuthService,
							private _workersService: WorkersService,
							private fb: FormBuilder
	) {
		this.setItems()

		this.requestForm = this.fb.group({
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]]
		})
	}

	setItems() {
		this.items$ = this._workersService.getWorkers()
	}

	goToWorker(id: string) {
		this._router.navigate(['account', 'worker', id]).then(() => {
		})
	}

	openModalAddWorker(addWorkerTemplate: TemplateRef<any>) {
		this._modalService.open(addWorkerTemplate, {actionVisible: false})?.subscribe(() => {
			this.selectedIndexAddWorker = 0
		})
	}

	addWorker($event: {
		phone: string;
		firstName: string;
		lastName: string;
		patronymic: string;
		address: string;
		password: string
	}) {
		firstValueFrom(
			this._authService.register(
				$event.lastName,
				$event.firstName,
				$event.patronymic,
				$event.address,
				$event.phone,
				$event.password,
				true
			)
		).then(() => {
			this.setItems()
			this._modalService.closeModal({isConfirm: true, isCancel: false})
		})
	}

	onSubmit() {
		if (this.requestForm.valid) {
			firstValueFrom(this._userService.updateByPhone(this.requestForm.get('phone')?.value))
			.then(() => {
				this.setItems()
				this._modalService.closeModal({isConfirm: true, isCancel: false})
			})
		}
	}

}
