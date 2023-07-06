import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-title-request',
  templateUrl: './title-request.component.html',
  styleUrls: ['./title-request.component.css']
})
export class TitleRequestComponent implements OnInit {

  titlePublishForm!: any;

  constructor(private titleService: AuthorService, private jwtService: JwtService, private authService: AuthService, private router: Router) { }
  ngOnInit(): void {

  }


}
