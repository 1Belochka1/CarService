import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
  name: 'distinct',
  standalone: true
})
export class DistinctPipe implements PipeTransform {

  transform(value: any): any {
		console.log(value)

		const uniqueHeaderNames = new Set<string>();

		return value.filter((v: any) => {
			if (!uniqueHeaderNames.has(v.headerName)) {
				uniqueHeaderNames.add(v.headerName);
				return true;
			}
			return false;
		});
  }

}
