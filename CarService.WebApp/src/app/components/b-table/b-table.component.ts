import {
	AfterContentInit,
	ChangeDetectorRef,
	Component,
	ContentChildren,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output,
	QueryList,
	SimpleChanges,
	TemplateRef
} from '@angular/core'
import {BTemplateDirective} from '../../direcrives/b-template.directive'
import {NgClass, NgForOf, NgIf, NgTemplateOutlet} from '@angular/common'
import {PaginationComponent} from '../pagination/pagination.component'
import {SearchComponent} from '../search/search.component'
import {SelectComponent} from '../select/select.component'
import {SvgIconComponent} from 'angular-svg-icon'
import {SortComponent} from '../sort/sort.component'
import {DistinctPipe} from '../../pipe/distinct.pipe'
import {SortingPipe} from '../../pipe/sorting.pipe'
import {PaginationPipe} from '../../pipe/pagination.pipe'
import {Data} from '@angular/router'

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
		DistinctPipe,
		SortingPipe,
		PaginationPipe
	],
	templateUrl: './b-table.component.html',
	styleUrl: './b-table.component.scss',
})
export class BTableComponent implements OnInit, OnChanges, AfterContentInit {

	@Input({transform: (value: any[] | null): any[] => value == null ? [] : value}) items: any[]

	@Input() addButton: boolean = false
	@Input() deleteButton: boolean = true
	@Input() searchVisible: boolean = true

	@Input() headTemplate?: TemplateRef<any>

	@Input() bodyTemplate?: TemplateRef<any>

	@Input() toolboxTemplate?: TemplateRef<any>

	@Output() addButtonClick: EventEmitter<any> = new EventEmitter<any>()
	@Output() deleteButtonClick: EventEmitter<any> = new EventEmitter<any>()

	@ContentChildren(BTemplateDirective) templates?: QueryList<BTemplateDirective>

	@Input() selectSortColumn: string = ''
	@Input() sortDescending: boolean = false
	@Input() pageSize: number = 25

	filterData: Data
	currentPage: number = 1
	filteredItems: any[]
	isLoading: boolean = false
	notFound: boolean = false
	searchText: string
	totalPages: number
	update: boolean = false

	constructor(private cd: ChangeDetectorRef) {
	}

	ngOnChanges(changes: SimpleChanges): void {
		this.setItems()
		this._page()
	}

	ngOnInit(): void {
	}

	setItems() {
		this.filteredItems = this.items
		this.notFound = this.filteredItems.length < 1
	}

	addClick() {
		this.addButtonClick.emit()
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

	onSearchChange(searchText: string) {
		this.isLoading = true
		this.filteredItems = this.items.filter(item => {
			return Object.values(item).some(value =>
				value != null && value.toString().toLowerCase().includes(searchText.toLowerCase())
			)
		})
		this.currentPage = 1
		this.isLoading = false

		this.notFound = this.filteredItems.length < 1

		this._filteredPage()
	}

	currentPageChanged(page: number) {
		this.currentPage = page
	}

	sortChange(sortColumn: string) {
		if (this.selectSortColumn == sortColumn) {
			this.sortDescending = !this.sortDescending
		}

		this.selectSortColumn = sortColumn

		this.currentPage = 1
		this.update = !this.update
	}

	private _page() {
		this.totalPages = Math.ceil(this.items.length / this.pageSize)
	}

	private _filteredPage() {
		this.totalPages = Math.ceil(this.filteredItems.length / this.pageSize)
	}

	onDelete(id: any) {
		this.deleteButtonClick.emit(id)
	}
}
