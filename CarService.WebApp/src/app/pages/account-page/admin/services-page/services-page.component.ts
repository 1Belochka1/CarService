import {Component, TemplateRef} from '@angular/core'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {AsyncPipe, JsonPipe, NgForOf} from '@angular/common'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {ServicesService} from '../../../../services/services/services.service'
import {Observable} from 'rxjs'
import {
	CardServiceComponent
} from '../../../../components/card-service/card-service.component'
import {SrcImagePipe} from '../../../../pipe/src-image.pipe'
import {
	FormAddServiceComponent
} from '../../../../components/form-add-service/form-add-service.component'
import {ModalService} from '../../../../services/modal.service'
import {
	TableSortHeaderIconDirective
} from '../../../../direcrives/table-sort-header-icon.directive'
import {Router} from '@angular/router'
import {MatIconButton} from '@angular/material/button'
import {MatIcon} from '@angular/material/icon'

@Component({
	selector: 'app-services-page',
	standalone: true,
	imports: [
		BTableComponent,
		AsyncPipe,
		BTemplateDirective,
		JsonPipe,
		CardServiceComponent,
		NgForOf,
		SrcImagePipe,
		FormAddServiceComponent,
		TableSortHeaderIconDirective,
		MatIconButton,
		MatIcon
	],
	templateUrl: './services-page.component.html',
	styleUrl: './services-page.component.scss',
	providers: [ModalService, ServicesService]
})
export class ServicesPageComponent {

	items$: Observable<any>

	constructor(private servicesService: ServicesService,
							private _modalService: ModalService,
							private _router: Router
	) {
		this.setItems()
	}

	setItems() {
		this.items$ = this.servicesService.getServices()
	}

	addClick(template: TemplateRef<any>) {
		this._modalService.open(template, {
			title: 'Создание услуги',
			actionVisible: false
		})?.subscribe((isConfirm) => {
			if (isConfirm) {
				this.setItems()
			}
		})
	}

	addSubmit(success: boolean) {
		this._modalService.closeModal(success)
	}

	navigate(id: string) {
		this._router.navigate(['account', 'services', id]).then()
	}
}
