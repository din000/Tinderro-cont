import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Message } from '../_models/Message';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';


@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit {

  @Input() recipientId: number;
  messages: Message[];
  message: Message;
  newMessage: any = {};

  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.userService.getMessageThread(this.authService.decodedToken.nameid, this.recipientId)
      .subscribe(response => {
        this.messages = response;
      }, error => {
        this.alertify.error(error);
      });
  }

  // metoda moglaby zadzialac ale trzeba formBuildera tak jak w rejestracji albo w edycji uzytkownika
  sendMessageByMe() {
    this.message.recipientId = this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid, this.message)
      .subscribe((message: Message) => {
        this.messages.unshift(message);
        this.newMessage.content = '';
      });
  }

  // tworzymy nowy pusty obiekcik potem go wypelniamy i wysylamy :D
  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid, this.newMessage)
      .subscribe((message: Message) => {
        this.messages.unshift(message); // tutaj robimy zeby od razu sie wyswietlila wiadomosc bez odswiezania
        this.newMessage.content = ''; // usuwamy to co napisalismy w inpuciku
      }, error => {
        this.alertify.error(error);
      });
  }

}
