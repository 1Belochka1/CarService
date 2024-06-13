import {Component, EventEmitter, Input, Output, TemplateRef} from '@angular/core'
import {AsyncPipe, NgIf} from '@angular/common'
import {firstValueFrom, Observable} from "rxjs";
import {AuthService} from "../../services/auth.service";
import {ModalService} from "../../services/modal.service";
import {BTableComponent} from '../b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {NotSpecifiedPipe} from '../../pipe/not-specified.pipe'
import {
	TableSortHeaderIconDirective
} from '../../direcrives/table-sort-header-icon.directive'
import {Router} from '@angular/router'
import {MatIcon} from '@angular/material/icon'
import {MatIconButton} from '@angular/material/button'

@Component({
	selector: 'app-table-workers',
	standalone: true,
	imports: [
		AsyncPipe,
		BTableComponent,
		BTemplateDirective,
		NotSpecifiedPipe,
		TableSortHeaderIconDirective,
		MatIcon,
		MatIconButton,
		NgIf
	],
	templateUrl: './table-workers.component.html',
	styleUrl: './table-workers.component.scss',
	providers: [ModalService]
})
export class TableWorkersComponent {

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value})
	items: any[]

	@Input() addButton: boolean = false

	@Input() headerTitle: string | null

	@Output() addClick: EventEmitter<any> = new EventEmitter<any>()
	@Output() remove: EventEmitter<any> = new EventEmitter<any>()
	@Input() deleteTitle: string;

	roleId$: Observable<number>

	constructor(private _router: Router,
	            private _modalService: ModalService,
	            private _authService: AuthService
	) {
		this.roleId$ = this._authService.getRoleId$()
	}

	goToWorker(id: string) {
		this._router.navigate(['account', 'worker', id]).then(() => {
		})
	}

	onAddClick() {
		this.addClick.emit()
	}


	onRemoveClick(temlate: TemplateRef<any>, id: string) {
		this._modalService.open(temlate, {title: this.deleteTitle})
			?.subscribe((isConfirm) => {
				if (isConfirm)
					this.remove.emit(id)
			})

	}
}
