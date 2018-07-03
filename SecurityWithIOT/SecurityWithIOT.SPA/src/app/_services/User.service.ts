import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/User';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { AuthHttp } from 'angular2-jwt';

@Injectable()
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private authHttp: AuthHttp) {}

  getUsers(): Observable<User[]> {
    return this.authHttp
      .get(this.baseUrl + 'users')
      .map(response => <User[]>response.json())
      .catch(this.errorHandler);
  }

  getUser(id): Observable<User> {
    return this.authHttp
    .get(this.baseUrl + 'users/' + id)
    .map(response => <User>response.json())
    .catch(this.errorHandler);
  }

  updateUser(id: number, user: User) {
    return this.authHttp.put(this.baseUrl + 'users/' + id, user).catch(this.errorHandler);
  }

  setMainPhoto(userId: number, id: number) {
    return this.authHttp.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain', {}).catch(this.errorHandler);
  }

  deletePhoto(userId: number, id: number) {
    return this.authHttp.delete(this.baseUrl + 'users/' + userId + '/photos/' + id).catch(this.errorHandler);
  }

  private errorHandler(error: any) {
    const applicationError = error.headers.get('Application-Error');
    if (applicationError) {
      return Observable.throw(applicationError);
    }
    const serverError = error.json();
    let modelStateErrors = '';
    if (serverError) {
      for (const key in serverError) {
        if (serverError[key]) {
          modelStateErrors += serverError[key] + '\n';
        }
      }
    }
    return Observable.throw(modelStateErrors || 'Server Error');
  }
}
