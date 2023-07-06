import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from 'src/app/Models/stores';
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

  constructor(private storeService: AuthorService, private jwtService: JwtService, private authService: AuthService, private router: Router) { }
    ngOnInit(): void {
    this.storeService.getAllStores().subscribe({
      next: (res) => {
        this.stores = res;
        console.log(this.stores);
      }, error: (err) => {
        console.log(err);
      }
    });
  }
}
