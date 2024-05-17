import {
	Component,
	Input,
	TemplateRef, ViewChild,
	ViewContainerRef
} from '@angular/core'
import {NgClass} from '@angular/common'

@Component({
  selector: 'app-tabs-content',
  standalone: true,
	imports: [
		NgClass
	],
  templateUrl: './tabs-content.component.html',
  styleUrl: './tabs-content.component.scss'
})
export class TabsContentComponent {

	@Input()
	public header: string = ''

	@Input()
	public selected: boolean = false

	@ViewChild('tmp')
	public template: TemplateRef<any>
}
