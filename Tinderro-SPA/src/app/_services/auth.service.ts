import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  // te 2 linijki to przesylania danych globalnie i ta metoda tez: changeUserPhoto
  // <string> bo to bedzie url, a w nawiasach () jest podstawowy obrazek
  mainPhotoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.mainPhotoUrl.asObservable();

  constructor(private http: HttpClient) { }

  // pozniej to trzeba wywolac w metodzie login
  changeUserPhoto(photoUrl: string) {
    this.mainPhotoUrl.next(photoUrl);
  }

  // UWAGAAAAAAAAAAAAAAAA trza pamietac zeby to co tutaj dajemy do local storage to pozniej to usunac np przy wylogowywaniu

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
          localStorage.setItem('user', JSON.stringify(user.user)); // parsikujemy na JSON zeby to nie byly swykle stringi tylko obiekt klasa
          // rozkodowywuje tokena i to przelozy sie na wyswietlanie imienia przy "Witam uzytkopwniku"
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
          this.changeUserPhoto(this.currentUser.photoUrl);
          // console.log(this.decodedToken); // dzieki temu na konsoli wyswietli sie rozkodowany token i bede wiedzial co z niego wyciagnac
        }
      }));
  }

  // rejestracja przed rozszerzeniem rejestracji
  // register(model: any) {
  //   return this.http.post(this.baseUrl + 'register', model);
  // }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedin() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);  // jezeli nie wygasl to zwraca false a to oznacza ze uzytkownik jest dalej zalogowany
  }
}
