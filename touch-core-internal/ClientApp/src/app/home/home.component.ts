import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

    constructor(private authService: AuthService, private empService: EmployeeService) {

    }

    ngOnInit() {

    }
}
