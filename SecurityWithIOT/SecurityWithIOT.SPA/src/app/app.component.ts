import { Component, OnInit } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';
import { AuthService } from './_services/auth.service';
import { User } from './_models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Security With IOT';
  jwtHelper: JwtHelper = new JwtHelper();

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
      // 130. yeni register olmuş kullanıcı çıkış yapıp geri girdiğinde default atanan fotoyu göremiyor diye.
      if (this.authService.currentUser.photoUrl !== null) {
        this.authService.changeMemberPhoto(user.photoUrl); // 116
      } else {
        this.authService.changeMemberPhoto('../assets/user.png');
      }

    }
  }

}
