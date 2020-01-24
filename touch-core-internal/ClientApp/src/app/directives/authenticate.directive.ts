import { Directive, ElementRef } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { environment } from '../../environments/environment';


@Directive({
  selector: '[appAuthenticate]'
})
export class AuthenticateDirective {

  constructor(private el: ElementRef, private authService: AuthService) { }

  ngOnInit() {
    var hasPermission = this.authService.isUserLoggedIn();
    if (!hasPermission) {
        //window.location.replace(environment.apiUrl + "/home");
      // var element = document.getElementById(this.el.nativeElement.id)
      // var parentElement = element.parentNode;
      // parentElement.removeChild(element);
    }
  }

}
