import {Component, Input} from '@angular/core'
import {CustomInputComponent} from '../custom-input/custom-input.component'
import {FormBuilder, FormGroup} from '@angular/forms'

@Component({
  selector: 'app-form-record',
  standalone: true,
  imports: [
    CustomInputComponent
  ],
  templateUrl: './form-record.component.html',
  styleUrl: './form-record.component.scss'
})
export class FormRecordComponent {
  @Input()
  header: string

  form: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      name: [''],
      login: ['']
    })
  }
}
