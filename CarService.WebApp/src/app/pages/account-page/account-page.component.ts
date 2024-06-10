import {AsyncPipe, NgClass, NgForOf} from '@angular/common'
import {Component, inject} from '@angular/core'
import {RouterLink, RouterOutlet} from '@angular/router'
import {SvgIconComponent} from 'angular-svg-icon'
import {TitleService} from '../../services/title.service'
import {
	HeaderLendingComponent
} from '../../components/header-lending/header-lending.component'
import {
	MenuAccountComponent
} from '../../components/menu-account/menu-account.component'
import {AuthService} from '../../services/auth.service'
import {firstValueFrom} from 'rxjs'
import {NotifyService} from '../../services/notify.service'

@Component({
	selector: 'app-account-page',
	standalone: true,
	imports: [RouterOutlet, NgForOf, SvgIconComponent, RouterLink, NgClass, AsyncPipe, HeaderLendingComponent, MenuAccountComponent],
	templateUrl: './account-page.component.html',
	styleUrl: './account-page.component.scss',
	providers: [TitleService]
})
export class AccountPageComponent {

	title = inject(TitleService).getTitle()

	isMinimize: boolean = true

	constructor(private _authService: AuthService, private _notifyService: NotifyService) {

	}

	toggleMenu() {
		this.isMinimize = !this.isMinimize
	}

	closeMenu(): void {
		if (!this.isMinimize) {
			this.isMinimize = true;
		}
	}
}
