import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { AuthGuard } from './_guards/auth.guard';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserCardComponent } from './user-card/user-card.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserDetailResolver } from './_resolves/user-etail.resolver';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListResolver } from './_resolves/user-list.resolver';
import { UserEDITTResolver } from './_resolves/UserEDITT.Resolver';
import { AlertifyService } from './_services/alertify.service';
import { PreventUnsaveChanges } from './_guards/prevent-unsavedChanges.guard';
import { PhotosComponent } from './photos/photos.component';
import { FileUploadModule } from 'ng2-file-upload';
import { TimeAgoPipe } from './_pipes/time-ago-pipe';


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
      MessagesComponent,
      UserCardComponent,
      UserDetailsComponent,
      UserEditComponent,
      PhotosComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      FileUploadModule,

      // --------------------------
      JwtModule.forRoot({
         config: {
            tokenGetter: TokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
      // ---------------------------

      // JwtModule.forRoot({
      //    config: {
      //       tokenGetter: TokenGetter,
      //       whitelistedDomains: ['localhost:5000'],
      //       blacklistedRoutes: ['localhost:5000/api/auth']
      //    }
      // }),


       // dodaje routing z pliku route.ts
      RouterModule.forRoot(appRoutes),
       // importujemy dropdown z ngx bootstrap
      BsDropdownModule.forRoot(),
      NgbModule,
   ],
   providers: [
      AuthService,
      UserService,
      AuthGuard,
      UserDetailResolver,
      UserListResolver,
      UserEDITTResolver,
      AlertifyService,
      PreventUnsaveChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
