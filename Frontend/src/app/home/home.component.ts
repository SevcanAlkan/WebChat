import { Component, OnInit } from '@angular/core';
import { UserService } from '@services/UserService';
import { Group } from '@models/Group';
import { User } from '@models/User';
import { GroupService } from '@services/GroupService';
import { AuthenticationService } from '@services/AuthenticationService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  private GroupService: GroupService;  
  private UserService: UserService;  

  private CurrentUser: User;
  CurrentGroup: Group;

  private GroupList: Group[] = []; 
  private UserList: User[] = [];  
  public UserTest:User;
  CurrentMessage = "";  
  messages;
  
  constructor(private _Route: Router,private authenticationService: AuthenticationService, private userService: UserService, private groupService :GroupService) {
    this.authenticationService.currentUser.subscribe(x => this.CurrentUser = x);   
    
    this.GroupService = groupService;  
    this.UserService = userService;
   }

  ngOnInit() {
    //Load groups
    this.getGroups();
    //Load messages
    this.getMessages();

    var users = this.UserService.GetAll();
    if(users){
      users.forEach((item: User[]) => {
        item.forEach(user => {
          this.UserList.push(user);
        })
      });
    }       
  }

  ngAfterContentInit(){
  }

  logout() {
    this.authenticationService.logout();
    this._Route.navigate(['/login']);
  }

  getGroups(){
    this.GroupService.GetAll().forEach(data => {
      data.forEach(item=>{
        var group = new Group();
        group.id = item.id;
        group.name = item.name;
        group.description = item.description;
        group.isMain = item.isMain;
        group.isPrivate = item.isPrivate;

        if(group.isMain){
          this.CurrentGroup = group;
        }

        this.GroupList.push(group);
      })
    });
  }  
  
  selectGroup(id){    
    if(id==null){
      return;
    }

    var group = this.GroupList.find(item=>item.id===id);
    if(group==null){
      return;
    }

    this.CurrentMessage = "";
    this.CurrentGroup = group;    

    this.getMessages();
  }

  getMessages(){
    // this.messages = this.allMessages.filter(item => item.groupId===this.CurrentGroup.id);
  }

  sendMessage(){   
    this.CurrentMessage = "";

    this.getMessages();
  }

  getUserName(id){
      if(this.UserList){
        var user = this.UserList.find(item=>item.id===id);
        if(user){
          return user.displayName;
        }else{
          return "Undefined User";
        }
    }
  }

  search(value){
    console.log(value);
  }

}
