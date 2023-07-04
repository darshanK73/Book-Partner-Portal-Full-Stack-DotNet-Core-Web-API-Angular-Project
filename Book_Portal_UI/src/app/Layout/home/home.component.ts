import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';
import { TitleService } from 'src/app/Services/title.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  Titles:any;
  constructor(private titleService:TitleService,private jwtService:JwtService,private authService:AuthService){}
  ngOnInit(): void {
    this.titleService.getAllTitles().subscribe({next:(res)=> {
      this.Titles = res;
    },error:(err)=>{
      console.log(err);
    }});
  }
}
