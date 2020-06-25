import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  // do metody przychodzi model, pobieramy dane url i w odpowiedzi dostajemy token, token przypisujemy do "user",
  // a pozniej jezeli wszystko poszlo dobrze czyli jezeli cos przyszlo (a przyjdzie jezeli user istnieje)
  // to token jest zapisywany na local storage
  // ogolnie to jest po to zeby przechwycic token

  login(model: any){
    return this.http.post(this.baseUrl + 'login', model) // ten model zawiera haslo i login ktore jest przekazywane do API
      .pipe(map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          // rozkodowywuje tokena i to przelozy sie na wyswietlanie imienia przy "Witam uzytkopwniku"
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          // console.log(this.decodedToken); // dzieki temu na konsoli wyswietli sie rozkodowany token i bede wiedzial co z niego wyciagnac
        }
      }));
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedin() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);  // jezeli nie wygasl to zwraca false a to oznacza ze uzytkownik jest dalej zalogowany
  }
}
