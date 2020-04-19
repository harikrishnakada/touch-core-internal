import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BroadcastService } from '@azure/msal-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy{
    title = 'app';

    private subscription: Subscription;
    isLoggedIn: boolean;

    constructor(private authService: AuthService, private router: Router, private broadcastService: BroadcastService) {

        this.isLoggedIn = this.authService.authenticated;

        if (!this.authService.authenticated) {
            this.router.navigate(["./"]);
        }

        //MSAL wrapper provides various callbacks for various operations. For all callbacks, you need to inject BroadcastService as a dependency in your component/service.
        // And It is extremely important to unsubscribe to avoid memory leaks.
        //For more detials please visit https://www.npmjs.com/package/@azure/msal-angular
        this.subscription = this.broadcastService.subscribe('msal:loginSuccess', () => {
            location.reload();
        });

        this.subscription = this.broadcastService.subscribe("msal:loginSuccess", payload => {
            location.reload();
        });
    }

    ngOnDestroy() {
        this.broadcastService.getMSALSubject().next(1);
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    ngOnInit() {
        console.log("App Component is rendered");
        console.log(sessionStorage.getItem('accessToken'));
    }
}
