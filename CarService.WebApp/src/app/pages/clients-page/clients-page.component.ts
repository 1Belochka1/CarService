import {Component} from '@angular/core';
import {TableComponent} from "../../components/table/table.component";

@Component({
	selector: 'app-clients-page',
	standalone: true,
	imports: [TableComponent],
	templateUrl: './clients-page.component.html',
	styleUrl: './clients-page.component.scss'
})
export class ClientsPageComponent {
	clients = [
		{
			name: 'Клиент ',
			email: 'kXGZU@example.com',
			phone: '8-999-999-99-99',
			address: 'ул. Строителей 1 д. 1 к. 1',
			car: 'Автомобиль ',
			requests: 'Заявки '
		}
	];

	constructor() {
		for (let i = 0; i < 100; i++) {
			this.clients.push({
				name: 'Клиент ' + i,
				email: 'kXGZU@example.com',
				phone: '8-999-999-99-99',
				address: 'ул. Строителей 1 д. 1 к. 1',
				car: 'Автомобиль ' + i,
				requests: 'Заявки ' + i
			})
		}
	}
}
