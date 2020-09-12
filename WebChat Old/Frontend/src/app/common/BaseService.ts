import { Injectable } from '@angular/core';  
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { environment } from '@environments/environment';
import { APIVersion } from '@environments/APIVersion';

@Injectable({  
    providedIn: 'root'  
})  

export abstract class BaseService{  
  
    protected apiUrl: string;  
    protected controllerName: string;
    protected apiVerison: APIVersion = environment.defaultAPIVersion;
  
    constructor(protected http: HttpClient, _controllerName: string) {  
        this.controllerName = _controllerName;
        this.apiUrl = environment.apiUrl + "/api/" + this.controllerName + "/";
        this.apiVerison = environment.defaultAPIVersion;
    }  

    protected readonly defaultHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'api-version': this.apiVerison.toString() });
    
    protected getHeaders(apiVersion: APIVersion = this.apiVerison) : HttpHeaders {
        let headers = this.defaultHeaders;

        if(apiVersion != this.apiVerison){
            headers.delete("api-version");
            headers.set("api-version", apiVersion.toString());
        }

        return headers;
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