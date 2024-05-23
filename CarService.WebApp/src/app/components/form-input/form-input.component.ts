import {Component, Input} from '@angular/core'
import {
	FormControl,
	ReactiveFormsModule
} from '@angular/forms'
import {NgClass} from '@angular/common'

@Component({
  selector: 'app-form-input',
  standalone: true,
	imports: [
		ReactiveFormsModule,
		NgClass
	],
  templateUrl: './form-input.component.html',
  styleUrl: './form-input.component.scss'
})
export class FormInputComponent {
	@Input({required: true})
	type: string = 'text'

	@Input({required: true})
	formControl: FormControl

	@Input()
	title: string
}
