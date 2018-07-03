import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { tokenNotExpired, JwtHelper } from 'angular2-jwt';

import { Observable } from 'rxjs/Observable';
import { User } from '../_models/User';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  userToken: any;
  decodedToken: any;
  currentUser: User;
  jwtHelper: JwtHelper = new JwtHelper();
  private photoUrl = new BehaviorSubject<string>('../../assets/user.png'); // 116
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: Http) {}

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(model: any) {
    return this.http
      .post(this.baseUrl + 'login', model, this.requestOptions())
      .map((response: Response) => {
        const user = response.json();
        if (user && user.tokenString) {
          localStorage.setItem('token', user.tokenString);
          localStorage.setItem('user', JSON.stringify(user.user)); // 114
          console.log('token was mapped to localStorage just now.');
          this.decodedToken = this.jwtHelper.decodeToken(user.tokenString); // This is where we are getting exception.
          this.currentUser = user.user; // 114
          console.log(this.decodedToken);
          this.userToken = user.tokenString;
          // this.changeMemberPhoto(this.currentUser.photoUrl);
          // yeni bir kullanıcı register olduğunda üstte verdiğimiz photo url i bu kısım null alarak eziyor. Bunu çözdük.
          if (this.currentUser.photoUrl !== null) {
            this.changeMemberPhoto(this.currentUser.photoUrl);
          } else {
              this.changeMemberPhoto('../../assets/user.png'); // embed olan bir foto çaktık default.
          }
        }
      })
      .catch(this.errorHandler);
  }

  register(user: User) {
    return this.http
      .post(this.baseUrl + 'register', user, this.requestOptions())
      .catch(this.errorHandler);
  }

  loggedIn() {
    return tokenNotExpired('token');
  }

  private requestOptions() {
    const headers = new Headers({ 'Content-type': 'application/json' });
    return new RequestOptions({ headers: headers });
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
