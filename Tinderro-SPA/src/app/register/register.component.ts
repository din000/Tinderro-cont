import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from '../_models/user';
import { Router } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() daneZInnegoKomponentuHOME: any;
  @Output() cancelRegisterMode = new EventEmitter(); // this.cancelRegisterMode.emit(false);
  // model: any = {}; // dane chyba brane sa z htmla i w klamrach mozemy wstawiac co chcemy :D // to bylo przed rozbudowaniem rejestracji
  user: User;


  registerForm: FormGroup;

  constructor(private authService: AuthService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  // z tego juz nie korzystamy bo to bylo przed formbuilderem
  oldVersionRegisterForm() {
    this.registerForm = new FormGroup({
      // '' - placeholder, dziala jak placeholder, tyle ze da sie to zaznaczyc w gotowej apce, validators - walidatorki
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]),
      confrimPassword: new FormControl('', Validators.required),
    }, this.passwordMatchValidator);
  }

  // tutaj wykorzystujemy formbuildera dla formularza reejstracji
  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]],
      confrimPassword: ['', Validators.required],
      gender: ['male'],
      dateOfBirth: [null, Validators.required],
      zodiacSign: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  // sprawdza hasla czy sa takie same
  passwordMatchValidator(naszaFormeka: FormControl) {
    return naszaFormeka.get('password').value === naszaFormeka.get('confrimPassword').value ? null : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid)
    {

      this.user = Object.assign({}, this.registerForm.value); // {} - obiekt // przypisujemy wartosci z formularza do usera

      this.authService.register(this.user).subscribe(() => {
        alertify.success('Zarejestrowales sie');
      }, error => {
        alertify.error('Cos poszlo nie tak z rejestracja');
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/użytkownicy']);
        });
      });
    }
  }

  // tak bylo przed rozbudowaniem rejestracji
  // register() {
  //   // this.authService.register(this.model).subscribe(() => {
  //   //   alertify.success('Zarejestrowales sie');
  //   // }, error => {
  //   //   alertify.error('cos poszlo nie tak');
  //   // });
  //   console.log(this.registerForm.value);
  // }

  cancel() {
    this.cancelRegisterMode.emit(false); // za false mozemy podstawic WSZYSTKO co chcemy
  }
}
