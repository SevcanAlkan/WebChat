import { Injectable } from '@angular/core';  
import { User, UserLoginVM, UserListVM, UserRegisterVM, UserUpdateVM } from '@app/models/User';
import { BaseService } from '@app/common/baseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { APIResultVM } from '@app/common/APIResultVM';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class UserService extends BaseService<User, UserUpdateVM> {
  constructor(http: HttpClient) {
    super(http, "user");    
  }

  public getUserList() : Observable<UserListVM[]> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  

    return this.http.get<UserListVM[]>(this.apiUrl + "get", { headers: headers })
    .pipe(map((data: UserListVM[]) => data),  
        catchError(this.handleError)  
    );  
  }

  public isUserNameExist(userName: string = "") : Observable<boolean> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  

    return this.http.get<boolean>(this.apiUrl + "UserNameIsExist?userName=" + userName, { headers: headers })
    .pipe(map((data: boolean) => data),  
        catchError(this.handleError)  
    );  
  }  
}
