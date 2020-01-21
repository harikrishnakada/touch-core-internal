//Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgSelectModule, NgOption } from '@ng-select/ng-select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MsalModule, MsalGuard } from '@azure/msal-angular';

//Components
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { BadgesComponent } from './badges/badges.component';
import { RewardsListComponent } from './rewards-list/rewards-list.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { RewardComponent } from './reward/reward.component';
import { RewardFormComponent } from './reward/reward-form/reward-form.component';

//Services
import { AuthService } from './services/auth.service';
import { AppSettings } from './app.config';

//Directives
import { AuthorizeDirective } from './directives/authorize.directive';


//import { Observable } from 'rxjs/Observable';

export const protectedResourceMap: [string, string[]][] = [['https://graph.microsoft.com/v1.0/me', ["user.read"]]];

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        EmployeeListComponent,
        EmployeeFormComponent,
        BadgesComponent,
        RewardsListComponent,
        AuthorizeDirective,
        HeaderComponent,
        FooterComponent,
        SpinnerComponent,
        RewardComponent,
        RewardFormComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        CommonModule,
        NgxSpinnerModule,
        DataTablesModule,
        NgSelectModule,
        BrowserAnimationsModule,
        MsalModule.forRoot({
            clientID: AppSettings.applicationId,
            authority: `${AppSettings.authority}/${AppSettings.tenantId}`,
            consentScopes: AppSettings.consentScopes,
            redirectUri: AppSettings.redirectUri,
            postLogoutRedirectUri: AppSettings.postLogoutRedirectUri,
            cacheLocation: AppSettings.cacheLocation,
            protectedResourceMap: protectedResourceMap
        }),
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'employee', component: EmployeeListComponent },
            { path: 'rewards', component: RewardComponent },
        ])
    ],
    providers: [AuthService],
    bootstrap: [AppComponent]
})
export class AppModule { }
