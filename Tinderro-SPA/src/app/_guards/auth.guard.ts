import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  // ten guardzik zwraca TRUE jezeli jestesmy zalaogowani, dzieki temu jezeli true to mamy dostep do routingu i linkow nawigacyjnych
  // potem trzeba dodac w app.moules w providers tego guardzika
  // potem w routingu trza dodac do interesujacego nas routingu takie cos: canActivate: [AuthGuard], w nawiasach [] podajemy nazwe guardzika
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.loggedin()) {
      return true;
    }

    alertify.error('Nie masz uprawnien');
    this.router.navigate(['/home']);
    return false;
  }
}
