import {Routes} from '@angular/router'
import {AuthPageComponent} from './pages/auth-page/auth-page.component'
import {AccountPageComponent} from './pages/account-page/account-page.component'
import {WorkersPageComponent} from './pages/account-page/admin/workers-page/workers-page.component'
import {ServicesPageComponent} from './pages/account-page/admin/services-page/services-page.component'
import {WorkerPageComponent} from './pages/account-page/admin/workers-page/worker-page/worker-page.component'
import {ClientsPageComponent} from './pages/account-page/admin/clients-page/clients-page.component'
import {RecordsPageComponent} from './pages/account-page/records-page/records-page.component'
import {RecordPageComponent} from './pages/account-page/records-page/record-page/record-page.component'
import {LendingPageComponent} from './pages/lending-page/lending-page.component'
import {ShopPageComponent} from './pages/lending-page/shop-page/shop-page.component'
import {CartPageComponent} from './pages/lending-page/cart-page/cart-page.component'
import {authGuard} from './guard/auth.guard'
import {MainPageComponent} from './pages/lending-page/main-page/main-page.component'


export const routes: Routes = [
	{
		path: 'lending',
		component: LendingPageComponent,
		children: [
			{
				path: '',
				component: MainPageComponent
			},
			{
				path: 'shop',
				component: ShopPageComponent,
			},
			{
				path: 'cart',
				component: CartPageComponent,
			},
			{
				path: 'record',
				component: MainPageComponent
			},
			{
				path: 'client',
				component: MainPageComponent
			}
		]
	},

	{
		path: 'auth',
		component: AuthPageComponent,
		canActivate: [authGuard],
	},
	{
		path: 'account',
		component: AccountPageComponent,
		canActivate: [authGuard],
		children: [
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
		redirectTo: 'lending'
	}
]
