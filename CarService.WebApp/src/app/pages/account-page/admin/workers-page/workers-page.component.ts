import { CdkStep, CdkStepLabel } from '@angular/cdk/stepper'
import {
	AsyncPipe,
	NgClass,
	NgForOf,
	NgIf,
	NgTemplateOutlet,
} from '@angular/common'
import { Component, TemplateRef } from '@angular/core'
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms'
import { Router, RouterLink } from '@angular/router'
import { SvgIconComponent } from 'angular-svg-icon'
import { Observable } from 'rxjs'
import { AboutComponent } from '../../../../components/about/about.component'
import { BTableComponent } from '../../../../components/b-table/b-table.component'
import { FormRegisterComponent } from '../../../../components/form-register/form-register.component'
import { PaginationComponent } from '../../../../components/pagination/pagination.component'
import { SearchComponent } from '../../../../components/search/search.component'
import { SelectComponent } from '../../../../components/select/select.component'
import { StepperComponent } from '../../../../components/stepper/stepper.component'
import { TableWorkersComponent } from '../../../../components/table-workers/table-workers.component'
import { BTableSortDirective } from '../../../../direcrives/b-table-sort.directive'
import { BTemplateDirective } from '../../../../direcrives/b-template.directive'
import { TableSortHeaderIconDirective } from '../../../../direcrives/table-sort-header-icon.directive'
import { FullNamePipe } from '../../../../pipe/full-name.pipe'
import { NotSpecifiedPipe } from '../../../../pipe/not-specified.pipe'
import { AuthService } from '../../../../services/auth.service'
import { ModalService } from '../../../../services/modal.service'
import { UsersService } from '../../../../services/users/users.service'

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
	providers: [ModalService],
})
export class WorkersPageComponent {
	selectedIndexAddWorker: number
	requestForm: FormGroup

	items$: Observable<any>

	constructor(
		private _userService: UsersService,
		private _router: Router,
		private _modalService: ModalService,
		private _authService: AuthService,
		private fb: FormBuilder
	) {
		this.setItems()

		this.requestForm = this.fb.group({
			phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]],
		})
	}

	setItems() {
		this.items$ = this._userService.getWorkers()
	}

	goToWorker(id: string) {
		this._router.navigate(['account', 'worker', id]).then(() => {})
	}

	openModalAddWorker(addWorkerTemplate: TemplateRef<any>) {
		this._modalService
			.open(addWorkerTemplate, { actionVisible: false })
			?.subscribe(() => {
				this.selectedIndexAddWorker = 0
			})
	}

	addWorker() {
		this.setItems()
		this._modalService.closeModal(true)
	}
}
