import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router, RouterModule } from '@angular/router';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { UserService } from '../_services/user.service';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;

@Injectable()
export class UserEditResolver implements Resolve<User> {

    constructor(private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    // !!!!!!!!!!!!!!!!!!!!!! resolver jest po to zeby otrzymac dane PRZEDDDDDDDDDd aktywacja samego routera, inaczej blad

    // to jest konkretnie do pobierania konkretnego usera
    // to jest po to zeby w htmlu nie pisac ciagle np user?.age (chodzi o pytajnik)
    // pozniej w route.ts przy konkretnym routingu trzeba dodac po przecinku RESOLVE....

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.GetUser(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                alertify.error('Problem z pobraniem danych');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
