import {HttpParams} from '@angular/common/http'

export enum SortClientProperty {
	EMAIL = "email",
	FULLNAME = "fullname",
	ADDRESS = 'address',
	PHONE = "phone",
	LASTRECORD = "lastrecord",
}

export class GetClientsParamsRequest {

	value: HttpParams

	constructor(
		searchValue: string | null,
		page: number | null,
		pageSize: number | null,
		sortProperty: SortClientProperty | null,
		sortDescending: boolean | null
	) {

		this.value = new HttpParams()

		if (searchValue != null)
			this.value = this.value.append('SearchValue', searchValue)

		if (page != null)
			this.value = this.value.append('Page', page)

		if (pageSize != null)
			this.value = this.value.append('PageSize', pageSize)

		if (sortProperty != null)
			this.value = this.value.append('SortProperty', sortProperty)

		if (sortDescending != null)
			this.value = this.value.append('SortDescending', sortDescending)
	}
}