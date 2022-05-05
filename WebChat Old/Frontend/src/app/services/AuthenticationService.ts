import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {BehaviorSubject, Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

import {environment} from '@environments/environment';
import {User, UserLoginVM, UserRegisterVM} from '@app/Models/user';

@Injectable({providedIn: 'root'})
export class AuthenticationService {
  public currentUser: Observable<User>;
  private currentUserSubject: BehaviorSubject<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    var vm = new UserLoginVM();
    vm.UserName = username;
    vm.PasswordHash = password;

    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    var result = this.http.post<User>(`${environment.apiUrl}/api/user/createtoken`, vm, {headers: headers})
      .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        catchError(this.handleError);

        return user;
      }));

    return result;
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  register(user: UserRegisterVM) {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    var result = this.http.post<UserRegisterVM>(`${environment.apiUrl}/api/user/register`, user, {headers: headers})
      .pipe(map(user => {
        catchError(this.handleError);
        return user;
      }));

    return result;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  };
}
