import { AsyncPipe, NgClass, NgForOf, NgIf } from '@angular/common'
import {
	AfterContentInit,
	ChangeDetectorRef,
	Component,
	inject,
} from '@angular/core'
import { MatIcon } from '@angular/material/icon'
import { Router, RouterLink } from '@angular/router'
import { SvgIconComponent } from 'angular-svg-icon'
import { Observable, firstValueFrom, tap } from 'rxjs'
import { AuthService } from '../../services/auth.service'
import { TitleService } from '../../services/title.service'
import { menuItems } from './menu.data'

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
		AsyncPipe,
	],
	templateUrl: './menu-account.component.html',
	styleUrl: './menu-account.component.scss',
})
export class MenuAccountComponent implements AfterContentInit {
	roleId$: Observable<number>

	title = inject(TitleService).getTitle()

	isMinimize: boolean = true

	protected readonly menuItems = menuItems

	constructor(
		private _authService: AuthService,
		private _router: Router,
		private _cd: ChangeDetectorRef
	) {
		this.roleId$ = this._authService.getRoleId().pipe(
			tap(id => {
				this._cd.detectChanges()
				console.log(id)
			})
		)
	}

	ngAfterContentInit(): void {}

	toggleMenu() {
		this.isMinimize = !this.isMinimize
	}

	closeMenu(): void {
		if (!this.isMinimize) {
			this.isMinimize = true
		}
	}

	logout() {
		firstValueFrom(this._authService.logout()).then(() => {
			this._router.navigate(['/lending'])
		})
	}
}
