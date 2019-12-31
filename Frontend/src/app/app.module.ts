import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

//Routing
import { AppRoutingModule } from './app-routing.module';

//App Componenets
import { AppComponent } from './app.component';

//Helpers
import { JwtInterceptor } from './shared/helpers/jwtInterceptor';
import { ErrorInterceptor } from './shared/helpers/errorInterceptor';

//--------------------------------------------------------------------MODULES

import { 
  //Components
  ChatComponent,
  //Services
  ChatService, GroupService, MessageService
} from './modules/chat/index'

//---------------------------------------------------------------------------


@NgModule({
  declarations: [
    AppComponent,
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
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ]
})
export class AppModule { }
