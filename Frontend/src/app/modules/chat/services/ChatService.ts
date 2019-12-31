import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@aspnet/signalr';  
import { MessageVM } from '@app/modules/chat/models/message';  
import { environment } from '@environments/environment';
import { Observable, Subject } from 'rxjs';
  
@Injectable()  
export class ChatService {  
  MessageReceived: EventEmitter<MessageVM>;  
  ConnectionEstablished: EventEmitter<Boolean>;  
  HubIsReady: Subject<boolean>;
  
  private groupId: string;
  private userId: string;
  private connectionIsEstablished: boolean;  
  private _hubConnection: HubConnection;  
  
  constructor() { 
    this.MessageReceived = new EventEmitter<MessageVM>();
    this.ConnectionEstablished = new EventEmitter<Boolean>();
    this.HubIsReady = new Subject<boolean>();
    this.groupId = "";
    this.userId = "";
    this.connectionIsEstablished = false;    
  }  
  
  OpenConnection(userId: string = "") : void {
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
  CloseConnection() : void {

  }

  IsConnected() : boolean{
    return this._hubConnection && this._hubConnection.state == HubConnectionState.Connected;
  }

  UpdateGroupId(_groupId: string = "") : void {
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

  private addToGroup() : void { 
    this._hubConnection.invoke('AddToGroup', this.groupId);  
  }  

  private removeFromGroup() : void {  
    this._hubConnection.invoke('RemoveFromGroup', this.groupId);    
  } 

  SendMessage(message: MessageVM) : void {  
    this._hubConnection.invoke('SendMessage', message);  
  }  

  SendPrivateMessage(message: MessageVM, userId:string) : void {  
    this._hubConnection.invoke('SendPrivateMessage', message, userId);  
  }  
  
  private createConnection() : void {  
    this._hubConnection = new HubConnectionBuilder()
    .configureLogging(LogLevel.Information)
    .withUrl(environment.apiUrl + "/chatHub?UserId=" + this.userId)
    .build();
  }  
  
  private startConnection() : void {  
    this._hubConnection  
      .start()  
      .then(() => {          
        this.connectionIsEstablished = true;   
        this.ConnectionEstablished.emit(true);      
        this.HubIsReady.next(true);
      })  
      .catch(err => {  
        console.log('Error while establishing connection, retrying...' + err.toString());  
        setTimeout(function () { this.startConnection(); }, 5000);  
      });  
  }  
  
  private registerOnServerEvents(): void {  
    this._hubConnection.on('ReceiveMessage', (data: any) => {  
        this.MessageReceived.emit(data);  
    });  
  }  
}    