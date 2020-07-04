import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
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

  registerForm: FormGroup;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      // '' - placeholder, dziala jak placeholder, tyle ze da sie to zaznaczyc w gotowej apce, validators - walidatorki
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]),
      confrimPassword: new FormControl('', Validators.required),
    }, this.passwordMatchValidator);
  }

  // sprawdza hasla czy sa takie same
  passwordMatchValidator(naszaFormeka: FormControl) {
    return naszaFormeka.get('password').value === naszaFormeka.get('confrimPassword').value ? null : { mismatch: true };
  }

  register() {
    // this.authService.register(this.model).subscribe(() => {
    //   alertify.success('Zarejestrowales sie');
    // }, error => {
    //   alertify.error('cos poszlo nie tak');
    // });
    console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegisterMode.emit(false); // za false mozemy podstawic WSZYSTKO co chcemy
  }
}
