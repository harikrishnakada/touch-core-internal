import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ToastrModule } from 'ngx-toastr';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { ChartModule } from 'angular-highcharts';
import { DataTablesModule } from 'angular-datatables';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgSelectModule, NgOption } from '@ng-select/ng-select';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { SpinnerComponent } from './components/spinner/spinner.component';

import { AuthorizeDirective } from './directives/authorize.directive';
import { HttpService } from './services/http.service';
import { AuthService } from '../core/services/auth.service';
import { SharedService } from './services/shared.service';


@NgModule({
    declarations: [AuthorizeDirective, SpinnerComponent],
  imports: [
      CommonModule,
      ChartModule,
      MDBBootstrapModule.forRoot(),
      HttpClientModule,
      FormsModule,
      NgxSpinnerModule,
      DataTablesModule,
      NgSelectModule,
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
      ToastrModule.forRoot(),
    ],
  providers:[HttpService, SharedService],
    exports: [AuthorizeDirective, SpinnerComponent, CommonModule, ChartModule, FormsModule,DataTablesModule,NgSelectModule,
        OwlDateTimeModule, OwlNativeDateTimeModule, MDBBootstrapModule, ToastrModule]
})
export class SharedModule { }
