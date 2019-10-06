import { Injectable } from '@angular/core';  
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { environment } from '@environments/environment';

@Injectable({  
    providedIn: 'root'  
})  
  
//Add API version header!

export abstract class BaseService{  
  
    protected apiUrl = "";  
    protected controllerName = "";
      
    constructor(protected http: HttpClient, _controllerName: string) {  
        this.controllerName = _controllerName;
        this.apiUrl = environment.apiUrl + "/api/" + this.controllerName + "/";
    }  
  
    protected handleError(error: HttpErrorResponse) {  
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