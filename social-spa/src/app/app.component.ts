import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Social Apps';
  users: any;
    constructor(private authService: AuthService){}

    ngOnInit(): void{
      this.setCurrrentUser();
    }
  setCurrrentUser(){
    const userString = localStorage.getItem('currentUser');

    if(!userString) return;
    const user = JSON.parse(userString);
    this.authService.setCurrrentUser(user);
  }
}
