import {TemplateRef} from '@angular/core'

export type SliderItem = {
  url?: string
  label?: string
  context?: any
}

export type SliderSettings = {
  template?: TemplateRef<any>
}
