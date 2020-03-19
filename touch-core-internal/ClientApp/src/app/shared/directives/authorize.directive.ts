import { Directive, ElementRef, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { EmployeeService } from '../../dashoard/services/employee.service';
import { GratificationService } from '../../gratification/gratification.service';

@Directive({
    selector: '[appAuthorize]'
})
export class AuthorizeDirective implements OnInit {

    constructor(private el: ElementRef, private authService: AuthService) { }

    ngOnInit() {
        var hasPermission = this.authService.checkPermissions();
        if (!hasPermission)
            this.el.nativeElement.style.display = 'none';
    }

}
