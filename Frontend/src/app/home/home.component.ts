import { Component, OnInit, EventEmitter, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { Message, TempMessage } from '@models/message';
import { Group } from '@models/Group';
import { User, UserListVM } from '@models/User';

import { UserService } from '@services/UserService';
import { GroupService } from '@services/GroupService';
import { AuthenticationService } from '@services/AuthenticationService';
import { ChatService } from '@services/ChatService';
import { MessageService } from '@services/MessageService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit  {  
  private CurrentUser: User; //Current logged in user.
  CurrentGroup: Group = new Group; //Already selected group.

  private GroupList: Group[] = []; 
  private UserList: UserListVM[] = [];  

  messages: Message[] = [];  
  private tempMessages: TempMessage[] = []; //Which messages have written but didn't send.
  tempMessage: string = " "; //Current message text, from input.
  private message: Message; //Message model for send to API.

  NavbarToggle = true;

  constructor(private _Route: Router,
    private authenticationService: AuthenticationService,
     private userService: UserService,
     private groupService :GroupService,
     private chatService: ChatService,
     private messageService: MessageService,
     private _ngZone: NgZone) {
     this.authenticationService.currentUser.subscribe(x => this.CurrentUser = x);   
        this.subscribeToEvents(); 
   }

  ngOnInit() {
    //Load groups
    this.getGroups();
    //Load old messages
    this.getMessages();

    var users = this.userService.getUserList();
    if(users){
      users.forEach((item: UserListVM[]) => {
        item.forEach(user => {
          this.UserList.push(user);
        })
      });
    }     
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
          this.CurrentGroup = group;
        }

        this.GroupList.push(group);
      })
    }).then(()=>{
      this.selectGroup(this.CurrentGroup.id, true);
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
    if(id==null){
      return;
    }else if(this.CurrentGroup && id===this.CurrentGroup.id && !loadAnyWay){
      return;
    }

    var group = this.GroupList.find(item=>item.id===id);
    if(group==null){
      return;
    }  
    
    var _tempMessage = new TempMessage();
    _tempMessage.text = this.tempMessage;
    _tempMessage.groupId = this.CurrentGroup.id;    
    this.tempMessages.push(_tempMessage);
    this.tempMessage=  " ";
    this.messages = [];
    
    this.CurrentGroup = group;        
    this.getMessages();
  }
  getMessages(){
    if((this.CurrentGroup && this.CurrentGroup.id)){

      this.messageService.getByGroupId(this.CurrentGroup.id).forEach(data => {
        data.forEach(item=>{  
          
          if(item.userId === this.CurrentUser.id){
            item.type = "sent";
          }else{
            item.type = "received";
          } 
          this.messages.push(item);
        })
      });

      var _tempMessage = this.tempMessages.find(a=>a.groupId===this.CurrentGroup.id);
      if(_tempMessage && _tempMessage.text){
        this.tempMessage = _tempMessage.text;
      }
    }
  }
  get sortMessages() {
    return this.messages.sort((a, b) => {
      return <any>new Date(a.date) - <any>new Date(b.date);
    });
  }

  sendMessage(message: Message) {  
     if (this.tempMessage) {  
      var text = "";
      if(this.tempMessage.length >= 500){
        text = this.tempMessage.substring(0, 499);
      }else{
        text = this.tempMessage;
      }
      
      this.message = new Message();  
      this.message.userId = this.CurrentUser.id; 
      this.message.groupId = this.CurrentGroup.id;  
      this.message.type = "sent";  
      this.message.text = text;  
      this.message.date = new Date(); 
      
      this.messages.push(this.message);  
      this.chatService.sendMessage(this.message);  

      //Clear temp message
      this.tempMessage = "";  
      let _sentMessage = this.tempMessages.find(a => a.groupId === this.message.groupId); 
      this.tempMessages = this.tempMessages.filter(obj => obj !== _sentMessage);
    }  
  }  
  private subscribeToEvents(): void {    
    this.chatService.messageReceived.subscribe((message: Message) => {  
      this._ngZone.run(() => {  
        if (message.userId !== this.CurrentUser.id) {  
          message.type = "received";  
          this.messages.push(message);  
        }  
      });  
    });  
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
