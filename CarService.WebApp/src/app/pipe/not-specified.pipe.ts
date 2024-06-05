import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
	name: 'notSpecified',
	standalone: true
})
export class NotSpecifiedPipe implements PipeTransform {

	transform(value: unknown): unknown {

		if (value == null || value == '')
			return 'Не указано'

		return value
	}

}
