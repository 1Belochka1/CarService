import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
  name: 'sorting',
  standalone: true
})
export class SortingPipe implements PipeTransform {


  transform(value: any[], prop: string, sortOrder: boolean): any[] {
    // if (!Array.isArray(value) || index == undefined || index < 0) {
    //   console.warn('Invalid input:', value, index)
    //   return value
    // }

    if (prop == '') {
      return value
    }

    return value.sort((a, b) => {

      const valA = a[prop]
      const valB = b[prop]

      // if (valA == undefined || valB == undefined) {
      //   console.warn('Undefined values encountered:', valA, valB)
      //   return 0
      // }

      const dateA = Date.parse(valA)
      const dateB = Date.parse(valB)

      let result: number

      if (!isNaN(dateA) || !isNaN(dateB)) {
        result = (!isNaN(dateA) ? dateA : 1) - (!isNaN(dateB) ? dateB : 0)
        return sortOrder ? result : -result
      }

      if (typeof valA === 'string' && typeof valB === 'string') {
        result = valA.localeCompare(valB)
      } else {
        result = valA - valB
      }

      return !sortOrder ? result : -result
    })
  }

}
