import {
	AfterContentInit,
	Component,
	ElementRef,
	EventEmitter,
	Input,
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
export class ModalComponent implements AfterContentInit {

	@Input() title?: string = ''

	@Output() closeEvent = new EventEmitter<{
		isConfirm: boolean,
		isCancel: boolean
	}>()

	constructor(private elementRef: ElementRef) {

	}

	ngAfterContentInit(): void {
		console.log(this)
	}

	close(): void {
		this.elementRef.nativeElement.remove()
		this.closeEvent.emit({isCancel: true, isConfirm: false})
	}

	submit(): void {
		this.elementRef.nativeElement.remove()
		this.closeEvent.emit({isCancel: false, isConfirm: true})
	}

}
