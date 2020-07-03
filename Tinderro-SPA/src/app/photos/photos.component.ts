import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from '../_models/photo';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { environment } from 'src/environments/environment.prod';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})
export class PhotosComponent implements OnInit {

  @Input() photos: Photo[];
  @Output() updatePhoto = new EventEmitter(); // bedziemy emitowac zmienione zdj do UserEditComponent
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMainPhoto: Photo;

  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos', // url
      authToken: 'Bearer ' + localStorage.getItem('token'), // 'Bearer ' WAZNE musi byc spacja
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; }; // ta linijkia powoduje ze zdj wgl sie wysyla

    // to robi ze po dodaniu zdjec NIE TRZEBA odswiezac strony zeby zobaczyc rezultat
    // UWAGAAAAA zeby to dzialo to w API trzeba zwracac zdj (mozliwe ze za pomoca createdataction / route), opisane jest to w API
    this.uploader.onSuccessItem = (item, respons, status, headers) => {
      if (respons) {
        const response: Photo = JSON.parse(respons); // parsujemy na response ktore jest klasy Photo
        const photo = {
          id: response.id,
          url: response.url,
          dateAdded: response.dateAdded,
          description: response.description,
          isMain: response.isMain
        };
        this.photos.push(photo); // dodajemy do naszej kolekcji zdjec
      }
    };
  }
  // to jest do edycji zdj
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id) // przekazujemy userId i id zdjecia
      .subscribe(() => {
        console.log('zdj ustawione jako glowne');
        // 3 poznizsze linjiki robia zeby od razu byly efekty przy klikaniu ze zdj ma byc glwone
        // [0] to znaczy ze pobieramy 1 element z przefiltrowanej kolekcji
        // najpierw pobiera aktualnego maina a potem go zmienia i ustawia ze inne jest glowne
        this.currentMainPhoto = this.photos.filter(p => p.isMain === true)[0];
        this.currentMainPhoto.isMain = false;
        photo.isMain = true;

        // emitujemy aktualne main photo
        // this.updatePhoto.emit(photo.url);  // juz nie emitujemy w ten sposob tylko "emitujemy" GLOOBALNIE (ponizsze 3 linijki)
        this.authService.changeUserPhoto(photo.url);
        this.authService.currentUser.photoUrl = photo.url; // aktualizujemy dla aktualnego uzytkownika glowne zdj
        localStorage.setItem('user', JSON.stringify(this.authService.currentUser)); // a teraz aktualizujemy go w local storage
      }, error => {
        this.alertify.error(error);
      });
  }
}
