import { Component, OnInit, ViewChild } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { NgxSpinnerService } from "ngx-spinner";
import { SharedService } from '../../shared/services/shared.service';
import { EmployeeService } from '../employee.service';

@Component({
    selector: 'app-employee-list',
    templateUrl: './employee-list.component.html',
    styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
    //@ViewChild(DataTablesModule)
    employees: any = [];
    dtElement: DataTablesModule;
    dtOptions: {};
    dtTrigger: Subject<any> = new Subject<any>();
    promises: Promise<any>[] = [];
    hasPermission: boolean;

    constructor(private http: HttpClient, private SpinnerService: NgxSpinnerService, private employeeService: EmployeeService, private sharedService: SharedService) {

        this.hasPermission = this.sharedService.checkPermissions();
    }

    ngOnInit() {
        this.SpinnerService.show();
        this.promises.push(this.GetEmployees());

        Promise.all(this.promises).then(() => {
            this.SpinnerService.hide();
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

    async GetEmployees(): Promise<any> {
        return this.employeeService.GetEmployees().then((result) => {
            this.employees = result;
            this.dtTrigger.next();
        });
    }
}
