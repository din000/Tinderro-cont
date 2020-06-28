import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Photo } from '../_models/photo';
declare let alertify: any;

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: User;
  images: Photo[];

  constructor(private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
      // this.loadUser(); // bez resolvera
      this.route.data.subscribe(data => {   // data to ogolnie dane jakie sa w tym routingu/resolvedzie?
        this.user = data.user;   // data.USER "USER" (nazwe) bierzemy z route.ts taka jak dalismy przy resolve
      });

      this.images = this.user.photos;
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
}
