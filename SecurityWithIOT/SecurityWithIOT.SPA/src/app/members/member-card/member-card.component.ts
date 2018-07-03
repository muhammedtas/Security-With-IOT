import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../_models/User';
import { Photo } from '../../_models/Photo';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;
  @Input() photos: Photo[];
  constructor() { }

  ngOnInit() {
  }

}
