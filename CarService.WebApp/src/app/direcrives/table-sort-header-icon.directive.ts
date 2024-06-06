import {
	Directive,
	ElementRef,
	Input,
	OnChanges,
	OnInit,
	Renderer2,
	SimpleChanges
} from '@angular/core'
import {HttpClient} from '@angular/common/http'

@Directive({
	selector: '[appTableSortHeaderIcon]',
	standalone: true
})
export class TableSortHeaderIconDirective implements OnInit, OnChanges {

	@Input() sortDirection: 'asc' | 'desc' | 'none' = 'none'
	@Input() isActive: boolean = false;

	private div: HTMLElement
	private divSvg: HTMLElement

	constructor(private element: ElementRef<HTMLTableCellElement>, private renderer2: Renderer2, private httpClient: HttpClient) {
	}

	ngOnInit(): void {
		const innerText = this.element.nativeElement.innerText
		this.element.nativeElement.innerText = ''

		this.div = this.renderer2.createElement('div')
		this.renderer2.addClass(this.div, 'flex')
		this.renderer2.addClass(this.div, 'gap-2')
		this.renderer2.addClass(this.div, 'justify-center')

		const textElement = this.renderer2.createText(innerText)
		this.renderer2.appendChild(this.div, textElement)

		this.loadSvg()

		this.renderer2.appendChild(this.element.nativeElement, this.div)
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (changes['sortDirection'] || changes['isActive']) {
			this.updateSvg()
		}
	}

	private loadSvg(): void {
		const svgPath = "assets/svg/minimize-min.svg"

		this.httpClient.get(svgPath, {responseType: 'text'}).subscribe(svg => {
			this.divSvg = this.renderer2.createElement('div')
			this.renderer2.setProperty(this.divSvg, 'innerHTML', svg)
			this.renderer2.addClass(this.divSvg, 'svg-white-sort')

			this.renderer2.addClass(this.divSvg, 'hidden')

			this.renderer2.appendChild(this.div, this.divSvg)
		})
	}

	private updateSvg(): void {
		if (this.divSvg) {
			this.renderer2.addClass(this.divSvg, 'hidden') // Скрываем SVG по умолчанию
			this.renderer2.removeClass(this.divSvg, 'asc')
			this.renderer2.removeClass(this.divSvg, 'desc')

			if (this.isActive) {
				this.renderer2.removeClass(this.divSvg, 'hidden')
				if (this.sortDirection === 'asc') {
					this.renderer2.addClass(this.divSvg, 'asc')
				} else if (this.sortDirection === 'desc') {
					this.renderer2.addClass(this.divSvg, 'desc')
				}
			}
		}
	}
}