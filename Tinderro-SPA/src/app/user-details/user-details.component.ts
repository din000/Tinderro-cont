import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Photo } from '../_models/photo';
import { NgbNav } from '@ng-bootstrap/ng-bootstrap';
import { asLiteral } from '@angular/compiler/src/render3/view/util';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
declare let alertify: any;

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: User;
  images: Photo[];
  activeTab = 1; // to jest ktora zakladka odpala sie jako PIERWSZA

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private alertify2: AlertifyService,
              private authService: AuthService) { }

  ngOnInit() {
      // this.loadUser(); // bez resolvera
      this.route.data.subscribe(data => {   // data to ogolnie dane jakie sa w tym routingu/resolvedzie?
        this.user = data.user;   // data.USER "USER" (nazwe) bierzemy z route.ts taka jak dalismy przy resolve
      });

      this.images = this.user.photos;

      // to jest przesylane z messages HTML z [queryParams]
      // i tu uwaga! to co tam przeslemy np XYZ to tutaj po kropce tez jest ten parametr pod XYZ
      this.route.queryParams.subscribe(paramsik => {
        this.activeTab = parseInt(paramsik.activeTab, 10) > 0 ? parseInt(paramsik.activeTab, 10) : 1; // miszczowski parsik na inta
      });
  }

  sendLike(recipientId: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, recipientId)
      .subscribe(response => {
        this.alertify2.success('Polubiles ' + this.user.username);
      }, error => {
        console.log(error);
        this.alertify2.error(error);
      });
  }


  }

  // ta metoda miala pobieraz zdjecia ale zamieilem to na 1 linijke kodu XD
  // getImages() {
  //   const imagesUrls = [];
  //   const chuj = [];
  //   // tslint:disable-next-line: prefer-for-of
  //   for (let i = 0; i < this.user.photos.length; i++) {
  //     imagesUrls.push(this.user.photos[i].url);
  //     this.chuj = imagesUrls;
  //   }
  //   return console.log(this.chuj);
  // }



  // to byloby gdyby nie bylo resolvera
  // loadUser() {
  //   // +this.route.snapshot.params['id'] - to niby zadziala jako routing xd
  //   this.userService.GetUser(+this.route.snapshot.params.id)
  //     .subscribe((user: User) => {
  //       this.user = user;
  //     }, error => {
  //       alertify.error('Coś poszło nie tak');
  //     });
  // }


