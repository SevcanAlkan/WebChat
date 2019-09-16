import { EventEmitter, Injectable } from '@angular/core';  
import { Subject } from 'rxjs';
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@aspnet/signalr';  
import { Message, TempMessage } from '@models/message';  
import { environment } from '@environments/environment';
import { UserListVM, User } from '@models/User';
import { Group } from '@models/Group';
  
@Injectable()  
export class ChatService {  
  messageReceived = new EventEmitter<Message>();  
  connectionEstablished = new EventEmitter<Boolean>();  
  
  CurrentUser = new Subject<User>(); 
  CurrentGroup = new Subject<Group>();
  UserList = new Subject<UserListVM[]>(); 
  tempMessage: string = "";

  private groupId;  
  private tempMessages:TempMessage[] = [];
  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  
  constructor() { 
    this.CurrentGroup.subscribe(x => {
      if(this._hubConnection && this._hubConnection.state == HubConnectionState.Connected){
      this._hubConnection.stop().then(() => {
        this.createConnection();  
        this.registerOnServerEvents();  
        this.startConnection();  
      });
      }else{
          this.createConnection();  
          this.registerOnServerEvents();  
          this.startConnection();  
      }

      this.groupId = x.id;
    });    
  }  

  saveTempMessage(){
    if(this.groupId && this.tempMessage != ""){
      var _tempMessage = this.tempMessage;
      this.clearTempMessage();
      var _tempMessageVM = new TempMessage();
      _tempMessageVM.text = _tempMessage;
      _tempMessageVM.groupId = this.groupId;    
      this.tempMessages.push(_tempMessageVM); 
    }      
  }

  getTempMessage(){
    if(this.groupId){   
      var _tempMessage = this.tempMessages.find(a=> a.groupId == this.groupId);
      if(_tempMessage != null && _tempMessage.text !== 'undefined'){
        this.tempMessage = _tempMessage.text;
        return _tempMessage.text;
      }else{
        this.tempMessage = "";
        return "";
      }
    }
  }

  clearTempMessage(){
    this.tempMessage = "";
    let _sentMessage = this.tempMessages.find(a => a.groupId == this.groupId);
    if(_sentMessage) {
      this.tempMessages = this.tempMessages.filter(obj => obj !== _sentMessage);
    }
  }

  sendMessage(message: Message) {  
    this._hubConnection.invoke('SendMessage', message);  
  }  
  
  private createConnection() {  
    this._hubConnection = new HubConnectionBuilder()
    .configureLogging(LogLevel.Information)
    .withUrl(environment.apiUrl + "/chatHub?GroupId=" + this.groupId)
    .build();
  }  
  
  private startConnection(): void {  
    this._hubConnection  
      .start()  
      .then(() => {  
        this.connectionIsEstablished = true;  
        console.log('Hub connection started');  
        this.connectionEstablished.emit(true);  
      })  
      .catch(err => {  
        console.log('Error while establishing connection, retrying...' + err.toString());  
        setTimeout(function () { this.startConnection(); }, 5000);  
      });  
  }  
  
  private registerOnServerEvents(): void {  
    this._hubConnection.on('ReceiveMessage', (data: any) => {  
        this.messageReceived.emit(data);  
    });  
  }  
}    