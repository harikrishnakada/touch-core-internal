import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { AuthService } from '../../services/auth.service';
import { HttpService } from '../../services/http.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgForm } from '@angular/forms';
import { TimeSheetService } from '../../services/time-sheet.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { EmployeeService } from '../../services/employee.service';

@Component({
    selector: 'app-add-time-sheet',
    templateUrl: './add-time-sheet.component.html',
    styleUrls: ['./add-time-sheet.component.css']
})
export class AddTimeSheetComponent implements OnInit {

    @ViewChild('addTimeSheet', { static: false }) addTimeSheetForm: NgForm;

    public ToDateTime: Date = null;
    public FromDateTime: Date = null;
    public timeSheets: any = [];
    public allEmployees: any = [];
    promises: Promise<any>[] = [];

    newTimeSheet: any = {};
    duration: any = null;

    constructor(private auth: AuthService, private http: HttpService,
        private timeSheetService: TimeSheetService,
        private spinnerService: NgxSpinnerService, private empService: EmployeeService) { }

    ngOnInit() {
        this.promises.push(this.getAllEmployees());
    }

    maxDate() {
        return moment(this.newTimeSheet.fromTime).add(1, 'days')["_d"];
    }

    onSubmit(timeSheet: any): Promise<any> {
        this.spinnerService.show();

        return this.getEmployee(this.auth.user.email).then((employee) => {
            this.newTimeSheet.employeeId = employee.id;
            var body = {
                "EmployeeId": employee.id,
                "FromDateTime": this.FromDateTime,
                "ToDateTime": this.ToDateTime
            }

            this.http.post(`/timeSheet`, body).toPromise().then(() => {
                this.reset();
                // this.rewardAdded.emit(form.value);
                this.spinnerService.hide();
            });
        });
    }

    reset() {
        this.addTimeSheetForm.reset();
    }

    getEmployee(username: any): Promise<any> {
        return this.http.get(`/employee/${username}`).toPromise();
    }

    getAllEmployees(): Promise<any> {
        return this.empService.GetEmployees().then((resp) => {
            this.allEmployees = resp;
        })
    }

    getDuration(fromDate: any, toDate: any) {
        return this.timeSheetService.getDuration(this.FromDateTime, this.ToDateTime);
    }

}
