import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_services/User.service';
import { AlertifyService } from '../_services/alertify.service';
// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MemberEditResolver implements Resolve<User> {

    constructor(private userService: UserService,
        private alertify: AlertifyService,
        private router: Router,
        private authService: AuthService) {}

        resolve(route: ActivatedRouteSnapshot): Observable<User> {
            return this.userService.getUser(this.authService.decodedToken.nameid).catch(error => {
            this.alertify.error('Problem Editing profile');
            this.router.navigate(['/members']);
            return Observable.of(null);
            });
        }
}
