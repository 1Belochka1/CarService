import {Priority} from '../enums/priority.enum'
import {Status} from '../enums/status.enum'
import {UserInfo} from './user-info.type'

export type RecordType = {
	id: string,
	clientId: string,
	client: UserInfo,
	carInfo?: string
	description: string,
	createTime: Date,
	completeTime: null,
	priority: Priority,
	status: Status,
	services: [],
	masters: any[]
}