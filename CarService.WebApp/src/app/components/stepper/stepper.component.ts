import {Component} from '@angular/core'
import {NgClass, NgTemplateOutlet} from '@angular/common'
import {CdkStepper, CdkStepperModule} from '@angular/cdk/stepper'

@Component({
	selector: 'app-stepper',
	standalone: true,
	imports: [
		NgTemplateOutlet,
		CdkStepperModule,
		NgClass
	],
	templateUrl: './stepper.component.html',
	styleUrl: './stepper.component.scss',
	providers: [{provide: CdkStepper, useExisting: StepperComponent}],
})
export class StepperComponent extends CdkStepper {
	selectStepByIndex(index: number): void {
		this.selectedIndex = index
	}
}
