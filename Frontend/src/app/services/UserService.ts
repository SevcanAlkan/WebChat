import { Injectable } from '@angular/core';  
import { User, UserLoginVM, UserListVM } from '@app/models/User';
import { BaseService } from '@common/BaseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class UserService extends BaseService<User> {
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
}
