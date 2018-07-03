import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_services/User.service';
import { AlertifyService } from '../_services/alertify.service';
// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';

@Injectable()
export class MemberDetailResolver implements Resolve<User> {

    constructor(private userService: UserService,
         private alertify: AlertifyService,
          private router: Router) {}

          resolve(route: ActivatedRouteSnapshot): Observable<User> {
              return this.userService.getUser(route.params['id']).catch(error => {
                this.alertify.error('Problem retreiving data');
                this.router.navigate(['/members']);
                return Observable.of(null);
              });
          }
}
