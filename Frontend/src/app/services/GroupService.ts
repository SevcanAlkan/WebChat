import {Injectable} from '@angular/core';
import {Group} from '@app/Models/group';
import {BaseService} from '@app/common/BaseService';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

@Injectable()
export class GroupService extends BaseService<Group, Group> {
  constructor(http: HttpClient) {
    super(http, "group");
  }

  public GetByUserId(userId: string): Observable<Group[]> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.get<Group[]>(this.apiUrl + "GetByUserId?userId=" + userId, {headers: headers})
      .pipe(map((data: Group[]) => data),
        catchError(this.handleError)
      );
  }

  public GetUsers(groupId: string): Observable<string[]> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.get<string[]>(this.apiUrl + "GetUsers?groupId=" + groupId, {headers: headers})
      .pipe(map((data: string[]) => data),
        catchError(this.handleError)
      );
  }
}
