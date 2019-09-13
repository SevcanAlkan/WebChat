import { Injectable } from '@angular/core';  
import { Observable, throwError } from 'rxjs'  
import { catchError, tap, map } from 'rxjs/operators'  
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { environment } from '@environments/environment';
import { BaseVM } from './BaseModel';

@Injectable({  
    providedIn: 'root'  
})  
  
export abstract class BaseService<T extends BaseVM>{  
  
    // private data: any;  
    protected apiUrl = "";  
    // token: any;  
    username: any;  
    private controllerName = "";
      
    constructor(protected http: HttpClient, _controllerName: string) {  
        // this.data = JSON.parse(localStorage.getItem('AdminUser'));  
        // this.token = this.data.token;  
        this.controllerName = _controllerName;
        this.apiUrl = environment.apiUrl + "/api/" + this.controllerName + "/";
    }  
  
    public GetAll():Observable<T[]> {  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        // headers = headers.append('Authorization', 'Bearer ' + `${"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4MDg4YTJkNi1kZmYwLTQzNjMtYmEwZi0zOTVmZjVjOWI1MmMiLCJ1bmlxdWVfbmFtZSI6InNldmNhbiIsImp0aSI6IjZiMTRlMzZlLTk3NzYtNGZmMi05NjgwLTNlMThhYWZkYjk4NiIsImlhdCI6IjkvMTIvMjAxOSAxMjowMTo0NyBQTSIsIm5iZiI6MTU2ODI4OTcwNywiZXhwIjoxNTY4Mzc2MTA3LCJpc3MiOiJXZWJDaGF0LkNvbSIsImF1ZCI6IldlYiBDaGF0In0.80vma9A6SpuATQ9j5L6xzXVCmPFefigEoIKQOcS1c1g"}`);  

        return this.http.get<T[]>(this.apiUrl + "get", { headers: headers })
        .pipe(map((data: T[]) => data),  
            catchError(this.handleError)  
        );  
    }  
  
    public GetById(id) :Observable<T>{  
        var editUrl = this.apiUrl + 'GetById?id=' + id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        // headers = headers.append('Authorization', 'Bearer ' + `${this.token}`);  
        return this.http.get<T>(editUrl, { headers: headers }).pipe(tap(data => data),  
            catchError(this.handleError)  
        );  
    }  
  
    public Add(model: T) {  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        // headers = headers.append('Authorization', 'Bearer ' + `${this.token}`);  
        return this.http.post<any>(this.apiUrl + "add", model, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  

    public Update(model: T) {  
        var putUrl = this.apiUrl + '/update?id=' + model.id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        // headers = headers.append('Authorization', 'Bearer ' + `${this.token}`);  
        return this.http.put<any>(putUrl, model, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  
  
    public Delete(id) {  
        var deleteUrl = this.apiUrl + '/delete?id=' + id;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        // headers = headers.append('Authorization', 'Bearer ' + `${this.token}`);  
        return this.http.delete<any>(deleteUrl, { headers: headers })  
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