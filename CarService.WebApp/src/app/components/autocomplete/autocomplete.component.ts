import {Component, EventEmitter, Input, Output} from '@angular/core'
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms'
import {
	debounceTime,
	distinctUntilChanged,
	Observable,
	of,
	switchMap
} from 'rxjs'
import {
	MatAutocomplete,
	MatAutocompleteTrigger,
	MatOption
} from '@angular/material/autocomplete'
import {AsyncPipe, NgForOf, NgIf} from '@angular/common'
import {MatFormField, MatInput} from '@angular/material/input'

@Component({
	selector: 'app-autocomplete',
	standalone: true,
	imports: [
		MatAutocomplete,
		MatOption,
		NgForOf,
		AsyncPipe,
		MatAutocompleteTrigger,
		ReactiveFormsModule,
		MatInput,
		MatFormField,
		NgIf
	],
	templateUrl: './autocomplete.component.html',
	styleUrl: './autocomplete.component.scss'
})
export class AutocompleteComponent {

	@Input() suggestions: { v: string, n: string }[]

	@Output() selectSuggestion = new EventEmitter<{ v: string, n: string }>()

	autocompleteForm: FormGroup

	filteredSuggestions: { v: string, n: string }[] = []

	constructor(private fb: FormBuilder) {
		this.autocompleteForm = this.fb.group({
			search: ['']
		})
	}

	ngOnInit() {
		this.autocompleteForm.get('search')!.valueChanges.pipe(
			debounceTime(300),
			distinctUntilChanged(),
			switchMap(term => this.getSuggestions(term))
		).subscribe(suggestions => {
			this.filteredSuggestions = suggestions
		})
	}

	onSelectSuggestion(suggestion: { v: string, n: string }) {
		this.selectSuggestion.emit(suggestion)
		this.autocompleteForm.get('search')!.setValue(suggestion.n, {emitEvent: false})
		this.filteredSuggestions = []
	}


	getSuggestions(term: string): Observable<{ v: string, n: string }[]> {
		const filtered = this.suggestions.filter(s => s.n.toLowerCase().includes(term.toLowerCase()))
		return of(filtered)
	}
}
