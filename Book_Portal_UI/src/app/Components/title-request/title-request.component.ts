import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorId } from 'src/app/Models/author-id';
import { TitleRequest } from 'src/app/Models/title-request';
import { TitleResponse } from 'src/app/Models/title-response';
import { AuthService } from 'src/app/Services/auth.service';
import { AuthorService } from 'src/app/Services/author.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-title-request',
  templateUrl: './title-request.component.html',
  styleUrls: ['./title-request.component.css']
})
export class TitleRequestComponent implements OnInit {

  titlePublishForm!: FormGroup;
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

        this.titlePublishForm.setValue({
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

    this.titlePublishForm = this.fb.group({
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
    console.log(this.titlePublishForm);

    let titleRequest = new TitleRequest();
    titleRequest.title1 = this.titlePublishForm.value.title1;
    titleRequest.price = this.titlePublishForm.value.price
    titleRequest.advance = this.titlePublishForm.value.advance
    titleRequest.royalty = this.titlePublishForm.value.royalty
    titleRequest.ytdSales = this.titlePublishForm.value.ytdSales
    titleRequest.type = this.titlePublishForm.value.type
    titleRequest.royaltyper = this.titlePublishForm.value.royaltyper
    titleRequest.notes = this.titlePublishForm.value.notes

  
     this.autherService.updateTitleDetails(titleRequest,this.titleId).subscribe({next:(res)=>{
      console.log(res);
     },error:(err)=>{
      console.log(err);
     }})
    
  }


}
