import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  User = {
    UserName: 'Sevcan',
    Status: 1
  };

  constructor() { }

  ngOnInit() {

  }

}
