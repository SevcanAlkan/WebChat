import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@aspnet/signalr';  
import { Message } from '@models/message';  
import { environment } from '@environments/environment';
  
@Injectable()  
export class ChatService {  
  messageReceived = new EventEmitter<Message>();  
  connectionEstablished = new EventEmitter<Boolean>();  
  
  private groupId = " ";
  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  
  constructor() {     
  }  
  
  updateGroupId(_groupId=""){
    this.groupId = _groupId;
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