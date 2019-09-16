import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { User, UserListVM } from '@models/User';
import { Group } from '@models/Group';
import { Message } from '@models/message';
import { ChatService } from '@services/ChatService';
import { MessageService } from '@services/MessageService';
import { AuthenticationService } from '@services/AuthenticationService';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  private currentUser: User; 
  private currentGroup: Group = new Group(); 
  private userList: UserListVM[];
  
  messages: Message[] = [];  
  tempMessage: string = " ";
  private message: Message;

  constructor(private messageService: MessageService, private _ngZone: NgZone,
    private chatService: ChatService, private _Route: Router,
    private authenticationService: AuthenticationService) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);             
      this.chatService.CurrentGroup.subscribe(x => {
      this.currentGroup = x;
      this.messages = [];
      this.getMessages();   
    });    
    this.chatService.UserList.subscribe(x => this.userList = x);
    this.subscribeToEvents(); 
   }

  ngOnInit() {      
  }

  ngOnDestroy() {
    this.chatService.messageReceived.unsubscribe();
    this.chatService.CurrentGroup.unsubscribe();
    this.chatService.UserList.unsubscribe();
  }

  getMessages(){
    if((this.currentGroup && this.currentGroup.id)){

      this.messageService.getByGroupId(this.currentGroup.id).forEach(data => {
        data.forEach(item=>{  
          
          if(item.userId === this.currentUser.id){
            item.type = "sent";
          }else{
            item.type = "received";
          } 
          this.messages.push(item);
        })
      });

      console.log(this.messages);
      
      this.tempMessage = this.chatService.getTempMessage();      
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
     this.message.userId = this.currentUser.id; 
     this.message.groupId = this.currentGroup.id;  
     this.message.type = "sent";  
     this.message.text = text;  
     this.message.date = new Date(); 
     
     this.messages.push(this.message);  
     this.chatService.sendMessage(this.message);  

     //Clear temp message
     this.tempMessage = "";  
     this.chatService.clearTempMessage();
   }  
  } 

  updateTempMessage(){
    this.chatService.tempMessage = this.tempMessage;
  }

  private subscribeToEvents(): void {    
      this.chatService.messageReceived.subscribe((message: Message) => {  
        this._ngZone.run(() => {  
          if (message.userId !== this.currentUser.id) {  
            message.type = "received";  
            this.messages.push(message);  
          }  
        });  
      });  
  }

  getUserName(id){
    if(this.userList){
      var user = this.userList.find(item=>item.id===id);
      if(user){
        return user.displayName;
      }else{
        return "Undefined User";
      }
    }
  }

  editGroup(){
    this._Route.navigate(['group', this.currentGroup.id]);
  }
}
