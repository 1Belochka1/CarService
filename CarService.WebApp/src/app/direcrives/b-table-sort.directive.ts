import {
	Directive,
	ElementRef, Input, OnInit, Renderer2,
	TemplateRef
} from '@angular/core'

@Directive({
	selector: '[bTableSort]',
	standalone: true
})
export class BTableSortDirective implements OnInit {

	@Input()
	bTableSort: { v: string, n: string }


	constructor(
		public host: ElementRef<HTMLElement>,
		private _renderer: Renderer2
	) {
		console.log(host)
	}

	ngOnInit(): void {
		this._renderer.appendChild(
			this.host.nativeElement,
			this._renderer.createText(this.bTableSort.n)
		)
	}

}
