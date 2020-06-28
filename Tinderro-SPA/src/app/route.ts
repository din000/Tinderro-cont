import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserDetailResolver } from './_resolves/user-etail.resolver';
import { UserListResolver } from './_resolves/user-list.resolver';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserEDITTResolver } from './_resolves/UserEDITT.Resolver';
import { PreventUnsaveChanges } from './_guards/prevent-unsavedChanges.guard';


export const appRoutes: Routes = [
    // routing leci po kolei!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    { path: '', component: HomeComponent},
    { path: 'użytkownicy', component: UserListComponent, canActivate: [AuthGuard], resolve: {users: UserListResolver}},
    { path: 'użytkownicy/:id', component: UserDetailsComponent, canActivate: [AuthGuard], resolve: {user: UserDetailResolver}},
    { path: 'uzytkownicy/edycja', component: UserEditComponent, canActivate: [AuthGuard],
                                                                resolve: {user: UserEDITTResolver},
                                                                canDeactivate: [PreventUnsaveChanges]},
    { path: 'polubienia', component: LikesComponent, canActivate: [AuthGuard]},
    // , canActivate: [AuthGuard] to jest powiazane z authguard i zabezpiecza routing (tzn dziala kiedy jestesmy zalogowani)
    { path: 'wiadomosci', component: MessagesComponent, canActivate: [AuthGuard]},
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
