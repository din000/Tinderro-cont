import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() daneZInnegoKomponentuHOME: any;
  @Output() cancelRegisterMode = new EventEmitter(); // this.cancelRegisterMode.emit(false);

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      alertify.success('Zarejestrowales sie');
    }, error => {
      alertify.error('cos poszlo nie tak');
    });
  }

  cancel() {
    this.cancelRegisterMode.emit(false); // za false mozemy podstawic WSZYSTKO co chcemy
  }
}
