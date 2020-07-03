import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private authService: AuthService) {}
  jwtHelper = new JwtHelperService();

  ngOnInit(): void {
    // te 2 linijki sa po to zeby decodedToken ZAWSZE sie tam znajdowal gdyz to jest GLOWNY komponent i ma wladze ktora zawsze dziala
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user')); // parsowanko na cos xd
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
      this.authService.changeUserPhoto(user.photoUrl);
    }
  }
}
