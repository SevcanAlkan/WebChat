import { Component, OnInit } from '@angular/core';
import { GroupService } from '@services/GroupService';
import { Group } from '@models/Group';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  private group: Group = new Group();

  constructor( private groupService :GroupService) {
      this.group.name = "isim";
      this.group.description = "aciklama";
      this.group.isPrivate = true;
   }

  ngOnInit() {
  }

  //Pass id data to here, for edit or delete
  //If any record dont have id value in save() create new recored. Or update
  //Set isMain is false on Saving new record and update old record
  //If old record is already isMain, dont change this value
  //turncate values before send API

  save(){
    
  }
}
