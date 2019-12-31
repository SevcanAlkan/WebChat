import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthGuard } from '@app/shared/helpers/authGuard';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'group',component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'group/:id',component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'profile',component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/:id',component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'register/:id', component: RegisterComponent, canActivate: [AuthGuard] },
  { path: 'search/:key', component: SearchComponent, canActivate: [AuthGuard] },

  //Chat Module
  { path: 'chat', loadChildren: '.chat/chat.module#ChatModule' },

  // otherwise redirect to home
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    CommonModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
