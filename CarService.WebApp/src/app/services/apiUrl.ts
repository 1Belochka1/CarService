import { environment } from '../../environments/environment'

const API_URL = environment.api

const USER_URL = API_URL + 'users'
const RECORD_URL = API_URL + 'records'
const CALENDAR_URL = RECORD_URL + '/calendars'
const DAYRECORD_URL = RECORD_URL + '/dayRecords'
const TIMERECORD_URL = RECORD_URL + '/TimeRecords'
const SERVICE_URL = API_URL + 'services'

export const apiUrls = {
	users: {
		login: `${USER_URL}/login`,
		logout: `${USER_URL}/logout`,
		register: `${USER_URL}/register`,
		registerMaster: `${USER_URL}/register/master`,
		getByCookie: `${USER_URL}/get/ByCookie`,
		getRoleId: `${USER_URL}/get/roleid`,
		getClients: `${USER_URL}/get/clients`,
		getWorkers: `${USER_URL}/get/workers`,
		getWorkersAutocomplete: `${USER_URL}/get/workers/autocomplete`,
		getWorker: `${USER_URL}/get/worker/`,
		getClient: `${USER_URL}/get/client/`,
		updateByPhone: `${USER_URL}/update/byPhone`,
		update: `${USER_URL}/update`,
		updatePassword: `${USER_URL}/update/password`,
		dismissById: `${USER_URL}/delete/dismiss/`,
		delete: `${USER_URL}/delete/`,
		updateToMasterById: `${USER_URL}/update/toMaster`,

	},
	records: {
		create: `${RECORD_URL}/create`,
		createWithOutAuth: `${RECORD_URL}/create/WithoutUserAuth`,
		update: `${RECORD_URL}/update`,
		getById: `${RECORD_URL}/get/`,
		getAll: `${RECORD_URL}/get/All`,
		getCompletedByMasterId: `${RECORD_URL}/get/CompletedByMasterId/`,
		getActiveByMasterId: `${RECORD_URL}/get/ActiveByMasterId/`,
		addMaster: `${RECORD_URL}/update/addmaster/`,
		delete: `${RECORD_URL}/delete/`,
		deleteMaster:`${RECORD_URL}/Update/DeleteMaster/`,

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
		getById: `${DAYRECORD_URL}/get/`,
		// update: `${DAYRECORD_URL}/update`,
		delete: `${DAYRECORD_URL}/delete/`,
	},
	timeRecords: {
		getAllByDayRecordId: `${TIMERECORD_URL}/Get/ByRecordId/`,
		delete: `${TIMERECORD_URL}/delete/`,
		update: `${TIMERECORD_URL}/update/`,
	},
	services: {
		getAll: `${SERVICE_URL}/get/all`,
		getById: `${SERVICE_URL}/get/`,
		getLending: `${SERVICE_URL}/get/lending`,
		create: `${SERVICE_URL}/create`,
		update: `${SERVICE_URL}/update`,
		delete: `${SERVICE_URL}/delete/`,
		getServicesAutocomplete: `${SERVICE_URL}/get/autocomplete`,
	},
	images: {
		getById: `${API_URL}images/get/`,
		upload: `${API_URL}images/upload`,
		update: `${API_URL}images/update`,
		delete: `${API_URL}images/delete`,
	},
}

export const apiHubUrls = {
	dayRecords: API_URL + 'hubs' + '/dayrecords',
	timeRecords: API_URL + 'hubs' + '/timerecords',
	notify: API_URL + 'hubs' + '/notify',
}
