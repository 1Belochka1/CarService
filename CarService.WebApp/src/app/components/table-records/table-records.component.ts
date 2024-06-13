import {Component, EventEmitter, Input, Output, TemplateRef} from '@angular/core'
import {ToastrService} from "ngx-toastr";
import {firstValueFrom, Observable} from "rxjs";
import {Priority} from '../../enums/priority.enum'
import {Status} from '../../enums/status.enum'
import {AsyncPipe, DatePipe, NgIf} from '@angular/common'
import {AuthService} from "../../services/auth.service";
import {ModalService} from "../../services/modal.service";
import {RecordsService} from "../../services/records/records.service";
import {BTableComponent} from '../b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	TableSortHeaderIconDirective
} from '../../direcrives/table-sort-header-icon.directive'
import {Router} from '@angular/router'
import {NotSpecifiedPipe} from '../../pipe/not-specified.pipe'
import {MatIcon} from '@angular/material/icon'
import {MatIconButton} from '@angular/material/button'

@Component({
	selector: 'app-table-records',
	standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		DatePipe,
		TableSortHeaderIconDirective,
		NotSpecifiedPipe,
		MatIcon,
		MatIconButton,
		NgIf
	],
	templateUrl: './table-records.component.html',
	styleUrl: './table-records.component.scss'
})
export class TableRecordsComponent {

	protected readonly priority = Priority
	protected readonly status = Status

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value})
	items: any[]

	@Input() addButton: boolean = false

	@Output() addClick: EventEmitter<any> = new EventEmitter<any>()

	@Output() update: EventEmitter<any> = new EventEmitter<any>()
	@Input() headerTitle: string | null;

	roleId$: Observable<number>

	constructor(
		private _router: Router,
		private _recordService: RecordsService,
		private _modalService: ModalService,
		private _authService: AuthService,
		private _toast: ToastrService
	) {
		this.roleId$ = this._authService.getRoleId$()

	}

	navigateRecord(id: string) {
		this._router.navigate(['account', 'record', id]).then(() => {
		})
	}

	onAddClick() {
		this.addClick.emit()
	}

	onRemoveClick(temlate: TemplateRef<any>, id: string) {
		this._modalService.open(temlate, {title: "Вы дейстивтельно хотите удалить заявку?"})
			?.subscribe((isConfirm) => {
				if (isConfirm)
					firstValueFrom(this._recordService.delete(id))
						.then(() => {
								this._toast.success("Заявка удалена")
								this.update.emit()
							}
						)
			})

	}
}
