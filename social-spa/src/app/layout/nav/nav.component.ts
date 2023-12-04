import { Component } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  isLoggedIn:boolean = false;
  loggedIn(){
    this.isLoggedIn = true;
  }
  onLogout(){
    this.isLoggedIn = false;
  }
}
