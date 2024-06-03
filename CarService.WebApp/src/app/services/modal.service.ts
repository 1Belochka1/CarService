import {
	ComponentRef,
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
	context?: any,
	actionVisible?: boolean
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

	private isOpen: boolean = false

	private _modalComponent: ComponentRef<ModalComponent>

	constructor(
		private viewContainerRef: ViewContainerRef,
		private injector: Injector,
		@Inject(DOCUMENT) private document: Document
	) {
	}

	open(content: TemplateRef<any>, options?: ModalOptions) {
		if (this.isOpen)
			return

		const contentViewRef = this.viewContainerRef.createEmbeddedView(content, {
			$implicit: options?.context,
		})

		this._modalComponent =
			this.viewContainerRef.createComponent(ModalComponent, {
				injector: this.injector,
				projectableNodes: [contentViewRef.rootNodes]
			})

		this._modalComponent.instance.title = options?.title

		this._modalComponent.instance.actionVisible = options?.actionVisible ?? true

		this._modalComponent.instance.closeEvent.subscribe((data) => this.closeModal(data))

		this._modalComponent.hostView.detectChanges()

		this.document.body.appendChild(this._modalComponent.location.nativeElement)

		this.isOpen = true

		this.modalNotifier = new Subject()

		return this.modalNotifier?.asObservable()
	}

	closeModal(data: {
		isConfirm: boolean,
		isCancel: boolean
	}) {
		this.modalNotifier?.next(data)
		this.modalNotifier?.complete()
		this._modalComponent.destroy()
		this.isOpen = false
	}
}
