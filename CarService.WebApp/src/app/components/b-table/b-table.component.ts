import {
	AfterContentInit,
	ChangeDetectorRef,
	Component,
	ContentChildren,
	Input,
	OnChanges, OnInit,
	QueryList,
	SimpleChanges,
	TemplateRef
} from '@angular/core'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {
	NgClass,
	NgForOf,
	NgIf,
	NgTemplateOutlet
} from '@angular/common'
import {PaginationComponent} from '../pagination/pagination.component'
import {TableService} from '../../services/table.service'
import {SearchComponent} from '../search/search.component'
import {
	IItem,
	SelectComponent
} from '../select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {SortComponent} from '../sort/sort.component'
import {DistinctPipe} from '../../pipe/distinct.pipe'

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
		DistinctPipe,
		NgClass,
		SortComponent,
		DistinctPipe
	],
	templateUrl: './b-table.component.html',
	styleUrl: './b-table.component.scss',
})
export class BTableComponent implements OnInit, AfterContentInit, OnChanges {

	@Input()
	headItems: IItem<any>[]

	@Input()
	service: TableService

	@Input()
	items: any

	@Input()
	isLoading: boolean = false

	@Input()
	notFound: boolean = false

	@Input()
	toolboxTemplate: TemplateRef<any>

	@Input()
	headTemplate?: TemplateRef<any>

	@Input()
	bodyTemplate?: TemplateRef<any>

	@ContentChildren(BTemplateDirective)
	templates?: QueryList<BTemplateDirective>

	rowCount: number = 5

	emptyRows: any[] = []

	headers: string[] = []

	constructor(private cd: ChangeDetectorRef) {
	}

	ngOnInit(): void {
		this.service.sortProperties = this.headItems
	}

	ngAfterContentInit(): void {
		if (this.templates) {
			this.templates.forEach(
				(template) => {
					if (template.bTemplate === 'body')
						this.bodyTemplate = template.template

					if (template.bTemplate === 'head')
						this.headTemplate = template.template

					if (template.bTemplate === 'toolbox')
						this.toolboxTemplate = template.template
				}
			)
		}
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (this.rowCount == 0) {
			this.rowCount = this.service.pageSize
		}
		if (this.items && this.items.length < this.rowCount && !this.service.notFound)
			this.emptyRows = new Array(this.rowCount - this.items.length)
		else
			this.emptyRows = []
	}

	sortChange(sortProp: IItem<any>) {

		if (this.service.sortProperty == sortProp)
			this.service.toggleDescending()
		else
			this.service.sortProperty = sortProp

	}
}
