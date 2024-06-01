import {TemplateRef} from '@angular/core'

export type SliderItem = {
  url?: string
  label?: string
  context?: any
  template?: TemplateRef<any>
  templateName?: string
}

export type SliderSettings = {
  template?: TemplateRef<any>
}
