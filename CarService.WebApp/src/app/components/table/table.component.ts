import {Component, Input, OnInit} from '@angular/core';
import {NgForOf} from "@angular/common";


export interface ITableProp<T> {
	name: string;
	key: keyof T;
}

@Component({
	selector: 'app-table',
	standalone: true,
	imports: [
		NgForOf
	],
	templateUrl: './table.component.html',
	styleUrl: './table.component.scss'
})
export class TableComponent implements OnInit {
	@Input()
	properties: ITableProp<any>[] = []

	@Input({required: true})
	items: any[];

	constructor() {
	}

	ngOnInit(): void {
		
		if (this.properties.length === 0) {
			Object.keys(this.items[0]).forEach(key => {
				this.properties.push({
					name: key,
					key
				})
			})
		}
	}

}
