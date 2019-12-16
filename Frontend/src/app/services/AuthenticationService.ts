import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map,tap } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { UserVM, UserLoginVM, UserRegisterVM } from '@app/models/User';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<UserVM>;
    public CurrentUser: Observable<UserVM>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserVM>(JSON.parse(localStorage.getItem('currentUser')));
        this.CurrentUser = this.currentUserSubject.asObservable();
    }

    public get CurrentUserValue() : UserVM {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string){
        var vm = new UserLoginVM();
        vm.UserName = username;
        vm.PasswordHash = password;

        let headers = new HttpHeaders({ 'Content-Type': 'application/json', "api-version": environment.defaultAPIVersion }); 
        var result = this.http.post<UserVM>(`${environment.apiUrl}/api/user/createtoken`, vm, { headers: headers }) 
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

    register(user:UserRegisterVM){      
        let headers = new HttpHeaders({ 'Content-Type': 'application/json', "api-version": environment.defaultAPIVersion }); 
        var result = this.http.post<UserRegisterVM>(`${environment.apiUrl}/api/user/register`, user, { headers: headers }) 
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