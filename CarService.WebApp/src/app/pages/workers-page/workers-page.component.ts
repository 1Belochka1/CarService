import {Component} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {SvgIconComponent} from "angular-svg-icon";
import {NgForOf} from "@angular/common";
import {workers} from "./workers";
import {ITableProp, TableComponent} from "../../components/table/table.component";
import {IUserTable} from "../../services/auth.service";


@Component({
	selector: 'app-workers-page',
	standalone: true,
	imports: [
		FormsModule,
		SvgIconComponent,
		NgForOf,
		TableComponent
	],
	templateUrl: './workers-page.component.html',
	styleUrl: './workers-page.component.scss'
})
export class WorkersPageComponent {
	search: string = '';

	tableProp: ITableProp<IUserTable>[] = [
		{
			name: 'ФИО',
			key: 'fullname'
		},
		{
			name: 'Email',
			key: 'email'
		},
		{
			name: 'Адрес',
			key: 'address'
		},
		{
			name: 'Телефон',
			key: 'phone'
		},
		{
			name: 'Должность',
			key: 'role'
		}
	];

	workers: IUserTable[] = workers.map<IUserTable>(worker => {
		return {
			email: worker.email,
			fullname: `${worker.userInfo.lastname} ${worker.userInfo.firstname} ${worker.userInfo.patronymic}`,
			address: worker.userInfo.address,
			phone: worker.userInfo.phone,
			role: worker.role.name
		}
	})

	constructor() {

	}
}
