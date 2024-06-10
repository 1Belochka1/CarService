const folder = 'assets/svg/'

export const menuItems = [
	{
		icon: 'account_circle',
		title: 'Профиль',
		routerLink: 'profile',
		access: 3
	},
	{
		icon: 'people_alt',
		title: 'Сотрудники',
		routerLink: 'workers',
		access: 1
	},
	{
		icon: 'list_alt',
		title: 'Заявки',
		routerLink: 'records',
		access: 3
	},
	{
		icon: 'work',
		title: 'Услуги',
		routerLink: 'services',
		access: 1
	},
	{
		icon: 'event_note',
		title: 'Расписание',
		routerLink: 'calendars',
		access: 2
	},
	{
		icon: 'group',
		title: 'Клиенты',
		routerLink: 'clients',
		access: 1
	},
	{
		icon: 'home',
		title: 'На главную',
		routerLink: '/lending',
		access: 3
	}
]

