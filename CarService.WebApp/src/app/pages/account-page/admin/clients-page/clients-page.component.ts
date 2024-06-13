import {Component, TemplateRef} from '@angular/core'
import {ToastrService} from "ngx-toastr";
import {TableSortHeaderIconDirective} from "../../../../direcrives/table-sort-header-icon.directive";
import {ModalService} from "../../../../services/modal.service";
import {IClient, UsersService} from '../../../../services/users/users.service'
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {
	PaginationComponent
} from '../../../../components/pagination/pagination.component'
import {SearchComponent} from '../../../../components/search/search.component'
import {SelectComponent} from '../../../../components/select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {NotSpecifiedPipe} from '../../../../pipe/not-specified.pipe'
import {firstValueFrom, Observable} from 'rxjs'
import {MatIcon} from '@angular/material/icon'
import {MatIconButton} from '@angular/material/button'


@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [AsyncPipe, NgForOf, NgIf, PaginationComponent, SearchComponent, SelectComponent, DatePipe, SvgIconComponent, NgClass, BTableComponent, BTemplateDirective, FullNamePipe, NotSpecifiedPipe, MatIcon, MatIconButton, TableSortHeaderIconDirective],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss',
	providers: [ModalService]
})
export class ClientsPageComponent {

	items$: Observable<IClient[]> = new Observable()

	constructor(
		private _userService: UsersService,
		private _modalService: ModalService,
		private _toastr: ToastrService,
	) {
		this.setItems()
	}

	setItems() {
		this.items$ = this._userService.getClients()
	}

	onRemoveClick(temlate: TemplateRef<any>, id: string) {
		this._modalService.open(temlate, {title: "Вы действительно хотите удалить данные клиента?"})
			?.subscribe((isConfirm) => {
				if (isConfirm)
					firstValueFrom(this._userService.delete(id))
						.then(() => {
							this.setItems()
							this._toastr.success('Данные клиента удалены')
						})
			})
	}

	updateClientToMaster(temp: TemplateRef<any>, id: string) {
		this._modalService.open(temp, {title: "Назначить сотрудником?"})
			?.subscribe((isConfirm) => {
				if (isConfirm)
					firstValueFrom(this._userService.updateToMasterById(id))
						.then(() => {
							this.setItems()
							this._toastr.success('Сотрудник назначен')
						})
			})
	}
}
