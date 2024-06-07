import {Component} from '@angular/core'
import {UsersService} from '../../../../services/users/users.service'
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
import {Observable} from 'rxjs'


@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [AsyncPipe, NgForOf, NgIf, PaginationComponent, SearchComponent, SelectComponent, DatePipe, SvgIconComponent, NgClass, BTableComponent, BTemplateDirective, FullNamePipe, NotSpecifiedPipe],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss'
})
export class ClientsPageComponent {

	items$: Observable<any> = new Observable()

	constructor(private _userService: UsersService) {
		this.setItems()
	}

	setItems() {
		this.items$ = this._userService.getClients()
	}

}
