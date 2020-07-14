import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Pagination, PaginationResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '../_models/Message';

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

  // to jest przed pagination
  // GetUsers(): Observable<User[]> {
  //   // return this.http.get<User[]>(this.baseUrl + 'users', httpOptions); // to bylo do autoryzacji ale trza bylo odswiezyc zeby dzialalo
  //   return this.http.get<User[]>(this.baseUrl + 'users');
  // }


  // ogolnie w tej metodzie przy zwracaniu to mapujemy to co dostaniemy od API na to co mamy w SPA
  GetUsers(page?, itemsPerPage?, userParams?, likesParam?): Observable<PaginationResult<User[]>> {
    const paginationResult: PaginationResult<User[]> = new PaginationResult<User[]>(); // PaginationResult to nasza klasa z _models
    let params = new HttpParams();

    // jezeli uzytkownik poda liczbe stron i cos tam to bedzie to przekazane do paramsow ktore pozniej beda przeslane dalej
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page),
      params = params.append('pageSize', itemsPerPage);
    }

    if (userParams != null) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('zodiacSign', userParams.zodiacSign);
      params = params.append('orderBy', userParams.orderBy);
    }

    if (likesParam === 'UserLikes') {
      params = params.append('UserLikes', 'true');
    }

    if (likesParam === 'SomeoneLikes') {
      params = params.append('SomeoneLikes', 'true');
    }

    // wychodzi na to ze to 'response' to to co dostajemy od API
    // params to parametry ktore zawieraja strone i ilosc na stronie
    return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params })
      .pipe(map(response => { // return z API o nazwie Response?
        paginationResult.result = response.body; // pod body kryje sie chyba WSZYSTKO co chcemy wyswietlic czyli uzytkownicy

        // parsik na obiekt bo sa stringi, w '' podajemy nazwe headera
        // wczesniej hedersika "Pagination" stworzylismy w API a tu go przechwytujemy
        // ten hedersik zawiera info o filtrach wyswietlania ktore pozniej parsikujemy na obiekt _models/paginations
        if (response.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginationResult; // i chyba paginationResult bedzie w routerze kryc sie pod nazwa jaka chcemy (w tym przypadku users)
      }));
  }

  GetUser(id: number): Observable<User> {
    // return this.http.get<User>(this.baseUrl + 'user/' + id, httpOptions);
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  UpdateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + 'users/' + id, user);
  }

  setMainPhoto(userId: number, id: number) {
    // te nawiasy {} to przekazanie pustego obiektu
     // {} - do metody post chyba trzeba wysylac jakis obiekt a ze nic nie przesylamy to przesylamy pusty obiekcik :D
    return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
  }

  // {} - do metody post chyba trzeba wysylac jakis obiekt a ze nic nie przesylamy to przesylamy pusty obiekcik :D
  sendLike(userId: number, recipientId: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/like/' + recipientId, {});
  }

  // messageContainer = Inbox / Outbox A JEZELI NIE MA to default czyli Nieprzeczytane xd
  getMessages(userId: number, page?, itemsPerPage?, messageContainer?) {
    const paginationResult: PaginationResult<Message[]> = new PaginationResult<Message[]>();
    let params = new HttpParams();

    // messageContainer = Inbox / Outbox A JEZELI NIE MA to default czyli Nieprzeczytane xd
    params = params.append('messageContainer', messageContainer);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Message[]>(this.baseUrl + 'users/' + userId + '/messages', { observe: 'response', params})
      .pipe(map(response => {
        paginationResult.result = response.body;

        if (response.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginationResult;
      }));
  }

  getMessageThread(userId: number, recipientId: number) {
    return this.http.get<Message[]>(this.baseUrl + 'users/' + userId + '/messages/thread/' + recipientId);
  }
}
