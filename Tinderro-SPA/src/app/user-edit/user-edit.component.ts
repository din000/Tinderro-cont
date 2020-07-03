import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from '../_models/user';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  user: User;
  // viewchild jest po to zeby ZRESETWOAC formularz
  @ViewChild('editform') editform: NgForm;
  mainPhotoUrl: string;

  // to zabezpiecza przed wcisnieciem krzyzyka i przed nie zapisaniem zmian
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editform.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private userService: UserService,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
    this.authService.currentPhotoUrl.subscribe(mainPhotoUrl => this.mainPhotoUrl = mainPhotoUrl);
  }

  updateUser() {

    this.userService.UpdateUser(this.authService.decodedToken.nameid, this.user)
        .subscribe(next => {
          this.alertify.success('Pomyslnie zaktualizowano');
           // resetuje formularz I WAZNE trzeba PRZEKAZAC usera bo inaczej beda puste pola
          this.editform.reset(this.user);
        }, error => {
          this.alertify.error(error);
        });

    // console.log(this.user);
    // this.alertify.success('Zapisano zmiany');
    // resetuje formularz I WAZNE trzeba PRZEKAZAC usera bo inaczej beda puste pola
    // this.editform.reset(this.user);
  }


  // poniewaz w htmlu przesylamy w nawiasikach $event
  // a $event to photourl ktory wczesniej wyslalismy z photo component
  // to ponizsza metoda przyjmuje wlasnie photourl (nazwa przyjmowanego argumentu w tej metodzie moze byc dowlona)
  updatePhoto(photoUrl) {
    this.user.photoUrl = photoUrl;
  }
}
