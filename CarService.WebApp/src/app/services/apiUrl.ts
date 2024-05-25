// const API_URL = 'http://localhost:5053/api/'
const API_URL = 'https://belka.local.com/api/'

const USER_URL = API_URL + 'users'
const RECORD_URL = API_URL + 'records'
const SERVICE_URL = API_URL + 'services'

export const apiUrls = {
	users: {
		login: `${USER_URL}/login`,
		getByCookie: `${USER_URL}/getByCookie`,
		getClients: `${USER_URL}/clients`,
		getWorkers: `${USER_URL}/workers`,
		getWorker: `${USER_URL}/worker/`,
		getClient: `${USER_URL}/client/`,
	},
	records: {
		create: `${RECORD_URL}/create`,
		update: `${RECORD_URL}/update`,
		getById: `${RECORD_URL}/`,
		getAll: `${RECORD_URL}/getAll`,
		getCompletedByMasterId: `${RECORD_URL}/getCompletedByMasterId/`,
		getActiveByMasterId: `${RECORD_URL}/getActiveByMasterId/`,
	},
	services: {
		get: `${SERVICE_URL}`,
		getLending: `${SERVICE_URL}/lending`,
		create: `${SERVICE_URL}`,
	}
}
