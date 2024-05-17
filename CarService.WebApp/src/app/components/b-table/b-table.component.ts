import {
	AfterContentInit,
	Component,
	ContentChildren, Input,
	QueryList,
	TemplateRef,
	ViewEncapsulation
} from '@angular/core'
import {
	BTemplateDirective
} from '../../direcrives/b-template.directive'
import {
	NgForOf,
	NgIf,
	NgTemplateOutlet
} from '@angular/common'

@Component({
	selector: 'b-table',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		NgIf,
		NgForOf
	],
	templateUrl: './b-table.component.html',
	styleUrl: './b-table.component.scss',
})
export class BTableComponent implements AfterContentInit {

	@Input()
	items: any

	@Input()
	isLoading: boolean = false

	@Input()
	notFound: boolean = false

	bodyTemplate?: TemplateRef<any>

	headTemplate?: TemplateRef<any>

	@ContentChildren(BTemplateDirective)
	templates?: QueryList<BTemplateDirective>

	ngAfterContentInit(): void {
		if (this.templates)
			this.templates.forEach(
				(template) => {
					if (template.bTemplate === 'body')
						this.bodyTemplate = template.template

					if (template.bTemplate === 'head')
						this.headTemplate = template.template

					console.log(template)
				}
			)
	}

}
