import { Directive, ElementRef, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

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
