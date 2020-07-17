import { Component, OnInit, Input } from '@angular/core';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {

  @Input() daneZUserList: User; // daneZUserList musi byc IDENTYCZNE jak to ktore przeyslamy

  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  sendLike(recipientId: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, recipientId)
      .subscribe(response => {
        this.alertify.success('Polubiles ' + this.daneZUserList.username);
      }, error => {
        console.log(error);
        this.alertify.error(error);
      });
  }
}
