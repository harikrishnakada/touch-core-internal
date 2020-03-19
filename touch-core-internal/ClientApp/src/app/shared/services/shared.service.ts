import { Injectable } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

    constructor(private authService: AuthService) { }

    checkPermissions() {
        var hasPermission = this.authService.checkPermissions();
        return hasPermission;
    }
}
