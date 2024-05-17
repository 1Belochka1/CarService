import {
	AfterViewInit,
	Component,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output
} from '@angular/core'
import {NgClass, NgForOf, NgIf} from '@angular/common'

@Component({
	selector: 'app-pagination',
	standalone: true,
	imports: [
		NgForOf,
		NgClass,
		NgIf
	],
	templateUrl: './pagination.component.html',
	styleUrl: './pagination.component.scss',
})
export class PaginationComponent implements OnChanges{

	@Output()
	changePage: EventEmitter<number> = new EventEmitter<number>()

	@Input({required: true})
	currentPage: number | null

	@Input({required: true})
	totalPages: number

	pages: number[]

	setPage(page: number): void {
		if (this.currentPage == page)
			return

		this.currentPage = page
		this.changePage.emit(this.currentPage)
	}

	ngOnChanges(): void {
		this.pages = Array.from({length: this.totalPages},
			(_, i) => i + 1)
	}

}
