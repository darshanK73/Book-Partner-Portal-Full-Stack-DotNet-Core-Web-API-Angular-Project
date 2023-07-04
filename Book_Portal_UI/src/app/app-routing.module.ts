import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorRegisterComponent } from './Auth/author-register/author-register.component';
import { LoginComponent } from './Auth/login/login.component';
import { PublisherRegisterComponent } from './Auth/publisher-register/publisher-register.component';
import { TitleComponent } from './Components/title/title.component';
import { HomeComponent } from './Layout/home/home.component';
import { AuthGuard } from './Services/auth.guard';
import { LogGuard } from './Services/log.guard';

const routes: Routes = [
  {path:"",component:LoginComponent},
  {path:"login",component:LoginComponent,canActivate:[LogGuard]},
  {path:"author-register",component:AuthorRegisterComponent,canActivate:[LogGuard]},
  {path:"publisher-register",component:PublisherRegisterComponent,canActivate:[LogGuard]},
  {path:"home",component:HomeComponent,children: 
  [
    {path:"title",component:TitleComponent}
  ],canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
