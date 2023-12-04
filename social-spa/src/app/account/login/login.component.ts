import { Component,OnInit } from '@angular/core';
import { FormGroup,FormBuilder,FormControl,Validators } from '@angular/forms';
import {MatTabChangeEvent} from '@angular/material/tabs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  isRegistraionForm: boolean = false;
  hide = true;
  loginForm: FormGroup;

  constructor(public fb: FormBuilder){
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
  onLogin(){
    console.log(this.loginForm);
  }
}
