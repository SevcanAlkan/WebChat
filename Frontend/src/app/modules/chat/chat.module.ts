import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChatRoutes } from './chat-routing-module';

import { 
    //Components
    ChatComponent,
    //Services
    ChatService, GroupService, MessageService
  } from './index';

@NgModule({
    declarations: [
        ChatComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(ChatRoutes)
    ],
    bootstrap: [],
    providers: [
        ChatService,
        GroupService,
        MessageService
    ]
})
export class ChatModule { }
    