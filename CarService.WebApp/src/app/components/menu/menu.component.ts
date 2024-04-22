import {NgClass, NgForOf} from "@angular/common";
import {Component, ElementRef, inject} from '@angular/core';
import {SvgIconComponent} from "angular-svg-icon";
import {AuthService} from "../../services/auth.service";
import {menuItems} from "./menu.data";

@Component({
	selector: 'panel-menu',
	standalone: true,
	imports: [
		NgClass,
		NgForOf,
		SvgIconComponent,
	],
	providers: [],
	templateUrl: './menu.component.html',
	styleUrl: './menu.component.scss'
})
export class MenuComponent {
	idRole: number = 3
	isMinimize: boolean = false;
	protected readonly menuItems = menuItems;

	constructor(element: ElementRef) {
		const authService = inject(AuthService)

		this.idRole = authService.user.idRole;
	}

	minimize() {
		this.isMinimize = !this.isMinimize
	}


}
