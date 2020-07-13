import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router, RouterModule } from '@angular/router';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { UserService } from '../_services/user.service';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/Message';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;

@Injectable()
export class MessageResolver implements Resolve<Message[]> {

    pageNumber = 1;
    pageSize = 18;
    messageContainer = 'Nieprzeczytane';

    constructor(private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    // !!!!!!!!!!!!!!!!!!!!!! resolver jest po to zeby otrzymac dane PRZEDDDDDDDDDd aktywacja samego routera, inaczej blad

    // to jest do pobierania userow
    // to jest po to zeby w htmlu nie pisac ciagle np user?.age (chodzi o pytajnik)
    // pozniej w route.ts przy konkretnym routingu trzeba dodac po przecinku RESOLVE....

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        return this.userService.getMessages(this.authService.decodedToken.nameid, this.pageNumber, this.pageSize, this.messageContainer)
        .pipe(
            catchError(error => {
                alertify.error('Problem z pobraniem wiadomosci');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
