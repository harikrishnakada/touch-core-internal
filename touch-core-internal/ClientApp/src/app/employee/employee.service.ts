import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../shared/services/http.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

    constructor(private httpService: HttpService) { }

    async GetEmployees(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.httpService.get(environment.baseUrl+ "/employee").subscribe((resp: Response) => {
               // this.employees = resp;
                // Calling the DT trigger to manually render the table
               // this.dtTrigger.next();
                resolve(resp);
            });
        });
    }

    getEmployeeByUserName(username: any) {
        return this.httpService.get(environment.baseUrl + "/employee")
    }
}
