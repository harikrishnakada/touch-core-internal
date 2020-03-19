import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TimeSheetComponent } from './time-sheet/time-sheet.component';

const routes: Routes = [
    { path: 'timeSheet', component: TimeSheetComponent },
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AttendanceRoutingModule { }
