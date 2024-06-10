import {AfterContentInit, Component, inject} from '@angular/core'
import {menuItems} from './menu.data'
import {AsyncPipe, NgClass, NgForOf, NgIf} from '@angular/common'
import {SvgIconComponent} from 'angular-svg-icon'
import {Router, RouterLink} from '@angular/router'
import {TitleService} from '../../services/title.service'
import {AuthService} from '../../services/auth.service'
import {MatIcon} from '@angular/material/icon'
import {firstValueFrom, Observable} from 'rxjs'

@Component({
	selector: 'app-menu-account',
	standalone: true,
	imports: [
		NgForOf,
		SvgIconComponent,
		RouterLink,
		NgClass,
		NgIf,
		MatIcon,
		AsyncPipe
	],
	templateUrl: './menu-account.component.html',
	styleUrl: './menu-account.component.scss'
})
export class MenuAccountComponent implements AfterContentInit {

	roleId$: Observable<number>

	title = inject(TitleService).getTitle()

	isMinimize: boolean = true

	protected readonly menuItems = menuItems

	constructor(private _authService: AuthService, private _router: Router) {

	}

	ngAfterContentInit(): void {
		this.roleId$ = this._authService.getRoleId()
	}

	toggleMenu() {
		this.isMinimize = !this.isMinimize
	}

	closeMenu(): void {
		if (!this.isMinimize) {
			this.isMinimize = true
		}
	}

	logout() {
		firstValueFrom(this._authService.logout()).then
		(() => {
			this._router.navigate(['/lending'])
		})
	}
}
