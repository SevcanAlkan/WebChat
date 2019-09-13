import { Injectable } from '@angular/core';  
import { User, UserLoginVM } from '@app/models/User';
import { BaseService } from '@common/BaseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { catchError } from 'rxjs/operators';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class UserService extends BaseService<User> {
  constructor(http: HttpClient) {
    super(http, "user");    
  }
}
