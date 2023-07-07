import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorId } from 'src/app/Models/author-id';
import { TitleRequest } from 'src/app/Models/title-request';
import { TitleResponse } from 'src/app/Models/title-response';
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-title-update',
  templateUrl: './title-update.component.html',
  styleUrls: ['./title-update.component.css']
})
export class TitleUpdateComponent implements OnInit {

  titleUpdateForm!: FormGroup;
  originalTitle: TitleResponse = new TitleResponse();
  titleId!: string;
  authorIds: AuthorId[] = [];

  constructor(private autherService: AuthorService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
   
  }
  ngOnInit(): void {

    this.titleId = this.route.snapshot.params['titleId'];

    this.autherService.getTitleFromId(this.titleId).subscribe({
      next: (res) => {
        console.log(res);
        this.originalTitle = res;
        console.log(this.originalTitle);

        this.titleUpdateForm.setValue({
          titleId:this.titleId,
          pubId:res.pubId,
          title1:res.title1,
          price:res.price,
          advance:res.advance,
          pubdate:formatDate(res.pubdate, 'yyyy-MM-dd', 'en'),
          royalty: res.royalty,
          ytdSales: res.ytdSales,
          notes : res.notes,
          type : res.type,
          auIds : res.auIds,
          auOrd : res.auOrd[0],
        royaltyper :res.royaltyper[0]

        });

      }, error: (err) => {
        console.log(err.errror.message);
      }
    });

    this.autherService.getAllAuthorsIds().subscribe({
      next: (res) => {
        this.authorIds = res;
        console.log(this.authorIds);
      }, error: (err) => {
        console.log(err.error.message);
      }
    })

    this.titleUpdateForm = this.fb.group({
      titleId: ['', Validators.required],
      pubId: ['', Validators.required],
      title1: ['', Validators.required],
      price: ['', Validators.required],
      advance: ['', Validators.required],
      pubdate: ['', Validators.required],
      royalty: ['', Validators.required],
      ytdSales: ['', Validators.required],
      type: ['', Validators.required],
      royaltyper: ['', Validators.required],
      auOrd: ['', Validators.required],
      auIds: ['', Validators.required],
      notes: ['', Validators.required]
    });
  }

  onSubmit() {
    alert("hello");
    console.log(this.titleUpdateForm);

    let titleRequest = new TitleRequest();
    titleRequest.title1 = this.titleUpdateForm.value.title1;
    titleRequest.price = this.titleUpdateForm.value.price
    titleRequest.advance = this.titleUpdateForm.value.advance
    titleRequest.royalty = this.titleUpdateForm.value.royalty
    titleRequest.ytdSales = this.titleUpdateForm.value.ytdSales
    titleRequest.type = this.titleUpdateForm.value.type
    titleRequest.royaltyper = this.titleUpdateForm.value.royaltyper
    titleRequest.notes = this.titleUpdateForm.value.notes

  
     this.autherService.updateTitleDetails(titleRequest,this.titleId).subscribe({next:(res)=>{
      console.log(res);
     },error:(err)=>{
      console.log(err);
     }})
    
  }
}