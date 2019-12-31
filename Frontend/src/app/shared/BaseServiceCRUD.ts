import { Injectable } from '@angular/core';  
import { Observable, fromEvent } from 'rxjs';
import { catchError, tap, map, retry, delay } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';  
import { BaseVM, AddVM, UpdateVM } from './models/BaseModel';
import { APIResultVM } from '@app/shared/models/APIResultVM';
import { BaseService } from './baseService';
import { APIVersion } from '@environments/APIVersion';

@Injectable({  
    providedIn: 'root'  
})  

export abstract class BaseServiceCRUD<V extends BaseVM, A extends AddVM, U extends UpdateVM> extends BaseService{  
  
    constructor(protected http: HttpClient, _controllerName: string) {  
        super(http, _controllerName);
    }      

    public GetAll(apiVersion: APIVersion = this.apiVerison) : Observable<V[]> {        
        let headers = this.getHeaders(apiVersion);
        
        return this.http.get<V[]>(this.apiUrl + "get", { headers: headers })            
            .pipe(
                map((data: V[]) => data),  
                catchError(this.handleError), 
                retry(3), 
                delay(1000)
            );  
    }  
  
    public GetById(id: string, apiVersion: APIVersion = this.apiVerison) : Observable<APIResultVM> {
        let headers = this.getHeaders(apiVersion);       
        return this.http.get<APIResultVM>(this.apiUrl + "GetById", { params: { "id": id }, headers: headers })
            .pipe(tap(data => data),  
                catchError(this.handleError)  
            );  
    }  
  
    public Add(model: A, apiVersion: APIVersion = this.apiVerison) : Observable<APIResultVM> {         
        let headers = this.getHeaders(apiVersion);        
        return this.http.post<APIResultVM>(this.apiUrl + "add", model, { headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  

    public Update(model: U, id: string, apiVersion: APIVersion = this.apiVerison) : Observable<APIResultVM> {  
        let headers = this.getHeaders(apiVersion);  
        return this.http.put<APIResultVM>(this.apiUrl + "update", model, { params: { "id": id }, headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }  
  
    public Delete(id: string, apiVersion: APIVersion = this.apiVerison) : Observable<APIResultVM> {  
        let headers = this.getHeaders(apiVersion);
        return this.http.delete<APIResultVM>(this.apiUrl + "delete", { params: { "id": id }, headers: headers })  
            .pipe(  
                catchError(this.handleError)  
            );  
    }      
}  