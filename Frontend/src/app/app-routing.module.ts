import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from '@helper/AuthGuard';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GroupComponent } from './group/group.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'group',component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'group/:id',component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'profile',component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/:id',component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'register/:id', component: RegisterComponent },

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
