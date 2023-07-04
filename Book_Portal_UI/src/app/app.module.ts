import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Auth/login/login.component';
import { HeaderComponent } from './Layout/header/header.component';
import { FooterComponent } from './Layout/footer/footer.component';
import { SidebarComponent } from './Layout/sidebar/sidebar.component';
import { HomeComponent } from './Layout/home/home.component';
import { TitleComponent } from './Components/title/title.component';
import { AuthorRegisterComponent } from './Auth/author-register/author-register.component';
import { PublisherRegisterComponent } from './Auth/publisher-register/publisher-register.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    HomeComponent,
    TitleComponent,
    AuthorRegisterComponent,
    PublisherRegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
