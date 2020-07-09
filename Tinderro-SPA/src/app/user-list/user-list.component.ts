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
  user: User = JSON.parse(localStorage.getItem('user')); // pobieramy userka z local storage i parsik na obiekt bo to stringi
  page = 1; // zeby dzialo stronnicowanie w html
  pagination: Pagination;
  userParams: any = {}; // pusty obiekt ktory bedzie przechowywal filterki, pozniej go wyslemy w metodzie get users

  // parametry ktore beda wyswietlane jako lista w htmlu
  genderList = [{value: 'kobieta', display: 'Kobiety'},
                {value: 'mężczyzna', display: 'Mężczyźni'}];

  zodiacSignList = [{value: 'wszystkie', display: 'Wszystkie'},
                    {value: 'Baran', display: 'Baran'},
                    {value: 'Byk', display: 'Byk'},
                    {value: 'Bliźnięta', display: 'Bliźnięta'},
                    {value: 'Rak', display: 'Rak'},
                    {value: 'Lew', display: 'Lew'},
                    {value: 'Panna', display: 'Panna'},
                    {value: 'Waga', display: 'Waga'},
                    {value: 'Scorpion', display: 'Skorpion'},
                    {value: 'Strzelec', display: 'Strzelec'},
                    {value: 'Koziorożec', display: 'Koziorożec'},
                    {value: 'Wodnik', display: 'Wodnik'},
                    {value: 'Ryby', display: 'Ryby'}];


  constructor(private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    // this.loadUsers(); // przed lesolverem
    this.route.data.subscribe(data => {
      this.users = data.users.result; // zeby pagination dziallo to trzeba dac .result
      this.pagination = data.users.pagination; // ustawia pagination
    });

    // ustawiamy poczatkowe dane
    this.userParams.gender = this.user.gender === 'mężczyzna' ? 'kobieta' : 'mężczyzna';
    this.userParams.zodiacSign = 'wszystkie';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 100;
    this.userParams.orderBy = 'lastActive';
  }

  resetFilters() {
    this.userParams.gender = this.user.gender === 'mężczyzna' ? 'kobieta' : 'mężczyzna';
    this.userParams.zodiacSign = 'wszystkie';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 100;
    this.userParams.orderBy = 'lastActive';
    this.loadUsers(); // i jeszcze zaladowanie userkow po reseciku
  }

  // pod tym evencikiem kryje sie nr strony z htmla
  pageChanged(event: any): void {
    this.pagination.currentPage = event; // najpierw ustawia nowa paginacje zeby pozniej wyslac info do loadusers()
    console.log(this.pagination.currentPage);
    this.loadUsers(); // i tutaj juz ma nowa paginacje i wyswietla ja
  }

    // ta metoda pobiera nowych uzytkownikow i info o nowej paginacji
    loadUsers() {
    this.userService.GetUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
      .subscribe(response => { // responsikiem jest klasa pagination bo to zwraca,u z user service
      this.users = response.result;
      this.pagination = response.pagination;
      console.log(this.userParams.orderBy);
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
