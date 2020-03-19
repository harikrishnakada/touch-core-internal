import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MsalModule, MsalGuard, MsalService } from '@azure/msal-angular';
import { RouterModule } from '@angular/router';
import { AppSettings } from '../app.config';
import { AuthService } from './services/auth.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SharedModule } from '../shared/shared.module';

export const protectedResourceMap: [string, string[]][] = [['https://graph.microsoft.com/v1.0/me', ["user.read"]]];

@NgModule({
    declarations: [NavMenuComponent, HeaderComponent, FooterComponent],
  imports: [
      CommonModule,
      RouterModule,
      SharedModule,
      MsalModule.forRoot({
          clientID: AppSettings.applicationId,
          authority: `${AppSettings.authority}/${AppSettings.tenantId}`,
          consentScopes: AppSettings.consentScopes,
          redirectUri: AppSettings.redirectUri,
          postLogoutRedirectUri: AppSettings.postLogoutRedirectUri,
          cacheLocation: AppSettings.cacheLocation,
          protectedResourceMap: protectedResourceMap
      }),
  ],
    providers: [AuthService, MsalService],
    exports: [NavMenuComponent, HeaderComponent, FooterComponent]
})
export class CoreModule { }
