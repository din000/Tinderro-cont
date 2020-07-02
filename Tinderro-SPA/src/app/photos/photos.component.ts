import { Component, OnInit, Input } from '@angular/core';
import { Photo } from '../_models/photo';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { environment } from 'src/environments/environment.prod';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})
export class PhotosComponent implements OnInit {

  @Input() photos: Photo[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private authService: AuthService) { }

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


  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

}
