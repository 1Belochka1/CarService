import { Component } from '@angular/core';
import {NgClass, NgOptimizedImage} from '@angular/common'
import {RouterLink} from '@angular/router'

@Component({
  selector: 'app-header-lending',
  standalone: true,
	imports: [
		NgClass,
		RouterLink,
		NgOptimizedImage
	],
  templateUrl: './header-lending.component.html',
  styleUrl: './header-lending.component.scss'
})
export class HeaderLendingComponent {

}
