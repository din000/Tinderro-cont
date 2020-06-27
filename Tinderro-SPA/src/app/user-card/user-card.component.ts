import { Component, OnInit, Input } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {

  @Input() daneZUserList: User; // daneZUserList musi byc IDENTYCZNE jak to ktore przeyslamy

  constructor() { }

  ngOnInit() {
  }

}
