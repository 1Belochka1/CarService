import { Injectable } from '@angular/core';
import {Subject} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class TitleService {

	_title: Subject<string> = new Subject<string>();

  constructor() { }

	getTitle() {
		return this._title.asObservable()
	}

	setTitle(title: string) {
		this._title.next(title);
	}
}
