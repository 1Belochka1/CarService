import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
	name: 'fullName',
	standalone: true
})
export class FullNamePipe implements PipeTransform {

	transform(value: any): unknown {
		return value.lastName + ' ' + value.firstName + ' ' + value.patronymic
	}

}
