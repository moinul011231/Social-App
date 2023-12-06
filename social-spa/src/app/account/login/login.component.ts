import { Component,OnInit } from '@angular/core';
import { FormGroup,FormBuilder,FormControl,Validators } from '@angular/forms';
import {MatTabChangeEvent} from '@angular/material/tabs';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  isRegistraionForm: boolean = false;
  hide = true;
  loginForm: FormGroup;

  constructor(public fb: FormBuilder, 
    public authService: AuthService,
    public router: Router
    ){
        this.loginForm = new FormGroup({});
  }

  ngOnInit():void{
      this.initalizeForms();   
  }

  initalizeForms(){
    this.loginForm= this.fb.group({
      username: ['', Validators.required],
      password: ['',Validators.required]
    })
  }
  onTabChange(event : MatTabChangeEvent){
      if (event.tab.textLabel === 'Login' ) {
        this.isRegistraionForm = false;
      }
      if(event.tab.textLabel === 'Register' ){
        this.isRegistraionForm = true;
      }
  }
  userLogin(){
    this.authService.onLogin(this.loginForm.value).subscribe({
      next: (response: any)=>{
        if(response){
          this.router.navigateByUrl('/');
        }
        console.log(response);
        
      }
    })
  }
}
