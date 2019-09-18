import { Injectable } from '@angular/core';  
import { Observable, throwError } from 'rxjs'  
import { catchError, tap, map } from 'rxjs/operators'  
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { environment } from '@environments/environment';
import { BaseVM } from './BaseModel';
import { APIResultVM } from '@models/APIResultVM';

@Injectable({  
    providedIn: 'root'  
})  
  
export abstract class BaseService<T extends BaseVM, U extends BaseVM>{  
  
    protected apiUrl = "";  
    username: any;  
    private controllerName = "";
      
    constructor(protected http: HttpClient, _controllerName: string) {  
        this.controllerName = _controllerName;
        this.apiUrl = environment.apiUrl + "/api/" + this.controllerName + "/";
    }  
  
    public GetAll():Observable<T[]> {  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });   
        return this.http.get<T[]>(this.apiUrl + "get", { headers: headers })
        .pipe(map((data: T[]) => data),  
            catchError(this.handleError)  
        );  
    }  
  
    public GetById(id) :Observable<APIResultVM>{  
        var url = this.apiUrl + 'GetById?id=' + id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        return this.http.get<APIResultVM>(url, { headers: headers }).pipe(tap(data => data),  
            catchError(this.handleError)  
        );  
    }  
  
    public Add(model: T) {  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });         
        return this.http.post<any>(this.apiUrl + "add", model, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  

    public Update(model: U) {  
        var url = this.apiUrl + 'update?id=' + model.id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });   
        return this.http.put<any>(url, model, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  
  
    public Delete(id) {  
        var url = this.apiUrl + 'delete?id=' + id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });   
        return this.http.delete<any>(url, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
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