import { Injectable } from '@angular/core';  
import { Group } from '@app/models/Group';
import { BaseService } from '@common/BaseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class GroupService extends BaseService<Group, Group> {
  constructor(http: HttpClient) {
    super(http, "group");    
  }

  public GetByUserId(userId: string):Observable<Group[]> {  
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });   
    return this.http.get<Group[]>(this.apiUrl + "GetByUserId?userId=" + userId, { headers: headers })
    .pipe(map((data: Group[]) => data),  
        catchError(this.handleError)  
    );  
  }  
}
