import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';
declare let alertify: any;

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: User;

  constructor(private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
      this.loadUser();
  }

  loadUser() {
    // +this.route.snapshot.params['id'] - to niby zadziala jako routing xd
    this.userService.GetUser(+this.route.snapshot.params.id)
      .subscribe((user: User) => {
        this.user = user;
      }, error => {
        alertify.error('Coś poszło nie tak');
      });
  }
}
