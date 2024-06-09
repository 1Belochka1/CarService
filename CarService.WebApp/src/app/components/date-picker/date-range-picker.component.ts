import {Component, EventEmitter, Output} from '@angular/core'
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	ValidationErrors,
	ValidatorFn,
	Validators
} from '@angular/forms'
import {NgIf} from '@angular/common'

@Component({
	selector: 'app-date-picker',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgIf
	],
	templateUrl: './date-range-picker.component.html',
	styleUrl: './date-range-picker.component.scss'
})
export class DateRangePickerComponent {
	dateTimeRangeForm: FormGroup

	@Output() subFill = new EventEmitter<any>

	constructor(private fb: FormBuilder) {
	}

	ngOnInit() {
		this.dateTimeRangeForm = this.fb.group(
			{
				startDate: ['', Validators.required],
				endDate: ['', Validators.required],
				startTime: ['00:00:00', Validators.required],
				endTime: ['00:00:00', Validators.required],
				breakStartTime: [null],
				breakEndTime: [null],
				duration: [1, Validators.min(10)]
			},
			{
				validators: [this.dateRangeValidator(), this.timeRangeValidator()],
			}
		)
	}

	onSubmit() {
		if (this.dateTimeRangeForm.valid) {
			this.subFill.emit(
				{
					startDate: this.dateTimeRangeForm.get('startDate')?.value,
					endDate: this.dateTimeRangeForm.get('endDate')?.value,
					startTime: this.dateTimeRangeForm.get('startTime')?.value,
					endTime: this.dateTimeRangeForm.get('endTime')?.value,
					breakStartTime: this.dateTimeRangeForm.get('breakStartTime')?.value,
					breakEndTime: this.dateTimeRangeForm.get('breakEndTime')?.value,
					duration: this.dateTimeRangeForm.get('duration')?.value,
				}
			)
		}
	}

	timeRangeValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const startTime = control.get('startTime')?.value
			const endTime = control.get('endTime')?.value
			if (startTime && endTime && startTime > endTime) {
				return {timeRangeInvalid: true}
			}
			return null
		}
	}

	dateRangeValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const startDate = control.get('startDate')?.value
			const endDate = control.get('endDate')?.value
			const today = new Date()
			const tomorrow = new Date(today.setDate(today.getDate() + 1))

			if (startDate && new Date(startDate) < tomorrow) {
				return {startDateInvalid: true}
			}

			if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
				return {dateRangeInvalid: true}
			}
			return null
		}
	}

}
