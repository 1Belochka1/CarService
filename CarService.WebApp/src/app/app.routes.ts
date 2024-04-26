import {Routes} from '@angular/router';
import {AuthPageComponent} from "./pages/auth-page/auth-page.component";
import {AccountPageComponent} from "./pages/account-page/account-page.component";
import {StatisticsPageComponent} from "./pages/statistics-page/statistics-page.component";
import {WorkersPageComponent} from "./pages/workers-page/workers-page.component";
import {ClientsPageComponent} from "./pages/clients-page/clients-page.component";

export const routes: Routes = [
	{
		path: 'auth',
		component: AuthPageComponent
	},
	{
		path: 'account',
		component: AccountPageComponent,
		children: [
			{
				path: 'statistics',
				component: StatisticsPageComponent,
				title: 'Личный кабинет - Общая статистика'
			},
			{
				path: 'workers',
				component: WorkersPageComponent,
				title: 'Личный кабинет - Сотрудники'
			},
			{
				path: 'clients',
				component: ClientsPageComponent,
				title: 'Личный кабинет - Клиенты'
			}
		]
	},
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'account/statistics'
	}
];