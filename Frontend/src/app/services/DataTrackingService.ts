import { EventEmitter, Injectable } from '@angular/core';  
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@aspnet/signalr';  
import { environment } from '@environments/environment';
import { Observable, Subject } from 'rxjs';
  
@Injectable()  
export class DataTrackingService {  
  GroupUpdateReceived: EventEmitter<string>;  
  ConnectionEstablished: EventEmitter<Boolean>;  
  HubIsReady: Subject<boolean>;
  
  private connectionIsEstablished: boolean;  
  private _hubConnection: HubConnection;  
  
  constructor() { 
    this.GroupUpdateReceived = new EventEmitter<string>();
    this.ConnectionEstablished = new EventEmitter<Boolean>();
    this.HubIsReady = new Subject<boolean>();
    this.connectionIsEstablished = false;    
  }  
  
  OpenConnection() : void {
    if(this._hubConnection && this._hubConnection.state == HubConnectionState.Connected){
      this._hubConnection.stop().then(() => {
      this.createConnection();  
      this.registerOnGroupUpdateEvents();  
      this.startConnection();  
      });
    }else{
      this.createConnection();  
      this.registerOnGroupUpdateEvents();  
      this.startConnection();  
    }
  }
  CloseConnection() : void {
    this._hubConnection.stop();
  }

  IsConnected() : boolean{
    return this._hubConnection && this._hubConnection.state == HubConnectionState.Connected;
  }

  GroupUpdated(groupId: string) : void {  
    this._hubConnection.invoke('UpdateGroupOnAllClients', groupId);  
  }  

  private registerOnGroupUpdateEvents() : void {  
    this._hubConnection.on('ReceiveGroupUpdateMessages', (data: string) => {  
        this.GroupUpdateReceived.emit(data);  
    });  
  } 

  private createConnection() : void {  
    this._hubConnection = new HubConnectionBuilder()
    .configureLogging(LogLevel.Information)
    .withUrl(environment.apiUrl + "/dataHub")
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
}    