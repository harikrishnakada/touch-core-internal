import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { HttpService } from '../../shared/services/http.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

    constructor(private http: HttpService) { }

    async GetEmployees(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.http.get(environment.baseUrl+ "/employee").subscribe((resp: Response) => {
               // this.employees = resp;
                // Calling the DT trigger to manually render the table
               // this.dtTrigger.next();
                resolve(resp);
            });
        });
    }
}
