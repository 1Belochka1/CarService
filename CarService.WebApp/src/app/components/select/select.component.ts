import {
	Component,
	ElementRef,
	EventEmitter,
	Input,
	OnInit,
	Output,
	Renderer2,
	ViewChild,
	ViewEncapsulation
} from '@angular/core'
import {NgForOf, NgIf} from '@angular/common'

export interface IItem<T> {
	value: T | string | number,
	name: string
}

@Component({
	selector: 'app-select',
	standalone: true,
	imports: [
		NgForOf,
		NgIf
	],
	templateUrl: './select.component.html',
	styleUrl: './select.component.scss',
})
export class SelectComponent implements OnInit {
	@ViewChild('details', {static: true})
	details: ElementRef<HTMLDetailsElement> | undefined

	@Output()
	selectionChange: EventEmitter<number> = new EventEmitter()

	@Input()
	items: IItem<any>[] = []

	@Input()
	width: number = 150

	@Input()
	selectedIndex = 0

	@Input()
	classContainer: string

	constructor(private _renderer: Renderer2) {
	}

	ngOnInit(): void {
		console.log(this)
	}

	change(event: any) {
		const input = event.target as HTMLInputElement

		if (this.details)
			this.details.nativeElement.open = false

		this.selectedIndex = this.items.findIndex(
			(i) => {
				return i.value == input.value
			}
		)

		this.selectionChange.emit(this.selectedIndex)
	}
}
