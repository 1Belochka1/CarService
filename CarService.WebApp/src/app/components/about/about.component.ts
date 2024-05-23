import {
	AfterContentInit,
	Component,
	ContentChild,
	Input,
	TemplateRef
} from '@angular/core'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {NgIf, NgTemplateOutlet} from '@angular/common'

@Component({
	selector: 'app-about',
	standalone: true,
	imports: [
		NgIf,
		NgTemplateOutlet
	],
	templateUrl: './about.component.html',
	styleUrl: './about.component.scss'
})
export class AboutComponent implements AfterContentInit {
	@Input()
	title: string = ''

	@Input()
	item: any

	@ContentChild(BTemplateDirective)
	bTemplate: BTemplateDirective

	templateAboutContent: TemplateRef<any>

	constructor() {

	}

	ngAfterContentInit(): void {
		if (this.bTemplate && this.bTemplate.bTemplate == 'about-content') {
			this.templateAboutContent = this.bTemplate.template
		}
	}
}
