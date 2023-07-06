import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';
import { AuthorService } from 'src/app/Services/author.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  role!:string;
  user!:any;
  email!:string;

  constructor(private jwtService:JwtService,private authService:AuthService){}
  ngOnInit(): void {
    this.jwtService.getUser().subscribe(val => {
      let u = this.authService.getUserFromToken();
      this.user = u || val;
    });

    this.jwtService.getRole().subscribe(val => {
      let role = this.authService.getRoleFromToken();
      this.role = role || val;

    });

    this.jwtService.getEmail().subscribe(val => {
      let em = this.authService.getEmailFromToken();
      this.email = val || em;

    });
  }

  isLoggedInAuthor()
  {
    if(this.role == "author")
    {
      return true;
    }
    return false;
  }

  isLoggedInPublisher()
  {
    if(this.role == "publisher")
    {
      return true;
    }
    return false;
  }

  logout(){
    this.authService.logout();
  }

}
