import {Component, inject} from '@angular/core'
import {UsersService} from '../../../../services/users/users.service'
import {
	AsyncPipe,
	DatePipe,
	NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {PaginationComponent} from '../../../../components/pagination/pagination.component'
import {SearchComponent} from '../../../../components/search/search.component'
import {SelectComponent} from '../../../../components/select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {BTableComponent} from '../../../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../../../direcrives/b-template.directive'
import {ClientsService} from '../../../../services/users/clients.service'
import {FullNamePipe} from '../../../../pipe/full-name.pipe'
import {NotSpecifiedPipe} from '../../../../pipe/not-specified.pipe'


@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [AsyncPipe, NgForOf, NgIf, PaginationComponent, SearchComponent, SelectComponent, DatePipe, SvgIconComponent, NgClass, BTableComponent, BTemplateDirective, FullNamePipe, NotSpecifiedPipe],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss'
})
export class ClientsPageComponent {

	clientsService: ClientsService = inject(ClientsService)

	constructor(private _userService: UsersService) {
		this.clientsService.update()
	}

}
