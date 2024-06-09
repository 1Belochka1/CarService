import {UserType} from './user.type'

export type UserInfo = {
	id: string
	email: string
	lastName: string
	firstName: string
	patronymic: string
	address: string
	phone: string
	userAuth: UserType
}