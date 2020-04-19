import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


@Injectable()
export class HttpService {

  constructor(private http: HttpClient) { }

  get<T>(url: string): Observable<any> {
      return this.http.get<T>(url);
  }

  post<T>(url: string, body: any, dataTableParameter?: any): Observable<any>{
      return this.http.post(url, body);
  }

  put<T>(url: string, body: string): Observable<T> {
      return this.http.put<T>(environment.baseUrl + url, body);
  }

  delete<T>(url: string): Observable<T> {
      return this.http.delete<T>(url);
  }

  patch<T>(url: string, body: string): Observable<T> {
      return this.http.patch<T>(url, body);
  }
}
