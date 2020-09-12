import { Injectable } from '@angular/core';  
import { BaseServiceCRUD } from '@app/common/BaseServiceCRUD';
import { HttpClient } from '@angular/common/http';  
import { MessageVM, MessageAddVM, MessageUpdateVM } from '@app/models/Message';
import { map, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class MessageService extends BaseServiceCRUD<MessageVM, MessageAddVM, MessageUpdateVM> {

  constructor(http: HttpClient) {
    super(http, "message");    
  }

  public getByGroupId(groupId: string) : Observable<MessageVM[]> {
    let headers = this.getHeaders();

    return this.http.get<MessageVM[]>(this.apiUrl + "GetByGroupId?groupId=" + groupId, { headers: headers })
    .pipe(map((data: MessageVM[]) => data),  
        catchError(this.handleError)  
    );  
  }  
}
