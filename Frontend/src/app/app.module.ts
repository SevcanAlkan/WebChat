import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

//Routing
import { AppRoutingModule } from './app-routing.module';

//App Componenets
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent } from './home/home.component';

//Helpers
import { JwtInterceptor } from './helpers/JwtInterceptor';
import { ErrorInterceptor } from './helpers/ErrorInterceptor';

//Services
import { ChatService } from '@services/ChatService';
import { GroupComponent } from './group/group.component';

@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    LoginComponent,
    HomeComponent,
    GroupComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ChatService
  ]
})
export class AppModule { }
