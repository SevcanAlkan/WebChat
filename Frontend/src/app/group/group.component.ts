import { Component, OnInit } from '@angular/core';
import { GroupService } from '@services/GroupService';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  constructor( private groupService :GroupService,) { }

  ngOnInit() {
  }

}
