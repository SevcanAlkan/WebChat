import { Injectable } from '@angular/core';  
import { Group } from '@app/models/Group';
import { BaseService } from '@common/BaseService';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';  

@Injectable({  
    providedIn: 'root'  
})  
  
@Injectable()
export class GroupService extends BaseService<Group> {
  constructor(http: HttpClient) {
    super(http, "group");    
  }
}
