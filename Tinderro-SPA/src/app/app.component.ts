import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';

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
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
