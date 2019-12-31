import { Injectable } from '@angular/core';  
import { BaseServiceCRUD } from '@app/shared/BaseServiceCRUD';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GroupVM, GroupAddVM, GroupUpdateVM } from '@app/modules/chat/models/group';
import { APIVersion } from '@environments/APIVersion';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class GroupService extends BaseServiceCRUD<GroupVM, GroupAddVM, GroupUpdateVM> {
  constructor(http: HttpClient) {
    super(http, "group");    
  }

  public GetByUserId(userId: string, apiVersion: APIVersion = this.apiVerison) : Observable<GroupVM[]> {  
    let headers = this.getHeaders(apiVersion);  
    return this.http.get<GroupVM[]>(this.apiUrl + "GetByUserId?userId=" + userId, { headers: headers })
    .pipe(map((data: GroupVM[]) => data),  
        catchError(this.handleError)  
    );  
  }  

  public GetUsers(groupId: string, apiVersion: APIVersion = this.apiVerison) : Observable<string[]> {  
    let headers = this.getHeaders(apiVersion);
    return this.http.get<string[]>(this.apiUrl + "GetUsers?groupId=" + groupId, { headers: headers })
    .pipe(map((data: string[]) => data),  
        catchError(this.handleError)  
    );  
  } 
}
