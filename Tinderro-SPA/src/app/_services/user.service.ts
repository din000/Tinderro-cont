import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

// to bylo do autoryzacji
// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + localStorage.getItem('token')
//   })
// };

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  // poniewaz do tych metod maja dostep tylko zarejestrowani uzytkownicy to trzeba dodac tokena w opcjach, czyli httpPotions

  GetUsers(): Observable<User[]> {
    // return this.http.get<User[]>(this.baseUrl + 'users', httpOptions); // to bylo do autoryzacji ale trza bylo odswiezyc zeby dzialalo
    return this.http.get<User[]>(this.baseUrl + 'users');
  }

  GetUser(id: number): Observable<User> {
    // return this.http.get<User>(this.baseUrl + 'user/' + id, httpOptions);
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  UpdateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + 'users/' + id, user);
  }

  setMainPhoto(userId: number, id: number) {
    // te nawiasy {} to przekazanie pustego obiektu?
    return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain', {});
  }
}
