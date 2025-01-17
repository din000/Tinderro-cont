import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  danes: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getValues() {
    this.http.get('http://localhost:5000/Dane').subscribe(response => {
    this.danes = response;
    }, error => {
      console.log(error);
    });
  }

  cancelRegisterMode(thisWillChangeRegisterMode: boolean) {
    this.registerMode = thisWillChangeRegisterMode;
  }
}
