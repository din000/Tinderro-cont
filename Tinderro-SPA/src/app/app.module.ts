import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DaneComponent } from './dane/dane.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { UserService } from './_services/user.service';
import { UserListComponent } from './user-list/user-list.component';
import { JwtModule } from '@auth0/angular-jwt';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './route';


export function TokenGetter() { // to jest do globalnej autoryzacji i juz jest ok z odswiezaniem
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      DaneComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      UserListComponent,
      LikesComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: TokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
      RouterModule.forRoot(appRoutes), // dodaje routing z pliku route.ts
   ],
   providers: [
      AuthService,
      UserService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
