import { Component } from '@angular/core';
import {FullNamePipe} from '../../pipe/full-name.pipe'
import {UserInfo} from '../../models/user-info.type'
import {NotSpecifiedPipe} from '../../pipe/not-specified.pipe'

@Component({
  selector: 'app-card-master',
  standalone: true,
	imports: [
		FullNamePipe,
		NotSpecifiedPipe
	],
  templateUrl: './card-master.component.html',
  styleUrl: './card-master.component.scss'
})
export class CardMasterComponent {
	master: UserInfo
}
