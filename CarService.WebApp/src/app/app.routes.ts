import {Routes} from '@angular/router'
import {AuthPageComponent} from './pages/auth-page/auth-page.component'
import {AccountPageComponent} from './pages/account-page/account-page.component'
import {StatisticsPageComponent} from './pages/account-page/admin/statistics-page/statistics-page.component'
import {WorkersPageComponent} from './pages/account-page/admin/workers-page/workers-page.component'
import {ServicesPageComponent} from './pages/account-page/admin/services-page/services-page.component'
import {WorkerPageComponent} from './pages/account-page/admin/workers-page/worker-page/worker-page.component'
import {ClientsPageComponent} from './pages/account-page/admin/clients-page/clients-page.component'
import {RequestsPageComponent} from './pages/account-page/admin/requests-page/requests-page.component'


export const routes: Routes = [
	{
		path: 'auth',
		component: AuthPageComponent,
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
				path: 'services',
				component: ServicesPageComponent,
				title: 'Личный кабинет - Услуги'
			},
			{
				path: 'worker/:id',
				component: WorkerPageComponent,
			},
			{
				path: 'clients',
				component: ClientsPageComponent,
				title: 'Личный кабинет - Клиенты'
			},
			{
				path: 'requests',
				component: RequestsPageComponent,
				title: 'Личный кабинет - Заявки'
			}
		]
	},
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'account/statistics'
	}
]
