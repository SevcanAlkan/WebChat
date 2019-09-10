import { Component } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';

import { Group } from './Models/group';
import { User } from './Models/user';
import { Message } from './Models/message';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  // private hubConnection: HubConnection;
  // nick = '';
  // message = '';
  // messages: string[] = [];

  // ngOnInit() {
  //   this.nick = window.prompt('Your name:', 'John');
  //   this.hubConnection = new HubConnection('http://localhost:5000/chat');

  //   this.hubConnection
  //     .start()
  //     .then(() => console.log('Connection started!'))
  //     .catch(err => console.log('Error while establishing connection :('));
  //   }
  
  groups = [
    new Group(1,"Main","",true,false),
    new Group(2, "Chat","",false,false),
    new Group(3, "PChat","",false,true)    
  ];

  users = [
    new User(1,"sevcan", 2),
    new User(2,"Mert", 2),
    new User(3,"Mark", 2)
  ];

  messages;
  allMessages = [
    new Message("Hello",1,1),
    new Message("Hello mate!",1,2),
    new Message("New room ha?",2,1),
    new Message("Whatsapp?",1,1),
    new Message("Hello",1,3)
  ];

  CurrentUser;
  CurrentGroup;
  CurrentMessage = "";
 
  
  getGroups(){
    return this.groups; //.filter(item=> !item.isPrivate);
  }

  selectGroup(id){
    if(id==null){
      return;
    }

    var group = this.groups.find(item=>item.id===id);
    if(group==null)
    {
      return;
    }

    this.CurrentMessage = "";
    this.CurrentGroup.id=group.id;    

    this.getMessages();
  }

  getMessages(){
    this.messages = this.allMessages.filter(item => item.groupId===this.CurrentGroup.id);
  }

  sendMessage(){
    this.allMessages.push(new Message(this.CurrentMessage,this.CurrentGroup.id, this.CurrentUser.id));
    this.CurrentMessage = "";

    this.getMessages();
  }

  getUserName(id){
    return this.users.find(item=>item.id===id).username;
  }

  search(value){
    console.log(value);
  }

  ngOnInit() {
    this.CurrentUser = this.users.find(item=>item.id===1);  
    this.CurrentGroup = JSON.parse(JSON.stringify(this.groups.find(item=>item.id===1))); 
    
    this.getMessages();
  }

  
}
