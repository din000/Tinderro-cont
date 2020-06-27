import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};    // ten model bedzie uzupelniony w nav.cpomponents.html, dlatego bedize mial .username i .password

  // wstrzykujemy authservice poniewaz on zawiera komunikacje z api
  // npm install alertifyjs --save
  // musi byc public bo w htmlu wywala blad przy wyswietlaniu nazwy
  // wstrzukujemy router bo potrzebny do nawigazji
  constructor(public authService: AuthService, private router: Router) { }
  ngOnInit() {
  }

  login() {
    // console.log(this.model); // tu juz nie wyswietlamy na konsole zeby zobaczyc ze dziala
    this.authService.login(this.model).subscribe(next => {
      alertify.success('Zalogowaleś się do aplikacji');
    }, error => {
      alertify.error('Coś poszło nie tak');
    }, () => {
      this.router.navigate(['/użytkownicy']);
    });
  }
  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token;   // dziala jak if, jezeli jest to true jezeli nie to false
    return this.authService.loggedin();
  }

  logOut() {
    localStorage.removeItem('token');
    alertify.message('Zostales wylogowany');
    this.router.navigate(['/home']);
  }

}
