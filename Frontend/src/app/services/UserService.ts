import {Injectable} from '@angular/core';
import {User, UserListVM, UserUpdateVM} from '@app/Models/user';
import {BaseService} from '@app/common/BaseService';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

@Injectable()
export class UserService extends BaseService<User, UserUpdateVM> {
  constructor(http: HttpClient) {
    super(http, "user");
  }

  public getUserList(): Observable<UserListVM[]> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.get<UserListVM[]>(this.apiUrl + "get", {headers: headers})
      .pipe(map((data: UserListVM[]) => data),
        catchError(this.handleError)
      );
  }

  public isUserNameExist(userName: string = ""): Observable<boolean> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.get<boolean>(this.apiUrl + "UserNameIsExist?userName=" + userName, {headers: headers})
      .pipe(map((data: boolean) => data),
        catchError(this.handleError)
      );
  }
}
