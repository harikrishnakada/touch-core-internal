import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { AuthService } from '../services/auth.service';
import { HttpService } from '../services/http.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgForm } from '@angular/forms';
import { TimeSheetService } from '../services/time-sheet.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-time-sheet',
    templateUrl: './time-sheet.component.html',
    styleUrls: ['./time-sheet.component.css']
})
export class TimeSheetComponent implements OnInit {

    //@ViewChild('addTimeSheet', { static: false }) addTimeSheetForm: NgForm;

    public ToDateTime: Date = null;
    public FromDateTime: Date = null;
    public timeSheets: any = [];
    public allEmployees: any = [];

    newTimeSheet: any = {};
    duration: any = null;
    promises: Promise<any>[] = [];

    dtElement: DataTableDirective;
    dtOptions: {};
    dtTrigger: Subject<any> = new Subject<any>();

    constructor(private auth: AuthService, private http: HttpService,
        private timeSheetService: TimeSheetService,
        private spinnerService: NgxSpinnerService, private empService: EmployeeService) { }

    ngOnInit() {
        this.promises.push(this.getTimeSheets());
        //this.promises.push(this.getAllEmployees());

        this.dtOptions = {
            pagingType: 'full_numbers',
            // columnDefs: [
            //   {
            // }
            // ],
            lengthMenu: [5, 20, 40],
            pageLength: 5,
            dom: 'Bfrtip',
            // dom: "<'row'<'col-sm-3'B>>" + "<'row'<'col-sm-12'tr>>" +
            // "<'row table-control-row'<'col-sm-3'i><'col-sm-3'l><'col-sm-6'p>>",
            // buttons: [
            //  'copy',
            //  'print',
            //  'excel',
            // ]
            "buttons": [
                { "extend": 'print', "text": 'Print', "className": 'fa fa-print btn btn-info btn-md' },
            ],
        };
    }

    getDuration(fromTime: any, toTime: any) {
        var d1 = new Date(fromTime);
        var d2 = new Date(toTime);
        var totalDuration = null;
        if (fromTime && toTime)
            totalDuration = moment.duration(Math.abs(<any>d1 - <any>d2))["_data"];

        if (totalDuration != null) {
            this.duration = totalDuration.hours + (totalDuration.minutes / 60);
        }
        return this.duration;
    }

    //maxDate() {
    //    return moment(this.newTimeSheet.fromTime).add(1, 'days')["_d"];
    //}

    //onSubmit(timeSheet: any): Promise<any> {
    //    this.spinnerService.show();

    //    return this.getEmployee(this.auth.user.email).then((employee) => {
    //        this.newTimeSheet.employeeId = employee.id;
    //        var body = {
    //            "EmployeeId": employee.id,
    //            "FromDateTime": this.FromDateTime,
    //            "ToDateTime": this.ToDateTime
    //        }

    //        this.http.post(`/timeSheet`, body).toPromise().then(() => {
    //            this.reset();
    //            // this.rewardAdded.emit(form.value);
    //            this.spinnerService.hide();
    //        });
    //    });
    //}

    //reset() {
    //    this.addTimeSheetForm.reset();
    //}

    //getEmployee(username: any): Promise<any> {
    //    return this.http.get(`/employee/${username}`).toPromise();
    //}

   

    getTimeSheets(): Promise<any> {
        return this.timeSheetService.GetTimeSheets().then((resp) => {
            this.timeSheets = resp;
            this.dtTrigger.next();
        })
    }

}
