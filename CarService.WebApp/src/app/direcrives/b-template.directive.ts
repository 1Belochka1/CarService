import {
	AfterViewInit,
	ContentChildren,
	Directive,
	ElementRef,
	Input, QueryList,
	TemplateRef, ViewContainerRef
} from '@angular/core'
import {BTableSortDirective} from './b-table-sort.directive'

@Directive({
	selector: '[bTemplate]',
	standalone: true
})
export class BTemplateDirective {



	@Input()
	bTemplate = ''

	constructor(
		public host: ElementRef<HTMLElement>,
		public template: TemplateRef<HTMLElement>,
	) {
	}

}
