import {
	Component,
	EventEmitter,
	Input,
	Output
} from '@angular/core'
import {FormsModule} from '@angular/forms'
import {SvgIconComponent} from 'angular-svg-icon'
import {BehaviorSubject, debounceTime} from 'rxjs'
import {UsersService} from '../../services/users.service'

@Component({
	selector: 'app-search',
	standalone: true,
	imports: [
		FormsModule,
		SvgIconComponent
	],
	templateUrl: './search.component.html',
	styleUrl: './search.component.scss'
})
export class SearchComponent {

	search: string = ''

	searchSubject: BehaviorSubject<string> = new BehaviorSubject<string>('')

	@Output()
	searchChangedEvent: EventEmitter<string> = new EventEmitter<string>()


	constructor() {
		this.searchSubject
				.pipe(debounceTime(500))
				.subscribe(
					(value) => {
						this.searchChangedEvent.emit(value)
					}
				)
	}

	searchChanged(): void {
		this.searchSubject.next(this.search)
	}
}
