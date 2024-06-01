import {TimeString} from './DayRecord.type'

export type TimeRecord = {
	id: string;
	dayRecordId: string;
	recordId: string | null;
	clientId: string | null;
	startTime: TimeString;
	endTime: TimeString;
	isBusy: boolean;
	dayRecord: any;
	record: any;
	client: any;
}