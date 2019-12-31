import { Injectable } from '@angular/core';
import { MessageSearchVM } from '@app/modules/chat/models/message';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { BaseService } from '@app/shared/baseService';

@Injectable()
export class SearchService extends BaseService {
    protected apiUrl = "";  

    constructor(protected http: HttpClient) { 
        super(http, "search");         
    }  

    public Get(key: string) : Observable<MessageSearchVM[]> {  
        var url = this.apiUrl + 'Get?key=' + key;  
        let headers = this.getHeaders();
        return this.http.get<MessageSearchVM[]>(url, { headers: headers }).pipe(tap(data => data));  
    }  
}