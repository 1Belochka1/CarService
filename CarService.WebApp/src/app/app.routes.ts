import {Routes} from '@angular/router'
import {authGuard} from './guard/auth.guard'
import {AccountPageComponent} from './pages/account-page/account-page.component'
import {CalendarPageComponent} from './pages/account-page/admin/calendars/calendar-page/calendar-page.component'
import {CalendarsPageComponent} from './pages/account-page/admin/calendars/calendars-page/calendars-page.component'
import {DayPageComponent} from './pages/account-page/admin/calendars/day-page/day-page.component'
import {ClientsPageComponent} from './pages/account-page/admin/clients-page/clients-page.component'
import {ServicePageComponent} from './pages/account-page/admin/services-page/service-page/service-page.component'
import {ServicesPageComponent} from './pages/account-page/admin/services-page/services-page.component'
import {WorkerPageComponent} from './pages/account-page/admin/workers-page/worker-page/worker-page.component'
import {WorkersPageComponent} from './pages/account-page/admin/workers-page/workers-page.component'
import {ProfilePageComponent} from './pages/account-page/profile-page/profile-page.component'
import {RecordPageComponent} from './pages/account-page/records-page/record-page/record-page.component'
import {RecordsPageComponent} from './pages/account-page/records-page/records-page.component'
import {AuthPageComponent} from './pages/auth-page/auth-page.component'
import {DayRecordLendingComponent} from './pages/day-record-lending/day-record-lending.component'
import {LendingPageComponent} from './pages/lending-page/lending-page.component'

export const routes: Routes = [
	{
		path: 'lending',
		component: LendingPageComponent,
		title: 'Автодоктор',
	},
	{
		path: 'day-record-lending/:calendarId',
		component: DayRecordLendingComponent,
		title: 'Автодоктор',
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
		data: {role: [1, 2, 3]},
		children: [
			{
				path: 'profile',
				component: ProfilePageComponent,
				title: 'Профиль',
				canActivate: [authGuard],

			},
			{
				path: 'workers',
				component: WorkersPageComponent,
				title: 'Сотрудники',
				data: {role: [1]},
				canActivate: [authGuard],

			},
			{
				path: 'services',
				component: ServicesPageComponent,
				title: 'Услуги',
				data: {role: [1]},
				canActivate: [authGuard],

			},
			{
				path: 'services/:serviceId',
				component: ServicePageComponent,
				title: 'Услуга',
				data: {role: [1]},
				canActivate: [authGuard],

			},
			{
				path: 'calendars',
				component: CalendarsPageComponent,
				title: 'Расписание',
				data: {role: [1, 2]},
				canActivate: [authGuard],

			},
			{
				path: 'calendar/:calendarId',
				component: CalendarPageComponent,
				title: 'Расписание',
				data: {role: [1, 2]},
				canActivate: [authGuard],

			},
			{
				path: 'calendar/:calendarId/:dayId',
				component: DayPageComponent,
				title: 'Расписание',
				data: {role: [1, 2]},
				canActivate: [authGuard],

			},
			{
				path: 'worker/:id',
				component: WorkerPageComponent,
				title: 'Работник',
				data: {role: [1]},
				canActivate: [authGuard],

			},
			{
				path: 'clients',
				component: ClientsPageComponent,
				title: 'Клиенты',
				canActivate: [authGuard],
				data: {role: [1]},
			},
			{
				path: 'records',
				component: RecordsPageComponent,
				title: 'Заявки',
				canActivate: [authGuard],
				data: {role: [1, 2, 3]},
			},
			{
				path: 'record/:id',
				component: RecordPageComponent,
				title: 'Заявка',
				canActivate: [authGuard],
				data: {role: [1, 2, 3]},
			}, {
				path: '',
				pathMatch: 'full',
				redirectTo: 'profile',
			},
		],
	},
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'lending',
	},
]
