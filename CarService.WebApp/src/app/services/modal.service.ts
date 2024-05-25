import {
	Inject,
	Injectable,
	Injector,
	TemplateRef,
	ViewContainerRef
} from '@angular/core'
import {DOCUMENT} from '@angular/common'
import {ModalComponent} from '../components/modal/modal.component'
import {Subject} from 'rxjs'

type ModalOptions = {
	title?: string
	buttonSubmit?: {
		on?: boolean
		text?: string
	},
	buttonClose?: {
		on?: boolean
		text?: string
	}
	context?: any
}

@Injectable({
	providedIn: 'root'
})
export class ModalService {

	public title: string
	private modalNotifier?: Subject<{
		isConfirm: boolean,
		isCancel: boolean
	}>

	constructor(
		private viewContainerRef: ViewContainerRef,
		private injector: Injector,
		@Inject(DOCUMENT) private document: Document
	) {
	}

	open(content: TemplateRef<any>, options?: ModalOptions) {

		const contentViewRef = this.viewContainerRef.createEmbeddedView(content, {
			$implicit: options?.context,
		})

		const modalComponent =
			this.viewContainerRef.createComponent(ModalComponent, {
				injector: this.injector,
				projectableNodes: [contentViewRef.rootNodes]
			})

		modalComponent.instance.title = options?.title

		modalComponent.instance.closeEvent.subscribe((data) => this.closeModal(data))

		modalComponent.hostView.detectChanges()

		this.document.body.appendChild(modalComponent.location.nativeElement)

		this.modalNotifier = new Subject()
		return this.modalNotifier?.asObservable()
	}

	closeModal(data: any) {
		this.modalNotifier?.next(data)
		this.modalNotifier?.complete()
	}
}
