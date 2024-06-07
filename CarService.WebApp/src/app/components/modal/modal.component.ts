import {
	AfterContentInit,
	Component,
	ElementRef,
	EventEmitter,
	HostListener,
	Input,
	OnDestroy,
	Output
} from '@angular/core'
import {NgIf, NgTemplateOutlet} from '@angular/common'

@Component({
	selector: 'app-modal',
	standalone: true,
	imports: [
		NgIf,
		NgTemplateOutlet,
	],
	templateUrl: './modal.component.html',
	styleUrl: './modal.component.scss'
})
export class ModalComponent implements AfterContentInit, OnDestroy {

	@Input() title?: string = ''

	actionVisible: boolean

	@Output() closeEvent = new EventEmitter<boolean>()
	protected readonly onkeydown = onkeydown

	constructor(private elementRef: ElementRef) {
	}

	ngOnDestroy(): void {
		console.log('destrou')
	}

	@HostListener('window:keydown.escape', ['$event'])
	updateValue(event: KeyboardEvent) {
		this.close()
	}

	ngAfterContentInit(): void {
		console.log(this)
	}

	close(): void {
		this.elementRef.nativeElement.remove()
		this.closeEvent.emit(false)
	}

	submit(): void {
		this.elementRef.nativeElement.remove()
		this.closeEvent.emit(true)
	}

	keydown(event: any) {
		console.log(event)
	}
}
