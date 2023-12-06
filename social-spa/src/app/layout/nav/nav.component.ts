import { Component,OnInit,OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy{
  isLoggedIn:boolean = false;
  constructor(public authService: AuthService){
    this.loggedIn();
  }
  ngOnDestroy(): void{
    
  }
  ngOnInit(): void{

  }
  loggedIn(){
    this.authService.currentUser$.subscribe({
      next: (res: any)=> {
        if(res){
          this.isLoggedIn = true;
        }
      }
    })
    
  }
  onLogout(){
    this.authService.loggedOut();
  }
}
