import { Injectable } from '@angular/core';  
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { BaseVM } from './baseModel';
import { APIResultVM } from '@app/common/APIResultVM';
import { BaseService } from './baseService';

@Injectable({  
    providedIn: 'root'  
})  
  
//Add API version header!

export abstract class BaseServiceCRUD<T extends BaseVM, U extends BaseVM> extends BaseService{  
  
    constructor(protected http: HttpClient, _controllerName: string) {  
        super(http,_controllerName);
    }  
  
    public GetAll() : Observable<T[]> {  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });   
        return this.http.get<T[]>(this.apiUrl + "get", { headers: headers })
        .pipe(map((data: T[]) => data),  
            catchError(this.handleError)  
        );  
    }  
  
    public GetById(id) : Observable<APIResultVM>{  
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
}  