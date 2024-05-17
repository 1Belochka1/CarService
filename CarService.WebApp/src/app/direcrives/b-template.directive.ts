import {
  Directive,
  ElementRef,
  Input,
  TemplateRef
} from '@angular/core'

@Directive({
  selector: '[bTemplate]',
  standalone: true
})
export class BTemplateDirective {

  @Input()
  bTemplate = ""

  constructor(
    public host: ElementRef<HTMLElement>,
    public template: TemplateRef<HTMLElement>,
  ) {
  }

}
