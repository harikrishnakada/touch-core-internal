import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgForm } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../core/services/auth.service';
import { HttpService } from '../../shared/services/http.service';
import { AttendanceService } from '../attendance.service';

@Component({
    selector: 'app-time-sheet',
    templateUrl: './time-sheet.component.html',
    styleUrls: ['./time-sheet.component.css']
})
export class TimeSheetComponent implements OnInit, OnDestroy {

    //@ViewChild('addTimeSheet', { static: false }) addTimeSheetForm: NgForm;
    @ViewChild(DataTableDirective, { static: false })
    dtElement: DataTableDirective;

    public ToDateTime: Date = null;
    public FromDateTime: Date = null;
    public timeSheets: any = [];
    public allEmployees: any = [];

    newTimeSheet: any = {};
    duration: any = null;
    promises: Promise<any>[] = [];

    dtOptions: {};
    dtTrigger: Subject<any> = new Subject<any>();

    constructor(private auth: AuthService, private http: HttpService,
        private attendanceService: AttendanceService,
        private spinnerService: NgxSpinnerService, private toastr: ToastrService) { }

    ngOnInit() {
        this.spinnerService.show();
        this.promises.push(this.getTimeSheets());
        //this.promises.push(this.getAllEmployees());

        Promise.all(this.promises).then(() => {
        this.spinnerService.hide();
        });

        this.dtOptions = {
            pagingType: 'full_numbers',
            // columnDefs: [
            //   {
            // }
            // ],
            lengthMenu: [5, 20, 40],
            pageLength: 10,
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

    onTimeSheetAdded(newTimeSheet: any) {
        console.log(newTimeSheet);
        this.toastr.success('Sucess');
        this.reRender();
    }

    reRender(): void {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
            // Destroy the table first
            dtInstance.destroy();
            this.ngOnInit();
        });
    }

    ngOnDestroy(): void {
        // Do not forget to unsubscribe the event
        this.dtTrigger.unsubscribe();
    }

    getTimeSheets(): Promise<any> {
        return this.attendanceService.getTimeSheets().toPromise().then((resp) => {
            this.timeSheets = resp;
            this.dtTrigger.next();
        })
    }

}
