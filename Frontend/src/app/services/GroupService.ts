import { Injectable } from '@angular/core';  
import { BaseServiceCRUD } from '@app/common/BaseServiceCRUD';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GroupVM, GroupAddVM, GroupUpdateVM } from '@app/models/Group';
import { APIVersion } from '@environments/APIVersion';

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class GroupService extends BaseServiceCRUD<GroupVM, GroupAddVM, GroupUpdateVM> {

  private GroupListSubject: BehaviorSubject<GroupVM[]>;
  public GroupList: Observable<GroupVM[]>;

  constructor(http: HttpClient) {
    super(http, "group");   
    
    if(!this.GroupList){
      this.GroupList = new Observable<GroupVM[]>();
    }
  }

  public ReLoadLocalList(){
    this.GetAll().subscribe(x => {
        x.forEach(item=>{
          var group = new GroupVM();
          group.id = item.id;
          group.name = item.name;
          group.description = item.description;
          group.isMain = item.isMain;
          group.isPrivate = item.isPrivate;
          group.users = [];

          this.GroupList.
    });
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
