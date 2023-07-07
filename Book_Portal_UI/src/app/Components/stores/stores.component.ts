import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from 'src/app/Models/stores';
import { Title } from 'src/app/Models/title';
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.css']
})
export class StoresComponent implements OnInit {

  stores: Store[] = [];
  titles: Title[] = [];
  titleIds:string[] = [];
  authorId!:string;

  constructor(private authorService: AuthorService, private jwtService: JwtService, private authService: AuthService, private router: Router) { }
    ngOnInit(): void {

      this.jwtService.getUserId().subscribe(val => {
        let u = this.authService.getUserIdFromToken();
        console.log(u);
        // console.log(u);
        this.authorId = u || val;
  
        this.authorService.getAllOwnTitles(this.authorId).subscribe({next:(res)=> {
          this.titles = res;
          for(let t of this.titles){
            this.titleIds.push(t.titleId)
          }

          console.log(this.titleIds);
          console.log(this.titles);

          this.authorService.getAllStoresSellingTitle(this.titleIds).subscribe({
            next: (res) => {
              this.stores = res;
              console.log(this.stores);
            }, error: (err) => {
              console.log(err);
            }
          });
          
        },error:(err)=>{
          console.log(err);
        }});
  
      });

    
  }
}
