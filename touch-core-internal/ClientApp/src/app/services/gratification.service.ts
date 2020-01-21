import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GratificationService {

  constructor(private http: HttpClient) { }

  async GetBadges(): Promise<any> {
      return new Promise((resolve, reject) => {
          this.http.get(environment.baseUrl + "/badge").subscribe((resp: Response) => {
              resolve(resp);
          });
      });
  }
}
