import {
	Component,
	ElementRef,
	EventEmitter, Input,
	Output, TemplateRef
} from '@angular/core'
import {NgIf, NgTemplateOutlet} from '@angular/common'
import {SelectComponent} from '../select/select.component'

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
export class ModalComponent {

	@Input() title: string = ''

	@Input() header: TemplateRef<any>
	@Input() content: TemplateRef<any>
	@Input() action: TemplateRef<any>

	@Output() closeEvent = new EventEmitter()
	@Output() submitEvent = new EventEmitter()


	constructor(private elementRef: ElementRef) {

	}

	close(): void {
		this.elementRef.nativeElement.remove()
		this.closeEvent.emit()
	}

	submit(): void {
		this.elementRef.nativeElement.remove()
		this.submitEvent.emit()
	}

}
