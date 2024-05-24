import {Priority} from '../enums/priority.enum'
import {Status} from '../enums/status.enum'
import {UserType} from './user.type'

export type RecordType = {
	id: string,
	clientId: string,
	client: UserType,
	description: string,
	createTime: Date,
	completeTime: null,
	priority: Priority,
	status: Status,
	services: [],
	masters: []
}