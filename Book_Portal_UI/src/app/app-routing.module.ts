import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorRegisterComponent } from './Auth/author-register/author-register.component';
import { LoginComponent } from './Auth/login/login.component';
import { PublisherRegisterComponent } from './Auth/publisher-register/publisher-register.component';
import { TitleComponent } from './Components/title/title.component';
import { HomeComponent } from './Layout/home/home.component';

const routes: Routes = [
  {path:"",component:LoginComponent},
  {path:"login",component:LoginComponent},
  {path:"author-register",component:AuthorRegisterComponent},
  {path:"publisher-register",component:PublisherRegisterComponent},
  {path:"home",component:HomeComponent,children: 
  [
    {path:"title",component:TitleComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
