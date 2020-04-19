import { Injectable, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { Client } from '@microsoft/microsoft-graph-client';
import { AppSettings } from '../../app.config';
import { User } from '../../model/user';

@Injectable()
export class AuthService implements OnInit {
    public authenticated: boolean = false;
    public isAuthorized: boolean = false;
    public user: User;
    public static nope: Client

    constructor(private msalService: MsalService) {

        console.log("Auth service is hit");
        if (sessionStorage.getItem('accessToken') != null)
            this.authenticated = true;

        var promise = new Promise((resolve, reject) => {
            this.getUser().then(() => {
                console.log("User request completed");
                resolve();
            });
        });
    }

    ngOnInit() { }


    public async signIn(): Promise<any> {
        let result = await this.msalService.loginPopup()
            .catch((reason) => {
                // this.alertsService.add('Login failed', JSON.stringify(reason, null, 2));
            });

        if (result) {
            sessionStorage.setItem("accessToken", result);
            console.log("Token from storage: " + sessionStorage.getItem('accessToken'));
            this.authenticated = true;
            return this.user = await this.getUser();
        }
    }

    // Sign out
    public signOut(): void {
        sessionStorage.removeItem('accessToken');
        sessionStorage.removeItem('hasPermission');
        this.authenticated = false;
        this.msalService.logout();
        this.user = null;
    }

    public async getUser(): Promise<User> {
        if (!this.authenticated) return null;

        let graphClient = Client.init({
            // Initialize the Graph client with an auth
            // provider that requests the token from the
            // auth service
            authProvider: async (done) => {
                let token = await this.getAccessToken()
                    .catch((reason) => {
                        done(reason, null);
                    });
                console.log("Token from graph:" + token)
                if (token) {
                    done(null, token);
                } else {
                    done("Could not get an access token", null);
                }
            }
        });

        let graphUser = await graphClient.api('/me').get();
        let graphUserGroups = await graphClient.api('/me/memberOf').get();

        let isAuthorized = graphUserGroups.value.filter(x => x.id == AppSettings.mangementGroupId).length > 0
        sessionStorage.setItem("hasPermission", isAuthorized ? "true" : "false");

        // let graphUserGroups = await graphClient.api(groupUrl).get();

        this.user = new User();
        this.user.displayName = graphUser.displayName;
        // Prefer the mail property, but fall back to userPrincipalName
        this.user.email = graphUser.mail || graphUser.userPrincipalName;

        return this.user;
    }

    // Silently request an access token
    async getAccessToken(): Promise<string> {
        let result = await this.msalService.acquireTokenSilent(["user.read"])
            .catch((reason) => {
                console.log("Acquiring Access token failed");
            });

        if (result)
            console.log("Acesss Token acquired: " + result);
        else
            console.log("Acesss Token not acquired");

        return result;
    }

    public checkPermissions(): boolean {

        if (sessionStorage.getItem("hasPermission") != null)
            return JSON.parse(sessionStorage.getItem("hasPermission").toLowerCase());
        else
            return false;
    }
}
