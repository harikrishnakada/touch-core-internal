import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { HttpService } from './http.service';
import * as moment from 'moment';

@Injectable({
    providedIn: 'root'
  })
export class TimeSheetService {

    constructor(private httpService: HttpService){}

    async GetTimeSheets(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.httpService.get("/timesheet").toPromise().then((resp: Response) => {
                resolve(resp);
            });
        });
    }

    async GetTimeSheetsByHour(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.httpService.get("/timesheet/hours/6").toPromise().then((resp: Response) => {
                resolve(resp);
            });
        });
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
