import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { Message } from '../_models/Message';
import { report } from 'process';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  messageContainer = 'Nieprzeczytane'; // wartosc domyslna ataka jak w API

  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private authService: AuthService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.messages = data.messages.result;
      this.pagination = data.messages.pagination;
    });
  }

  loadMessages() {
    this.userService.getMessages(this.authService.decodedToken.nameid, this.pagination.currentPage,
                                 this.pagination.itemsPerPage, this.messageContainer)
        // tutaj mozna dac bardziej szczegolowa linijke ale chyba nie trzeba
        // no bo responsik jest wlasnie tym (linijka nizej)
        // .subscribe((response: PaginationResult<Message[]>) => {
        .subscribe(response => {
          this.messages = response.result;
          this.pagination = response.pagination;
        }, error => {
          this.alertify.error(error);
        });
  }

  pageChanged(event: any): void {
    // ten evencik juz wie ze bedzie event.page
    this.pagination.currentPage = event; // najpierw ustawia nowa paginacje zeby pozniej wyslac info do loadusers()
    this.loadMessages(); // i tutaj juz ma nowa paginacje i wyswietla ja
  }

  loadMessagesUnread() {
    this.messageContainer = 'Nieprzeczytane';
    this.userService.getMessages(this.authService.decodedToken.nameid, this.pagination.currentPage,
                                 this.pagination.itemsPerPage, this.messageContainer)
        // tutaj mozna dac bardziej szczegolowa linijke ale chyba nie trzeba
        // no bo responsik jest wlasnie tym (linijka nizej)
        // .subscribe((response: PaginationResult<Message[]>) => {
        .subscribe(response => {
          this.messages = response.result;
          this.pagination = response.pagination;
        }, error => {
          this.alertify.error(error);
        });
  }

  loadMessagesInbox() {
    this.messageContainer = 'Inbox';
    this.userService.getMessages(this.authService.decodedToken.nameid, this.pagination.currentPage,
                                 this.pagination.itemsPerPage, this.messageContainer)
        // tutaj mozna dac bardziej szczegolowa linijke ale chyba nie trzeba
        // no bo responsik jest wlasnie tym (linijka nizej)
        // .subscribe((response: PaginationResult<Message[]>) => {
        .subscribe(response => {
          this.messages = response.result;
          this.pagination = response.pagination;
        }, error => {
          this.alertify.error(error);
        });
  }

  loadMessagesOutbox() {
    this.messageContainer = 'Outbox';
    this.userService.getMessages(this.authService.decodedToken.nameid, this.pagination.currentPage,
                                 this.pagination.itemsPerPage, this.messageContainer)
        // tutaj mozna dac bardziej szczegolowa linijke ale chyba nie trzeba
        // no bo responsik jest wlasnie tym (linijka nizej)
        // .subscribe((response: PaginationResult<Message[]>) => {
        .subscribe(response => {
          this.messages = response.result;
          this.pagination = response.pagination;
        }, error => {
          this.alertify.error(error);
        });
  }

   // messageId bedzie z htmla
   deleteMessage(messageId: number) {
    this.alertify.confirm('Czy usunac wiadomosc?', () => {
      this.userService.deleteMessage(messageId, this.authService.decodedToken.nameid).subscribe(() => {
        this.messages.splice(this.messages.findIndex(m => m.id === messageId), 1);
        this.alertify.success('Wiadomosc usunieta');
      }, error => {
        this.alertify.error('Nie mozna usunac wiadomosci');
      });
    });
  }
}
