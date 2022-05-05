import {Injectable} from '@angular/core';
import {MessageVM} from '@app/Models/message';
import {Observable} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {tap} from 'rxjs/operators';
import {environment} from '@environments/environment';

@Injectable()
export class SearchService {
  protected apiUrl = "";

  constructor(protected http: HttpClient) {
    this.apiUrl = environment.apiUrl + "/api/search/";
  }

  public Get(key): Observable<MessageVM[]> {
    var url = this.apiUrl + 'Get?key=' + key;
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.get<MessageVM[]>(url, {headers: headers}).pipe(tap(data => data));
  }
}
