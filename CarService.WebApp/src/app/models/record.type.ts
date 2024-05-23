import {Priority} from '../enums/priority.enum'
import {Status} from '../enums/status.enum'

export type RecordType = {
	id: string,
	clientId: string,
	client: null,
	description: string,
	createTime: Date,
	completeTime: null,
	priority: Priority,
	status: Status,
	services: [],
	masters: []
}