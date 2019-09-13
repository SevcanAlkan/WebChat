import { Injectable } from '@angular/core';  
import { BaseService } from '@common/BaseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Message } from '@models/message';
import { map, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class MessageService extends BaseService<Message> {
  constructor(http: HttpClient) {
    super(http, "message");    
  }

  public getByGroupId(groupId: string) : Observable<Message[]> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });  

    return this.http.get<Message[]>(this.apiUrl + "GetByGroupId?groupId=" + groupId, { headers: headers })
    .pipe(map((data: Message[]) => data),  
        catchError(this.handleError)  
    );  
  }  
}
