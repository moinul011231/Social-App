import { Injectable } from '@angular/core';
import { NgsSpinnerService } from 'ngx-spinner';
@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  loadingRequestCound=0;

  constructor(private spinerService: NgxSpinnerService) { }

  showSpiner(){
    this.loadingRequestCound++;
    this.spinerService 
  }
  hideSpiner(){}
}
