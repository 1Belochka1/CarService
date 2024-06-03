const API_URL = 'http://localhost:5053/api/'
// const API_URL = 'https://belka.local.com/api/'

const USER_URL = API_URL + 'users'
const RECORD_URL = API_URL + 'records'
const CALENDAR_URL = RECORD_URL + '/calendars'
const DAYRECORD_URL = RECORD_URL + '/dayRecords'
const TIMERECORD_URL = RECORD_URL + '/TimeRecords'
const SERVICE_URL = API_URL + 'services'

export const apiUrls = {
	users: {
		login: `${USER_URL}/login`,
		register: `${USER_URL}/register`,
		getByCookie: `${USER_URL}/get/ByCookie`,
		getIsAuth: `${USER_URL}/get/isAuth`,
		getClients: `${USER_URL}/get/clients`,
		getWorkers: `${USER_URL}/get/workers`,
		getWorker: `${USER_URL}/get/worker/`,
		getClient: `${USER_URL}/get/client/`,
	},
	records: {
		create: `${RECORD_URL}/create`,
		update: `${RECORD_URL}/update`,
		getById: `${RECORD_URL}/get/`,
		getAll: `${RECORD_URL}/get/All`,
		getCompletedByMasterId: `${RECORD_URL}/getCompletedByMasterId/`,
		getActiveByMasterId: `${RECORD_URL}/getActiveByMasterId/`,
	},
	calendars: {
		create: `${CALENDAR_URL}/create`,
		getAll: `${CALENDAR_URL}/get/all`,
		getById: `${CALENDAR_URL}/get/`,
		update: `${CALENDAR_URL}/update`,
		delete: `${CALENDAR_URL}/delete/`,
	},
	dayRecords: {
		fill: `${DAYRECORD_URL}/Fill`,
		getByCalendarId: `${DAYRECORD_URL}/Get/ByCalendarId/`,
		// getById: `${DAYRECORD_URL}/get/`,
		// update: `${DAYRECORD_URL}/update`,
		// delete: `${DAYRECORD_URL}/delete/`,
	},
	timeRecords: {
		getAllByDayRecordId: `${TIMERECORD_URL}/Get/ByRecordId/`,
	},
	services: {
		get: `${SERVICE_URL}`,
		getLending: `${SERVICE_URL}/lending`,
		create: `${SERVICE_URL}`,
	}
}

export const apiHubUrls = {
	dayRecords: API_URL + 'hubs' + '/dayrecords',
	timeRecords: API_URL + 'hubs' + '/timerecords'
}