import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorRegisterComponent } from './Auth/author-register/author-register.component';
import { LoginComponent } from './Auth/login/login.component';
import { PublisherRegisterComponent } from './Auth/publisher-register/publisher-register.component';
import { TitleComponent } from './Components/title/title.component';
import { AuthGuard } from './Services/auth.guard';
import { LogGuard } from './Services/log.guard';
import { OwntitlesComponent } from './Components/owntitles/owntitles.component';

import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { AuthorsComponent } from './Components/authors/authors.component';
import { EmployeesComponent } from './Components/employees/employees.component';
import { PublishersComponent } from './Components/publishers/publishers.component';
import { StoresComponent } from './Components/stores/stores.component';
import { TitleRequestComponent } from './Components/title-request/title-request.component';
import { TitleUpdateComponent } from './Components/title-update/title-update.component';
import { AuthorProfileComponent } from './Components/author-profile/author-profile.component';
import { PublisherProfileComponent } from './Components/publisher-profile/publisher-profile.component';

const routes: Routes = [
  {path:"",component:DashboardComponent,canActivate:[AuthGuard]},
  {path:"login",component:LoginComponent,canActivate:[LogGuard]},
  {path:"author-register",component:AuthorRegisterComponent,canActivate:[LogGuard]},
  {path:"publisher-register",component:PublisherRegisterComponent,canActivate:[LogGuard]},
  { path: "dashboard", component: DashboardComponent,children: 
  [
    {path:"titles",component:TitleComponent},
    {path:"authors",component:AuthorsComponent},
    {path:"employees",component:EmployeesComponent},
    {path:"profile",component:ProfileComponent},
    {path: "owntitles", component:OwntitlesComponent},
    {path: "publishers", component:PublishersComponent},
    {path: "stores", component:StoresComponent},
    {path: "title-request", component:TitleRequestComponent},
    {path: "title-update/:titleId", component:TitleUpdateComponent},
    {path: "author-profile", component:AuthorProfileComponent},
    {path: "publisher-profile", component:PublisherProfileComponent}
  ], canActivate: [AuthGuard] 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
