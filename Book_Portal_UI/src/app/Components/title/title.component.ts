import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from 'src/app/Models/title';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';
import { AuthorService } from 'src/app/Services/author.service';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.css']
})
export class TitleComponent implements OnInit{
  
  titles:Title[] = [];

  constructor(private titleService:AuthorService,private jwtService:JwtService,private authService:AuthService,private router:Router){}
  
  ngOnInit(): void {
    this.titleService.getAllTitles().subscribe({next:(res)=> {
      this.titles = res;
      console.log(this.titles);
    },error:(err)=>{
      console.log(err);
    }});
  }

}
