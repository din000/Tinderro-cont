import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';

export const appRoutes: Routes = [
    { path: 'none', component: HomeComponent},
    { path: 'użytkownicy', component: UserListComponent},
    { path: 'polubienia', component: LikesComponent},
    { path: 'wiadomosci', component: MessagesComponent},
    { path: '**', redirectTo: 'none', pathMatch: 'full'},
];
