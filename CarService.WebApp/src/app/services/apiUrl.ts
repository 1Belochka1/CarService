const API_URL = 'http://localhost:5053/api/'
const USER_URL = API_URL + 'users'
const RECORD_URL = API_URL + 'records'

export const ApiUrls = {
	users: {
		login: `${USER_URL}/login`,
		getByCookie: `${USER_URL}/getByCookie`,
		getClients: `${USER_URL}/clients`,
		getWorkers: `${USER_URL}/workers`,
		getWorker: `${USER_URL}/worker/`,
		getClient: `${USER_URL}/client/`,
	},
	records: {
		getCompletedByMasterId: `${RECORD_URL}/getCompletedByMasterId/`,
		getActiveByMasterId: `${RECORD_URL}/getActiveByMasterId/`,
	},
}
