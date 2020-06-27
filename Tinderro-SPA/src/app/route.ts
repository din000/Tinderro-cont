import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailsComponent } from './user-details/user-details.component';

export const appRoutes: Routes = [
    // routink leci po kolei!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    { path: '', component: HomeComponent},
    { path: 'użytkownicy', component: UserListComponent, canActivate: [AuthGuard]},
    { path: 'użytkownicy/:id', component: UserDetailsComponent, canActivate: [AuthGuard]},
    { path: 'polubienia', component: LikesComponent, canActivate: [AuthGuard]},
    // , canActivate: [AuthGuard] to jest powiazane z authguard i zabezpiecza routing (tzn dziala kiedy jestesmy zalogowani)
    { path: 'wiadomosci', component: MessagesComponent, canActivate: [AuthGuard]},
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
