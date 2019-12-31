import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthGuard } from '@app/shared/helpers/authGuard';

//--------------------------------------------------------

import { ChatComponent } from './components/chat/chat.component';

export const ChatRoutes = [
    { path: 'detail', component: ChatComponent, canActivate: [AuthGuard] }
]
