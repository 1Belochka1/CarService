import {UserInfo} from './user-info.type'

export type UserType = {
	id: string
	email: string
	createDate: string
	roleId: number
	role: any
	userInfoId: string
	userInfo: UserInfo
	records: any[]
	works: any[]
}
