import {Routes} from '@angular/router'
import {AuthPageComponent} from './pages/auth-page/auth-page.component'
import {AccountPageComponent} from './pages/account-page/account-page.component'
import {StatisticsPageComponent} from './pages/account-page/admin/statistics-page/statistics-page.component'
import {WorkersPageComponent} from './pages/account-page/admin/workers-page/workers-page.component'
import {ServicesPageComponent} from './pages/account-page/admin/services-page/services-page.component'
import {WorkerPageComponent} from './pages/account-page/admin/workers-page/worker-page/worker-page.component'
import {ClientsPageComponent} from './pages/account-page/admin/clients-page/clients-page.component'
import {RecordsPageComponent} from './pages/account-page/records-page/records-page.component'
import {RecordPageComponent} from './pages/account-page/records-page/record-page/record-page.component'


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
				path: 'records',
				component: RecordsPageComponent,
				title: 'Личный кабинет - Заявки'
			},
			{
				path: 'record/:id',
				component: RecordPageComponent,
				title: 'Личный кабинет - Заявка'
			}
		]
	},
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'account/statistics'
	}
]
