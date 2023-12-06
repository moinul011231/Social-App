import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map,BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<any | null >(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http:HttpClient) { }

   onLogin(model: any){
    return this.http.post<any>(`${this.baseUrl}auth/login`,model).pipe(
      map((response : any)=>{
        const user = response;
        if(user){
          //set current user
          this.setCurrrentUser(user);
        }
        return user;
      } ) 
    )
   }
   setCurrrentUser(user: any){
      localStorage.setItem('currentUser',JSON.stringify(user));
      this.currentUserSource.next(user);
   }

   loggedOut(){
    localStorage.removeItem('currentUser');
    this.currentUserSource.next(null);
   }
}
