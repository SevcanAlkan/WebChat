import { Injectable } from '@angular/core';
import { MessageSearchVM } from '@models/message';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable()
export class SearchService {
    protected apiUrl = "";  

    constructor(protected http: HttpClient) {          
        this.apiUrl = environment.apiUrl + "/api/search/";
    }  

    public Get(key) : Observable<MessageSearchVM[]>{  
        var url = this.apiUrl + 'Get?key=' + key;  
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  
        return this.http.get<MessageSearchVM[]>(url, { headers: headers }).pipe(tap(data => data));  
    }  
}