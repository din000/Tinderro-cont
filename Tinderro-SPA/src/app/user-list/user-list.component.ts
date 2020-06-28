import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[];

  constructor(private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    // this.loadUsers(); // przed lesolverem
    this.route.data.subscribe(data => {
      this.users = data.users;
    });
  }


  // przed resolverem (patrz user-details- tam ejst wiekszy opsi tego przypadku)
  // loadUsers() {
  //   this.userService.GetUsers().subscribe((users: User[]) => {
  //     this.users = users;
  //   }, error => {
  //     alertify.error('Cos poszlo nie tak'); // declare let alertify: any;
  //   });
  // }

}
