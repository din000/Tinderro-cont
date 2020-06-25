import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-dane',
  templateUrl: './dane.component.html',
  styleUrls: ['./dane.component.css']
})
export class DaneComponent implements OnInit {

  danes: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getDane();
  }

  getDane() {
    this.http.get('http://localhost:5000/Dane').subscribe(response => {
      this.danes = response;
    }, error => {
      console.log(error);
    });
  }
}
