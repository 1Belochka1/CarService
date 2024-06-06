import {Pipe, PipeTransform} from '@angular/core'

@Pipe({
  name: 'sorting',
  standalone: true
})
export class SortingPipe implements PipeTransform {

  transform(value: any[], index: number): any[] {
    if (!Array.isArray(value) || index == undefined || index < 0) {
      console.warn('Invalid input:', value, index)
      return value
    }

    return value.sort((a, b) => {
      try {
        const keysA = Object.keys(a)
        const keysB = Object.keys(b)

        if (index >= keysA.length || index >= keysB.length) {
          console.warn('Index out of bounds:', index)
          return 0
        }

        const keyA = keysA[index]
        const keyB = keysB[index]

        const valA = a[keyA]
        const valB = b[keyB]

        if (valA == undefined || valB == undefined) {
          console.warn('Undefined values encountered:', valA, valB)
          return 0
        }

        // console.log('Comparing:', keyA, keyB, valA, valB)

        // Handle non-numeric values
        if (typeof valA === 'number' && typeof valB === 'number') {
          return valA - valB
        } else if (typeof valA === 'string' && typeof valB === 'string') {
          return valA.localeCompare(valB)
        } else {
          console.warn('Incompatible types for comparison:', typeof valA, typeof valB)
          return 0
        }

      } catch (error) {
        console.error('Error during sorting:', error)
        return 0
      }
    })
  }

}
