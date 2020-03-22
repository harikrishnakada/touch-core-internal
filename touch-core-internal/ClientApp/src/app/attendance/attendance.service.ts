import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';
import { HttpService } from '../shared/services/http.service';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
  })
export class AttendanceService {

    constructor(private httpService: HttpService){}

    getTimeSheets() {
        return this.httpService.get(environment.baseUrl + "/timesheet");
    }

    getTimeSheetsByHour() {
        return this.httpService.get(environment.baseUrl + "/timesheet/hours/6");
    }

    getEmployees() {
        return this.httpService.get(environment.baseUrl + "/employee")
    }

    upsertTimeSheet(body: any) {
        return this.httpService.post(environment.baseUrl + "/timeSheet", body)
    }

    getDuration(fromTime: any, toTime: any) {
        var d1 = new Date(fromTime);
        var d2 = new Date(toTime);
        var totalDuration = null;
        if (fromTime && toTime)
            totalDuration = moment.duration(Math.abs(<any>d1 - <any>d2))["_data"];

        if (totalDuration != null) {
            return totalDuration.hours + (totalDuration.minutes / 60);
        }
    }
}
