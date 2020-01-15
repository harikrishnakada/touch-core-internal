import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
    title = 'app';

    constructor(private authService: AuthService) {}

    ngOnInit() {
        console.log("App Component is rendered");
        console.log(sessionStorage.getItem('accessToken'));

       // this.authService.getUser();
      
    }
}
