import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NgxSpinnerService } from "ngx-spinner";  
import { resolve } from 'url';
import { reject } from 'q';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
    isLoggedIn: boolean = true;

    constructor(public authService: AuthService, private SpinnerService: NgxSpinnerService) {
    }

    ngOnInit() {
        this.isLoggedIn = !!sessionStorage.getItem('accessToken');
    }

    async signIn(): Promise<void> {
        this.SpinnerService.show();
        await this.authService.signIn().then(() => {
            this.SpinnerService.hide();
        });
    }

    signOut(): void {
        this.authService.signOut();
        this.isLoggedIn = false;
    }
}
