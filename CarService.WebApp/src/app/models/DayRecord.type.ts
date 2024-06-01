import {TimeRecord} from './TimeRecord.type'

export type DayRecord = {
	id: string;
	calendarId: string;
	date: Date;
	startTime: TimeString;
	endTime: TimeString;
	offset: number;
	isWeekend: boolean;
	calendar: any;
	timeRecords: TimeRecord[];
}

export type TimeString = `${number}:${number}:${number}`;