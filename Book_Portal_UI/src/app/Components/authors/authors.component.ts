import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/Models/author';
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent {

  authors:Author[] = [];

  constructor(private authorService:AuthorService,private jwtService:JwtService,private authService:AuthService,private router:Router){}
  
  ngOnInit(): void {
    this.authorService.getAllAuthors().subscribe({next:(res)=> {
      this.authors = res;
      console.log(this.authors);
    },error:(err)=>{
      console.log(err);
    }});
  }

}
