import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module';

import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { EmployeeService } from './employee.service';
import { EmployeeRoutingModule } from './employee-routing.module';

@NgModule({
    declarations: [
        EmployeeListComponent,
        EmployeeFormComponent,
    ],
    imports: [
        CommonModule,
        EmployeeRoutingModule,
        SharedModule
    ],
    providers: [EmployeeService]
})
export class EmployeeModule { }
