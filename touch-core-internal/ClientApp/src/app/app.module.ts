//Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
//import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { RouterModule } from '@angular/router';

import { SharedModule } from './shared/shared.module'
import { AppRoutingModule } from './app-routing.module'

//Components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CoreModule } from './core/core.module';
import { GratificationModule } from './gratification/gratification.module';
import { AttendanceModule } from './attendance/attendance.module';
import { EmployeeModule } from './employee/employee.module';

//import { Observable } from 'rxjs/Observable';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        //NgbModule,
        CommonModule,
        SharedModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        CoreModule,
        EmployeeModule,
        GratificationModule,
        AttendanceModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
