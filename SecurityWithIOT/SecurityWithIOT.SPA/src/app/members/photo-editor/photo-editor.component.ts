import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Photo } from '../../_models/Photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/User.service';
import { AlertifyService } from '../../_services/alertify.service';
import * as _ from 'underscore'; // underscore altında ne varsa al demek bu _ adına atayarak da. 111.

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() photos: Photo[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMain: Photo;
  @Output() getMemberPhotoChange = new EventEmitter<string>();

  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    // 109
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain,
          userId: res.userId // Burada bir problem olabilir.
        };
        this.photos.push(photo);
        if (photo.isMain) { // 130 alt taraftan kopyaladık.
          this.authService.changeMemberPhoto(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
      }
    };
  }
 // 111
  setMainPhoto (photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      console.log('Successfully set main photo');
      this.currentMain = _.findWhere(this.photos, {isMain: true});
      this.currentMain.isMain = false;
      photo.isMain = true;
      // this.getMemberPhotoChange.emit(photo.url); 116
      this.authService.changeMemberPhoto(photo.url); // 116
      this.authService.currentUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
      // Değişen foto aynı anda db ye yazlsın ve ekrana gelsin diye
    }, error => {
      this.alertify.error(error);
    });
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Are you sure you want to delete this photo', () => {
      this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
        this.photos.splice(_.findIndex(this.photos, { id: id}), 1);
        this.alertify.success('Photo has been deleted');
      }, error => {
        this.alertify.error('Failed to delete photo');
      });
    });
  }
}
