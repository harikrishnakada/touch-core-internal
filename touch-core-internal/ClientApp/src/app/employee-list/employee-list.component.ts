import { Component, OnInit } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: any = [];
  dtOptions: DataTables.Settings = {};

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get('https://9253ccf3-458a-4b37-8c2e-9a5b8b07d344.mock.pstmn.io/employees').subscribe((resp: Response) => {
      this.employees = resp;
     });
   
     this.dtOptions = {
      columnDefs: [
        { defaultContent: "", targets: "_all" },
        { targets: [1, 2, 3, 4, 5, 6], orderable: true },
        { targets: "_all", orderable: false }
      ],
      language: {
        info: "Items _START_ to _END_ of _TOTAL_",
        lengthMenu: "Page Size:  _MENU_",
        processing: "",
        zeroRecords: "No data available"
    },
    dom: "<'row'<'col-sm-3'B>>" + "<'row'<'col-sm-12'tr>>" +
        "<'row table-control-row'<'col-sm-3'i><'col-sm-3'l><'col-sm-6'p>>",
    processing: false,
    info: true,
    paging: true,
    searching: false,
    pageLength: 1,
    lengthChange: false,
    autoWidth: false,
    destroy: true,
      // Configure the buttons
    }
  }
}
