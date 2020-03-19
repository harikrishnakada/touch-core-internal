import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../shared/shared.module';

import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { EmployeeService } from './services/employee.service';

@NgModule({
    declarations: [
        EmployeeListComponent,
        EmployeeFormComponent,
       
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        SharedModule
    ],
    providers: [EmployeeService]
})
export class DashoardModule { }
