import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from '../_models/user';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  user: User;
  // viewchild jest po to zeby ZRESETWOAC formularz
  @ViewChild('editform') editform: NgForm;

  // to zabezpiecza przed wcisnieciem krzyzyka i przed nie zapisaniem zmian
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editform.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }

  updateUser() {
    console.log(this.user);
    this.alertify.success('Zapisano zmiany');
    // resetuje formularz I WAZNE trzeba PRZEKAZAC usera bo inaczej beda puste pola
    this.editform.reset(this.user);
  }
}
