import { Component, OnInit, ViewChild } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { NgxSpinnerService } from "ngx-spinner";

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

    constructor(private http: HttpClient, private SpinnerService: NgxSpinnerService) { }

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

    async GetEmployees(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.http.get('https://c6b60f2b-52d4-4233-88ea-6c1ed0158c18.mock.pstmn.io/Employees').subscribe((resp: Response) => {
                this.employees = resp;
                // Calling the DT trigger to manually render the table
                this.dtTrigger.next();
                resolve();
            });
        });
    }
}
