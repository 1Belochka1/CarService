import {
	ApplicationRef,
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
	title: string
	buttonSubmit: {
		on?: boolean
		text?: string
	},
	buttonClose: {
		on?: boolean
		text?: string
	}
}

@Injectable({
	providedIn: 'root'
})
export class ModalService {

	public title: string
	private modalNotifier?: Subject<string>

	constructor(
		private appRef: ApplicationRef,
		private viewContainerRef: ViewContainerRef,
		private injector: Injector,
		@Inject(DOCUMENT) private document: Document
	) {
	}

	open(content: TemplateRef<any>, context?: any, options?: ModalOptions) {
		const contentViewRef = content.createEmbeddedView(context, this.injector)

		console.log(contentViewRef)

		const modalComponent =
			this.viewContainerRef.createComponent(ModalComponent, {
				injector: this.injector,
				environmentInjector: this.appRef.injector,
				projectableNodes: [contentViewRef.rootNodes]
			})

		console.log(modalComponent)

		modalComponent.instance.title = 'Заголовок'
		modalComponent.instance.closeEvent.subscribe(() => this.closeModal())
		modalComponent.instance.submitEvent.subscribe(() => this.submitModal())

		modalComponent.hostView.detectChanges()

		console.log(this)


		this.document.body.appendChild(modalComponent.location.nativeElement)
	}

	closeModal() {
		this.modalNotifier?.complete()
	}

	submitModal() {
		this.modalNotifier?.next('confirm')
		this.closeModal()
	}
}
