import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AttendanceRoutingModule } from './attendance-routing.module';
import { TimeSheetComponent } from './time-sheet/time-sheet.component';
import { AddTimeSheetComponent } from './time-sheet/add-time-sheet/add-time-sheet.component';
import { SharedModule } from '../shared/shared.module';
import { AttendanceService } from './attendance.service';

@NgModule({
    declarations: [TimeSheetComponent,
        AddTimeSheetComponent],
    imports: [
        CommonModule,
        AttendanceRoutingModule,
        SharedModule
    ],
    providers: [AttendanceService]
})
export class AttendanceModule { }
