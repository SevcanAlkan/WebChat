import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

//Routing
import { AppRoutingModule } from './app-routing.module';

//App Componenets
import { AppComponent } from './app.component';
import { LoginComponent } from './component/login/login.component';
import { ProfileComponent } from './component/profile/profile.component';
import { HomeComponent } from './component/home/home.component';
import { SearchComponent } from './component/search/search.component';
import { GroupComponent } from './component/group/group.component';
import { RegisterComponent } from './component/register/register.component';

//Helpers
import { JwtInterceptor } from './helpers/jwtInterceptor';
import { ErrorInterceptor } from './helpers/errorInterceptor';

//Services
import { ChatService } from '@services/ChatService';
import { SearchService } from '@services/SearchService';
import { DataTrackingService } from './services/DataTrackingService';


@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    LoginComponent,
    HomeComponent,
    GroupComponent,
    RegisterComponent,
    SearchComponent
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
    ChatService, SearchService, DataTrackingService
  ]
})
export class AppModule { }
