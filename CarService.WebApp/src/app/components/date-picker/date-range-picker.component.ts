import {ChangeDetectorRef, Component, EventEmitter, Output} from '@angular/core'
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
import {MatButton} from "@angular/material/button";
import {
	MatDatepicker,
	MatDatepickerInput,
	MatDatepickerToggle,
	MatDateRangeInput,
	MatDateRangePicker
} from "@angular/material/datepicker";
import {MatError, MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

@Component({
	selector: 'app-date-picker',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		NgIf,
		MatInput,
		MatError,
		MatFormField,
		MatLabel,
		MatButton,
		MatDateRangePicker,
		MatDatepickerToggle,
		MatDateRangeInput,
		MatSuffix,
		MatDatepickerInput,
		MatDatepicker
	],
	templateUrl: './date-range-picker.component.html',
	styleUrl: './date-range-picker.component.scss'
})
export class DateRangePickerComponent {
	dateTimeRangeForm: FormGroup;

	@Output() subFill = new EventEmitter<any>();

	constructor(private fb: FormBuilder, private cd: ChangeDetectorRef) {
	}

	ngOnInit() {
		this.dateTimeRangeForm = this.fb.group({
				startDate: ['', Validators.required],
				endDate: ['', Validators.required],
				startTime: ['00:00', Validators.required],
				endTime: ['00:00', Validators.required],
				breakStartTime: [''],
				breakEndTime: [''],
				duration: [10, [Validators.required, Validators.min(10)]]
			},
			{
				validators: [this.dateRangeValidator(), this.timeRangeValidator()],
				updateOn: "blur"
			});
	}

	onSubmit() {
		if (this.dateTimeRangeForm.valid) {
			this.subFill.emit(this.dateTimeRangeForm.value);
		}
	}

	timeRangeValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const startTime = control.get('startTime')?.value
			const endTime = control.get('endTime')?.value
			if (startTime && endTime && startTime > endTime) {
				const error = {timeRangeInvalid: true}
				control.get('endTime')?.setErrors(error)
				return error
			}
			return null
		}
	}

	dateRangeValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const startDate = control.get('startDate')?.value
			const endDate = control.get('endDate')?.value
			const today = new Date()

			if (startDate && new Date(startDate) <= today) {
				const error = {startDateInvalid: true}
				control.get('startDate')?.setErrors(error)
				return error
			}

			if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
				const error = {dateRangeInvalid: true}
				control.get('endDate')?.setErrors(error)
				return error
			}
			return null
		}
	}
}
