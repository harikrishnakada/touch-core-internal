import { Component, OnInit } from '@angular/core';

import { AuthService } from '../services/auth.service';
import { User } from 'msal';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    isExpanded = false;
    isLoggedIn: boolean = true;
    setting: string = "";

    constructor(private authService: AuthService) {
    }

    ngOnInit() {
        this.isLoggedIn = !!sessionStorage.getItem('accessToken');
    }

    async signIn(): Promise<void> {
        await this.authService.signIn().then(function(data) {
            this.isLoggedIn = true;
        }).catch(() => {
            this.isLoggedIn = false;
        });
    }

    signOut(): void {
        this.authService.signOut();
        this.isLoggedIn = false;
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
