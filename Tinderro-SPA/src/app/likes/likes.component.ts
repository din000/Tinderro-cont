import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-likes',
  templateUrl: './likes.component.html',
  styleUrls: ['./likes.component.css']
})
export class LikesComponent implements OnInit {

  users: User[];
  pagination: Pagination;
  likesParam: string;

  constructor(private authService: AuthService,
              private userService: UserService,
              private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users_lkes.result;
      this.pagination = data.users_lkes.pagination;
    });
    this.likesParam = 'UserLikes';
    // this.loadUsers();
  }

  // ta metoda pobiera polubianych uzytkownikow lub tych ktorzy nas polubili
  loadUsersWhichLikesMe() {
    this.likesParam = 'SomeoneLikes';
    this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam)
      .subscribe(response => { // responsikiem jest klasa pagination bo to zwraca,u z user service ????? tutaj moze juz inny responsik
      this.users = response.result;
      this.pagination = response.pagination;
    }, error => {
      this.alertify.error('Cos poszlo nie tak'); // declare let alertify: any;
    });
  }

  loadUsersThatILike() {
    this.likesParam = 'UserLikes';
    this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam)
      .subscribe(response => { // responsikiem jest klasa pagination bo to zwraca,u z user service ????? tutaj moze juz inny responsik
      this.users = response.result;
      this.pagination = response.pagination;
    }, error => {
      this.alertify.error('Cos poszlo nie tak'); // declare let alertify: any;
    });
  }

  loadUsers() {
    this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam)
      .subscribe(response => { // responsikiem jest klasa pagination bo to zwraca,u z user service ????? tutaj moze juz inny responsik
      this.users = response.result;
      this.pagination = response.pagination;
    }, error => {
      this.alertify.error('Cos poszlo nie tak'); // declare let alertify: any;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event; // najpierw ustawia nowa paginacje zeby pozniej wyslac info do loadusers()
    this.loadUsers(); // i tutaj juz ma nowa paginacje i wyswietla ja
  }
}
