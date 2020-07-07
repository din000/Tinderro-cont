import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination } from '../_models/pagination';
declare let alertify: any;

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[];
  page = 1;
  pagination: Pagination;

  constructor(private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    // this.loadUsers(); // przed lesolverem
    this.route.data.subscribe(data => {
      this.users = data.users.result; // zeby pagination dziallo to trzeba dac .result
      this.pagination = data.users.pagination; // ustawia pagination
    });
  }

  // pod tym evencikiem kryje sie nr strony z htmla
  pageChanged(event: any): void {
    this.pagination.currentPage = event; // najpierw ustawia nowa paginacje zeby pozniej wyslac info do loadusers()
    console.log(this.pagination.currentPage);
    this.loadUsers(); // i tutaj juz ma nowa paginacje i wyswietla ja
  }

    // ta metoda pobiera nowych uzytkownikow i info o nowej paginacji
    loadUsers() {
    this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(response => { // responsikiem jest klasa pagination bo to zwraca,u z user service
      this.users = response.result;
      this.pagination = response.pagination;
    }, error => {
      alertify.error('Cos poszlo nie tak'); // declare let alertify: any;
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
