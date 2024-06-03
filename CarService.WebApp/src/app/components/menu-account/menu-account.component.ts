import {Component, inject} from '@angular/core'
import {menuItems} from '../../pages/account-page/menu.data'
import {NgClass, NgForOf, NgIf} from '@angular/common'
import {SvgIconComponent} from 'angular-svg-icon'
import {RouterLink} from '@angular/router'
import {TitleService} from '../../services/title.service'

@Component({
  selector: 'app-menu-account',
  standalone: true,
  imports: [
    NgForOf,
    SvgIconComponent,
    RouterLink,
    NgClass,
    NgIf
  ],
  templateUrl: './menu-account.component.html',
  styleUrl: './menu-account.component.scss'
})
export class MenuAccountComponent {

  protected readonly menuItems = menuItems

  title = inject(TitleService).getTitle()

  isMinimize: boolean = true

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
