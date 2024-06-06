import {Routes} from '@angular/router'
import {AuthPageComponent} from './pages/auth-page/auth-page.component'
import {AccountPageComponent} from './pages/account-page/account-page.component'
import {
	WorkersPageComponent
} from './pages/account-page/admin/workers-page/workers-page.component'
import {
	ServicesPageComponent
} from './pages/account-page/admin/services-page/services-page.component'
import {
	WorkerPageComponent
} from './pages/account-page/admin/workers-page/worker-page/worker-page.component'
import {
	ClientsPageComponent
} from './pages/account-page/admin/clients-page/clients-page.component'
import {
	RecordsPageComponent
} from './pages/account-page/records-page/records-page.component'
import {
	RecordPageComponent
} from './pages/account-page/records-page/record-page/record-page.component'
import {LendingPageComponent} from './pages/lending-page/lending-page.component'
import {authGuard} from './guard/auth.guard'
import {
	CalendarsPageComponent
} from './pages/account-page/admin/calendars/calendars-page/calendars-page.component'
import {
	CalendarPageComponent
} from './pages/account-page/admin/calendars/calendar-page/calendar-page.component'
import {
	FillDaysPageComponent
} from './pages/account-page/admin/calendars/fill-days-page/fill-days-page.component'
import {
	DayPageComponent
} from './pages/account-page/admin/calendars/day-page/day-page.component'
import {
	DayRecordLendingComponent
} from './pages/day-record-lending/day-record-lending.component'


export const routes: Routes = [
	{
		path: 'lending',
		component: LendingPageComponent,
		title: 'Автодоктор'
	},
	{
		path: 'day-record-lending/:calendarId',
		component: DayRecordLendingComponent,
		title: 'Автодоктор'
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
				title: 'Сотрудники'
			},
			{
				path: 'services',
				component: ServicesPageComponent,
				title: 'Услуги'
			},
			{
				path: 'calendars',
				component: CalendarsPageComponent,
				// TODO: ЗАМЕНИТЬ!!!
				title: 'Я еще не придумал'
			},
			{
				path: 'calendar/:calendarId',
				component: CalendarPageComponent,
				// TODO: ЗАМЕНИТЬ!!!
				title: 'Я еще не придумал'
			},
			{
				path: 'calendar/:calendarId/filldays',
				component: FillDaysPageComponent
			},
			{
				path: 'calendar/:calendarId/:dayId',
				component: DayPageComponent
			},
			{
				path: 'worker/:id',
				component: WorkerPageComponent,
				title: 'Работник'
			},
			{
				path: 'clients',
				component: ClientsPageComponent,
				title: 'Клиенты'
			},
			{
				path: 'records',
				component: RecordsPageComponent,
				title: 'Заявки'
			},
			{
				path: 'record/:id',
				component: RecordPageComponent,
				title: 'Заявка'
			}
		]
	},
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'lending'
	}
]
