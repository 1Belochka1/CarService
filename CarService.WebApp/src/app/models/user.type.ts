import {UserInfo} from './user-info.type'

export type UserType = {
	id: string
	phone: string
	createDate: string
	roleId: number
	role: any
	userInfo: UserInfo
	records: any[]
	services: any[]
	works: any[]
	chats: any[]
	messages: any[]
}
