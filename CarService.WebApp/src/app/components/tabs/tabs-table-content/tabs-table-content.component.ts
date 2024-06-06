import {
	AfterContentInit,
	Component, ContentChildren, ElementRef,
	Input, QueryList,
	TemplateRef,
	ViewChild
} from '@angular/core'
import {AsyncPipe, NgClass, NgIf} from '@angular/common'
import {BTableComponent} from '../../b-table/b-table.component'
import {TableService} from '../../../services/table.service'
import {BTemplateDirective} from '../../../direcrives/b-template.directive'
import { IItem } from '../../select/select.component'
import {RecordType} from '../../../models/record.type'

@Component({
	selector: 'app-tabs-table-content',
	standalone: true,
	imports: [
		NgClass,
		BTableComponent,
		AsyncPipe,
		NgIf
	],
	templateUrl: './tabs-table-content.component.html',
	styleUrl: './tabs-table-content.component.scss'
})
export class TabsTableContentComponent implements AfterContentInit {

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value}) items: any[]

	@Input()
	public header: string = ''

	@Input()
	public type: string = ''

	@Input()
	public selected: boolean = false

	@ViewChild('tmp')
	public template: TemplateRef<any>

	@ContentChildren(BTemplateDirective)
	templates?: QueryList<BTemplateDirective>

	bodyTemplate?: TemplateRef<any>

	headTemplate?: TemplateRef<any>

	constructor(public elementRef: ElementRef<HTMLElement>) {
	}

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
