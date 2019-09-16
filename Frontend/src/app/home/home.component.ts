import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Group } from '@models/Group';
import { User } from '@models/User';

import { UserService } from '@services/UserService';
import { GroupService } from '@services/GroupService';
import { AuthenticationService } from '@services/AuthenticationService';
import { ChatService } from '@services/ChatService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit  {  

  private GroupList: Group[] = [];  
  private currentGroup: Group = new Group();
  private currentUser: User = new User(); 
   
  NavbarToggle = true;

  constructor(private _Route: Router,
      private authenticationService: AuthenticationService,
      private userService: UserService,
      private groupService: GroupService,
      private chatService: ChatService) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);             
      this.chatService.CurrentUser.next(this.currentUser);

      this.chatService.CurrentGroup.subscribe(x => this.currentGroup = x);      
  }

  ngOnInit() {
    //Load groups
    this.getGroups();

    var users;
    this.userService.getUserList().subscribe(x => {users = x}).unsubscribe();
    if(users){
      this.chatService.UserList.next(users);
    }     
  } 

  ngOnDestroy() {
    this.chatService.CurrentGroup.unsubscribe();
  }

  logout() {
    this.authenticationService.logout();
    this._Route.navigate(['/login']);
  }
  getGroups(){
    this.groupService.GetAll().forEach(data => {
      data.forEach(item=>{
        var group = new Group();
        group.id = item.id;
        group.name = item.name;
        group.description = item.description;
        group.isMain = item.isMain;
        group.isPrivate = item.isPrivate;

        if(group.isMain){
          this.currentGroup = group;
        }

        this.GroupList.push(group);
      })
    }).then(()=>{
      this.selectGroup(this.currentGroup.id, true);
    });
  } 
  get sortGroups() {
    this.GroupList = this.GroupList.sort((a, b) => a.name.localeCompare(b.name)).sort(a => {
      if(a.isMain){
        return -1;
      }else{
        return 1;
      }
    });
    return this.GroupList;
  }
  selectGroup(id, loadAnyWay:boolean=false){    
    if(id==null && id != this.currentGroup.id){
      return;
    }else if(this.currentGroup && id===this.currentGroup.id && !loadAnyWay){
      return;
    }

    var group = this.GroupList.find(item=>item.id===id);
    if(group==null){
      return;
    }  
    
    this.chatService.saveTempMessage();    
    this.chatService.CurrentGroup.next(group); 
  }
  
  getMessageCountForGroup(groupId){
    //   if(this.Mesa){
    //     var user = this.UserList.find(item=>item.id===id);
    //     if(user){
    //       return user.displayName;
    //     }else{
    //       return "Undefined User";
    //     }
    // }
    return 0; //Add feature to get unreaded messages
  }

  editGroup(){
    this._Route.navigate(['group', this.currentGroup.id]);
  }

  search(value){
    console.log(value);
  }

  toggleNavBar(){
    if(this.NavbarToggle){
      this.NavbarToggle = false;
    }else{
      this.NavbarToggle = true;
    }
  }
}
