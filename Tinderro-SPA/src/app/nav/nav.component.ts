import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};    // ten model bedzie uzupelniony w nav.cpomponents.html, dlatego bedize mial .username i .password

  // wstrzykunpm install alertifyjs --savejemy authservice poniewaz on zawiera komunikacje z api
  constructor(public authService: AuthService) { }  // musi byc public bo w htmlu wywala blad przy wyswietlaniu nazwy
  ngOnInit() {
  }

  login() {
    // console.log(this.model); // tu juz nie wyswietlamy na konsole zeby zobaczyc ze dziala
    this.authService.login(this.model).subscribe(next => {
      alertify.success('Zalogowaleś się do aplikacji');
    }, error => {
      alertify.error('Coś poszło nie tak');
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
  }

}
