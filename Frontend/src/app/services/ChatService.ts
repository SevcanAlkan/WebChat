import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@aspnet/signalr';  
import { Message } from '@models/message';  
import { environment } from '@environments/environment';
import { Observable, Subject } from 'rxjs';
import { promise } from 'protractor';
  
@Injectable()  
export class ChatService {  
  MessageReceived = new EventEmitter<Message>();  
  ConnectionEstablished = new EventEmitter<Boolean>();  
  HubIsReady = new Subject<boolean>();
  
  private groupId = "";
  private userId = "";
  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  
  constructor() {  
  }  
  
  OpenConnection(userId){
    this.userId = userId;

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
  }
  CloseConnection(){

  }

  IsConnected() : boolean{
    return this._hubConnection && this._hubConnection.state == HubConnectionState.Connected;
  }

  UpdateGroupId(_groupId=""){
    if( _groupId == null || _groupId == ""){
      return;
    }

    if(this.groupId != _groupId){    
      if(this.groupId != null && this.groupId != ""){
        this.removeFromGroup();       
      }

      this.groupId = _groupId;
      this.addToGroup();    
    }
  }

  private addToGroup() { 
    this._hubConnection.invoke('AddToGroup', this.groupId);  
  }  

  private removeFromGroup() {  
    this._hubConnection.invoke('RemoveFromGroup', this.groupId);    
  } 

  SendMessage(message: Message) {  
    this._hubConnection.invoke('SendMessage', message);  
  }  

  SendPrivateMessage(message: Message, userId:string) {  
    this._hubConnection.invoke('SendPrivateMessage', message, userId);  
  }  
  
  private createConnection() {  
    this._hubConnection = new HubConnectionBuilder()
    .configureLogging(LogLevel.Information)
    .withUrl(environment.apiUrl + "/chatHub?UserId=" + this.userId)
    .build();
  }  
  
  private startConnection(): void {  
    this._hubConnection  
      .start()  
      .then(() => {          
        this.connectionIsEstablished = true;   
        this.connectionEstablished.emit(true);      
        this.hubIsReady.next(true);
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