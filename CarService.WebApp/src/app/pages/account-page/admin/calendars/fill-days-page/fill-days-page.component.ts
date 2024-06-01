import {Component} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {FillDaysService} from '../../../../../services/day-record/fill-days.service'
import {FormsModule} from '@angular/forms'

@Component({
	selector: 'app-fill-days-page',
	standalone: true,
	imports: [
		FormsModule
	],
	templateUrl: './fill-days-page.component.html',
	styleUrl: './fill-days-page.component.scss',
	providers: [FillDaysService]
})
export class FillDaysPageComponent {
	selected: any

	constructor(
		private _fillDaysService: FillDaysService,
		private _router: ActivatedRoute,
	) {
		const id = this._router.snapshot.paramMap.get('calendarId')

		if (id === null) {
			return
		}


	}
}
