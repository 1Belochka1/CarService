import {
	AfterContentInit,
	Component,
	ContentChildren,
	QueryList, TemplateRef, ViewContainerRef
} from '@angular/core'
import {TabsContentComponent} from './tabs-content/tabs-content.component'
import {
	NgComponentOutlet,
	NgForOf,
	NgIf, NgTemplateOutlet
} from '@angular/common'

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

	items: {
		head: string,
		template: TemplateRef<any>,
		component: TabsContentComponent
	}[] = []

	activeTab: TabsContentComponent

	@ContentChildren(TabsContentComponent)
	tabs: QueryList<TabsContentComponent>

	ngAfterContentInit(): void {
		this.tabs.forEach(tab => {
			if (tab.selected)
				this.activeTab = tab

			this.items.push({
				head: tab.header,
				template: tab.template,
				component: tab
			})
			console.log(tab)
		})

	}


	open(tab: TabsContentComponent) {
		this.activeTab.selected = false

		tab.selected = true

		this.activeTab = tab

		console.log(this.items)
	}
}
