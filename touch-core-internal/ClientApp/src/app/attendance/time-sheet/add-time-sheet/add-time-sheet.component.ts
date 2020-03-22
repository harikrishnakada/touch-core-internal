import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgForm } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';
import { HttpService } from '../../../shared/services/http.service';
import { AttendanceService } from '../../attendance.service';
import { EmployeeService } from '../../../employee/employee.service';

@Component({
    selector: 'app-add-time-sheet',
    templateUrl: './add-time-sheet.component.html',
    styleUrls: ['./add-time-sheet.component.css']
})
export class AddTimeSheetComponent implements OnInit {

    @ViewChild('addTimeSheet', { static: false }) addTimeSheetForm: NgForm;

    @Output() timeSheetAdded = new EventEmitter<any>();

    public ToDateTime: Date = null;
    public FromDateTime: Date = null;
    public timeSheets: any = [];
    public allEmployees: any = [];
    promises: Promise<any>[] = [];

    newTimeSheet: any = {};
    duration: any = null;

    constructor(private auth: AuthService, private http: HttpService,
        private attendanceService: AttendanceService, private empService: EmployeeService,
        private spinnerService: NgxSpinnerService) { }

    ngOnInit() {
        this.promises.push(this.getAllEmployees());
    }

    maxDate() {
        if (this.FromDateTime) {
            var maxdate = moment(this.FromDateTime).add(1, 'days')["_d"];
            return moment(this.FromDateTime).add(1, 'days')["_d"];
        }
    }

    onSubmit(timeSheet: any): Promise<any> {
        this.spinnerService.show();

        if (this.newTimeSheet.employeeId == null) {
            return this.getEmployee(this.auth.user.email).then((employee) => {
                this.newTimeSheet.employeeId = employee.id;
                var body = {
                    "EmployeeId": employee.id,
                    "FromDateTime": this.FromDateTime,
                    "ToDateTime": this.ToDateTime
                }

                this.attendanceService.upsertTimeSheet(body).toPromise().then(() => {
                    this.reset();
                    this.timeSheetAdded.emit(this.newTimeSheet);
                    this.spinnerService.hide();
                });
            });
        }
        else {
            var body = {
                "EmployeeId": this.newTimeSheet.employeeId,
                "FromDateTime": this.FromDateTime,
                "ToDateTime": this.ToDateTime,
                "Comments": this.newTimeSheet.Comments
            }

            this.attendanceService.upsertTimeSheet(body).toPromise().then(() => {
                this.reset();
                this.timeSheetAdded.emit(this.newTimeSheet);
                this.spinnerService.hide();
            });
        }
    }

    reset() {
        this.addTimeSheetForm.reset();
    }

    getEmployee(username: any): Promise<any> {
        return this.empService.getEmployeeByUserName(username).toPromise();

    }

    getAllEmployees() {
        return this.empService.GetEmployees().then((resp) => {
            this.allEmployees = resp;
        })
    }

    getDuration(fromDate: any, toDate: any) {
        return this.attendanceService.getDuration(this.FromDateTime, this.ToDateTime);
    }

}
