import {
	AfterContentInit,
	Component,
	ContentChildren, Input, OnChanges,
	QueryList,
	SimpleChanges,
	TemplateRef,
	ViewEncapsulation
} from '@angular/core'
import {
	BTemplateDirective
} from '../../direcrives/b-template.directive'
import {
	NgClass,
	NgForOf,
	NgIf,
	NgTemplateOutlet
} from '@angular/common'
import {PaginationComponent} from '../pagination/pagination.component'
import {TableService} from '../../services/table.service'
import {SearchComponent} from '../search/search.component'
import {SelectComponent} from '../select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'

@Component({
	selector: 'b-table',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		NgIf,
		NgForOf,
		PaginationComponent,
		SearchComponent,
		SelectComponent,
		SvgIconComponent,
		NgClass
	],
	templateUrl: './b-table.component.html',
	styleUrl: './b-table.component.scss',
})
export class BTableComponent implements AfterContentInit, OnChanges {

	emptyRows: any[] = []

	@Input()
	service: TableService

	@Input()
	items: any

	@Input()
	isLoading: boolean = false

	@Input()
	notFound: boolean = false

	@Input()
	rowCount: number = 0

	@Input()
	bodyTemplate?: TemplateRef<any>

	@Input()
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

	ngOnChanges(changes: SimpleChanges): void {
		if (this.items && this.items.length < this.rowCount && !this.service.notFound)
			this.emptyRows = new Array(this.rowCount - this.items.length)
		else
			this.emptyRows = []
	}
}
