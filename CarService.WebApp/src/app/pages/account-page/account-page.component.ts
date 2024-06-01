import {AsyncPipe, NgClass, NgForOf} from '@angular/common'
import {Component, inject} from '@angular/core'
import {RouterLink, RouterOutlet} from '@angular/router'
import {SvgIconComponent} from 'angular-svg-icon'
import {menuItems} from './menu.data'
import {TitleService} from '../../services/title.service'

@Component({
	selector: 'app-account-page',
	standalone: true,
	imports: [RouterOutlet, NgForOf, SvgIconComponent, RouterLink, NgClass, AsyncPipe],
	templateUrl: './account-page.component.html',
	styleUrl: './account-page.component.scss',
	providers: [TitleService]
})
export class AccountPageComponent {

	title = inject(TitleService).getTitle()

	isMinimize: boolean = true

	protected readonly menuItems = menuItems

	constructor() {
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
