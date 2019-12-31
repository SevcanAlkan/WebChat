import { Injectable } from '@angular/core';  
import { UserVM, UserLoginVM, UserListVM, UserRegisterVM, UserUpdateVM } from '@app/shared/models/user';
import { BaseServiceCRUD } from '@app/shared/BaseServiceCRUD';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { APIResultVM } from '@app/shared/models/APIResultVM';
import { APIVersion } from '@environments/APIVersion';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class UserService extends BaseServiceCRUD<UserVM, UserRegisterVM, UserUpdateVM> {
  constructor(http: HttpClient) {
    super(http, "user");    
  }

  public getUserList(apiVersion: APIVersion = this.apiVerison) : Observable<UserListVM[]> {
    let headers = this.getHeaders(apiVersion);

    return this.http.get<UserListVM[]>(this.apiUrl + "get", { headers: headers })
    .pipe(map((data: UserListVM[]) => data),  
        catchError(this.handleError)  
    );  
  }

  public isUserNameExist(userName: string = "", apiVersion: APIVersion = this.apiVerison) : Observable<boolean> {
    let headers = this.getHeaders(apiVersion);

    return this.http.get<boolean>(this.apiUrl + "UserNameIsExist?userName=" + userName, { headers: headers })
    .pipe(map((data: boolean) => data),  
        catchError(this.handleError)  
    );  
  }  
}
