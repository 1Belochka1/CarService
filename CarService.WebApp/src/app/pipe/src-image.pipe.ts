import {Pipe, PipeTransform} from '@angular/core'
import {apiUrls} from '../services/apiUrl'

@Pipe({
	name: 'srcImage',
	standalone: true
})
export class SrcImagePipe implements PipeTransform {

	transform(value: string | undefined, ...args: unknown[]): string | undefined {
		return value ?
			apiUrls.images.getById + value
			: undefined
	}

}
