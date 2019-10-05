import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ProfileComponent } from './component/profile/profile.component';
import { LoginComponent } from './component/login/login.component';
import { AuthGuard } from '@app/helpers/authGuard';
import { AppComponent } from './app.component';
import { HomeComponent } from './component/home/home.component';
import { GroupComponent } from './component/group/group.component';
import { RegisterComponent } from './component/register/register.component';
import { SearchComponent } from './component/search/search.component';

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
