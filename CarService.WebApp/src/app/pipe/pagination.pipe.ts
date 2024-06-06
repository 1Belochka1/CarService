import {ChangeDetectorRef, Pipe, PipeTransform} from '@angular/core'

@Pipe({
  name: 'pagination',
  standalone: true
})
export class PaginationPipe implements PipeTransform {

  constructor(private cd: ChangeDetectorRef) {
  }

  transform(items: any[], page: number = 1, pageSize: number = 10, update: boolean): any[] {

    if (!items || items.length === 0) {
      return [];
    }

    const startIndex = (page - 1) * pageSize;
    const endIndex = startIndex + pageSize;
    return items.slice(startIndex, endIndex);
  }

}
