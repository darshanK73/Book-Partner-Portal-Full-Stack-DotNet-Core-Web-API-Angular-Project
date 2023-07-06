import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from 'src/app/Models/title'
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-owntitles',
  templateUrl: './owntitles.component.html',
  styleUrls: ['./owntitles.component.css']
})
export class OwntitlesComponent implements OnInit{

  titles: Title[] = []
  constructor(private titleService:AuthorService,private jwtService:JwtService,private authService:AuthService,private router:Router,private route:ActivatedRoute){}

  ngOnInit(): void {
    this.titleService.getAllTitles().subscribe({next:(res)=> {
      this.titles = res;
      console.log(this.titles);
    },error:(err)=>{
      console.log(err);
    }});
  }

  updateTitle(title:Title)
  {
    this.router.navigate(['/dashboard/title-update',title.titleId])
  }
}
