import { Component } from '@angular/core';
import * as signalR from "@aspnet/signalr";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private _hubConnection: signalR.HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];

  public sendMessage(): void {
    this._hubConnection
      .invoke('SendMessage', this.nick, this.message)
      .catch(err => console.error(err));
  }

  ngOnInit() {
    this.nick = window.prompt('Your name:', 'John');

    this._hubConnection = new signalR.HubConnectionBuilder().withUrl('http://localhost:5008/chathub', {skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets}).build();

    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
    
      this._hubConnection.on('ReceiveMessage', (nick: string, receivedMessage: string) => {
        const text = `${nick}: ${receivedMessage}`;
        this.messages.push(text);
      }); 
    }
}
