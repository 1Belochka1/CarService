import {
	AfterContentInit,
	Component,
	ContentChildren, EventEmitter, inject, Output,
	QueryList, Renderer2, TemplateRef, ViewContainerRef
} from '@angular/core'
import {TabsContentComponent} from './tabs-content/tabs-content.component'
import {
	NgComponentOutlet,
	NgForOf,
	NgIf, NgTemplateOutlet
} from '@angular/common'
import {TabsTableContentComponent} from './tabs-table-content/tabs-table-content.component'

@Component({
	selector: 'app-tabs',
	standalone: true,
	imports: [
		NgForOf,
		NgIf,
		NgComponentOutlet,
		NgTemplateOutlet
	],
	templateUrl: './tabs.component.html',
	styleUrl: './tabs.component.scss'
})
export class TabsComponent implements AfterContentInit {

	private renderer = inject(Renderer2)

	@ContentChildren(TabsContentComponent)
	tabs: QueryList<TabsContentComponent>

	@ContentChildren(TabsTableContentComponent)
	tabsTable: QueryList<TabsTableContentComponent>

	@Output()
	tabsChange: EventEmitter<TabsContentComponent | TabsTableContentComponent> = new EventEmitter<TabsContentComponent | TabsTableContentComponent>()

	items: {
		head: string,
		template: TemplateRef<any>,
		component: TabsContentComponent | TabsTableContentComponent
	}[] = []

	activeTab: TabsContentComponent | TabsTableContentComponent


	ngAfterContentInit(): void {
		const t = this.tabs.length == 0 ? this.tabsTable : this.tabs
		t.forEach((tab: TabsContentComponent | TabsTableContentComponent) => {
			if (tab.selected) {
				this.activeTab = tab
				this.tabsChange.emit(this.activeTab)
				this.renderer.setAttribute(tab.elementRef.nativeElement, 'data-selected', 'true')
			} else {
				this.renderer.setAttribute(tab.elementRef.nativeElement, 'data-selected', 'false')
			}

			this.items.push({
				head: tab.header,
				template: tab.template,
				component: tab
			})
			console.log(tab)
		})
	}

	open(tab: TabsContentComponent | TabsTableContentComponent) {
		this.activeTab.selected = false
		this.renderer.setAttribute(this.activeTab.elementRef.nativeElement, 'data-selected', 'false')


		tab.selected = true

		this.activeTab = tab

		this.renderer.setAttribute(this.activeTab.elementRef.nativeElement, 'data-selected', 'true')

		this.tabsChange.emit(this.activeTab)

		console.log(this.items)
	}
}
