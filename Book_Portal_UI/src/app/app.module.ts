import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Auth/login/login.component';
import { TitleComponent } from './Components/title/title.component';
import { AuthorRegisterComponent } from './Auth/author-register/author-register.component';
import { PublisherRegisterComponent } from './Auth/publisher-register/publisher-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './Services/token.interceptor';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { AuthorsComponent } from './Components/authors/authors.component';
import { EmployeesComponent } from './Components/employees/employees.component';
import { PublishersComponent } from './Components/publishers/publishers.component';
import { OwntitlesComponent } from './Components/owntitles/owntitles.component';
import { StoresComponent } from './Components/stores/stores.component';
import { TitleRequestComponent } from './Components/title-request/title-request.component';
import { TitleUpdateComponent } from './Components/title-update/title-update.component';
import { AuthorProfileComponent } from './Components/author-profile/author-profile.component';
import { PublisherProfileComponent } from './Components/publisher-profile/publisher-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TitleComponent,
    AuthorRegisterComponent,
    PublisherRegisterComponent,
    DashboardComponent,
    ProfileComponent,
    AuthorsComponent,
    EmployeesComponent,
    PublishersComponent,
    OwntitlesComponent,
    StoresComponent,
    TitleRequestComponent,
    TitleUpdateComponent,
    AuthorProfileComponent,
    PublisherProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgToastModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
