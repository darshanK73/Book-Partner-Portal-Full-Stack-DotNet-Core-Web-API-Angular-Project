import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  email!:string;
  user!:any;

  constructor(private authService : AuthService,private jwtService:JwtService){ }

  ngOnInit() {
    this.jwtService.getUser().subscribe(val => {
      let u = this.authService.getUserFromToken();
      this.user = JSON.parse(u) || val;
    });
    this.jwtService.getEmail().subscribe(val => {
      let em = this.authService.getEmailFromToken();
      this.email = val || em;
    });
  }

  

  

}
