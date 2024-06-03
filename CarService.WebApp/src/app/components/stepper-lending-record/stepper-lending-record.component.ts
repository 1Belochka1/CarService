import {Component} from '@angular/core'
import {NgClass, NgTemplateOutlet} from '@angular/common'
import {CdkStepper, CdkStepperModule} from '@angular/cdk/stepper'

@Component({
	selector: 'app-stepper-lending-record',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		CdkStepperModule,
		NgClass
	],
	templateUrl: './stepper-lending-record.component.html',
	styleUrl: './stepper-lending-record.component.scss',
	providers: [{provide: CdkStepper, useExisting: StepperLendingRecordComponent}],
})
export class StepperLendingRecordComponent extends CdkStepper {
	selectStepByIndex(index: number): void {
		this.selectedIndex = index
	}
}
