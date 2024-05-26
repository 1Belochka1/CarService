import {NgClass, NgForOf} from '@angular/common'
import {Component} from '@angular/core'
import {RouterLink, RouterOutlet} from '@angular/router'
import {SvgIconComponent} from 'angular-svg-icon'
import {menuItems} from './menu.data'

@Component({
	selector: 'app-account-page',
	standalone: true,
	imports: [RouterOutlet, NgForOf, SvgIconComponent, RouterLink, NgClass],
	templateUrl: './account-page.component.html',
	styleUrl: './account-page.component.scss'
})
export class AccountPageComponent {

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
