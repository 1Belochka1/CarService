import {Component, inject} from '@angular/core'
import {UsersService} from '../../services/users.service'
import {
	AsyncPipe,
	DatePipe,
	NgClass,
	NgForOf,
	NgIf
} from '@angular/common'
import {PaginationComponent} from '../../components/pagination/pagination.component'
import {SearchComponent} from '../../components/search/search.component'
import {SelectComponent} from '../../components/select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {BTableComponent} from '../../components/b-table/b-table.component'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {ClientsService} from '../../services/clients.service'

@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [AsyncPipe, NgForOf, NgIf, PaginationComponent, SearchComponent, SelectComponent, DatePipe, SvgIconComponent, NgClass, BTableComponent, BTemplateDirective],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss'
})
export class ClientsPageComponent {

	clientsService: ClientsService = inject(ClientsService)

	constructor(private _userService: UsersService) {
		this.clientsService.update()
	}

}
