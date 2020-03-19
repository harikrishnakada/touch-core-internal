import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { HttpService } from '../shared/services/http.service';

@Injectable()
export class GratificationService {

    constructor(private http: HttpService) { }

    GetBadges() {
        return this.http.get(environment.baseUrl + "/badge")
    }

    GetRewards(){
        return this.http.get(environment.baseUrl + "/reward")
    }

    GetEmployees(){
        return this.http.get(environment.baseUrl + "/employee")
    }
}
