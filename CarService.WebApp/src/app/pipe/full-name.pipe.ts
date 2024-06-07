import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
	name: 'fullName',
	standalone: true
})
export class FullNamePipe implements PipeTransform {

	transform(value: any): string {
		let result: string = ''

		if (value.lastName)
			result += value.lastName + ' '

		result += value.firstName

		if (value.patronymic)
			result += ' ' + value.patronymic

		return result
	}

}
