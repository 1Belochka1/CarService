import {HttpParams} from '@angular/common/http'

export enum SortMastersProperty {
	EMAIL = "Email",
	LASTNAME = "LastName",
	FIRSTNAME = "FirstName",
	PATRONYMIC = "Patronymic",
	ADDRESS = 'Address',
	PHONE = "Phone"
}

export class GetListWithPageRequest {

	value: HttpParams

	constructor(
		searchValue: string | null,
		page: number | null,
		pageSize: number | null,
		sortProperty: string | null,
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

export class GetListWithPageAndFilterRequest {

	value: HttpParams

	constructor(
		searchValue: string | null,
		page: number | null,
		pageSize: number | null,
		sortProperty: string | null,
		sortDescending: boolean | null,
		filterName: string | null,
		filterValue: string | null
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

		if (filterName != null)
			this.value = this.value.append("FilterProperty", filterName)

		if (filterValue != null)
			this.value = this.value.append('FilterValue', filterValue)
	}
}