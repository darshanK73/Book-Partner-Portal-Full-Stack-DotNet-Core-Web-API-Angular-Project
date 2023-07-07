import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
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
  // originalTitle: TitleResponse = new TitleResponse();
  // titleId!: string;
  authorIds: AuthorId[] = [];
  currentAuthorId!:string;

  constructor(private autherService: AuthorService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute,private toast:NgToastService) {
   
  }
  ngOnInit(): void {

    // this.titleId = this.route.snapshot.params['titleId'];

    // this.autherService.getTitleFromId(this.titleId).subscribe({
    //   next: (res) => {
    //     console.log(res);
    //     this.originalTitle = res;
    //     console.log(this.originalTitle);

        

    //   }, error: (err) => {
    //     console.log(err.errror.message);
    //   }
    // });


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
      auIds: [[], Validators.required],
      notes: ['', Validators.required]
    });
  }

  onSubmit() {
    let titleRequest = new TitleRequest();
    titleRequest.title1 = this.titlePublishForm.value.title1;
    titleRequest.pubId = this.titlePublishForm.value.pubId;
    titleRequest.price = this.titlePublishForm.value.price;
    titleRequest.advance = this.titlePublishForm.value.advance;
    titleRequest.royalty = this.titlePublishForm.value.royalty;
    titleRequest.ytdSales = this.titlePublishForm.value.ytdSales;
    titleRequest.type = this.titlePublishForm.value.type;
    titleRequest.pubdate = this.titlePublishForm.value.pubdate;
    titleRequest.auIds = this.titlePublishForm.value.auIds;
    titleRequest.royaltyper = this.titlePublishForm.value.royaltyper;
    titleRequest.notes = this.titlePublishForm.value.notes;
    titleRequest.auOrd = this.titlePublishForm.value.auOrd;

  
     this.autherService.postPublishTitles(titleRequest).subscribe({next:(res)=>{
        this.titlePublishForm.reset();
        this.toast.success({detail:'Success',summary:res.message,duration:5000})
     },error:(err)=>{
        this.toast.error({detail:'Error',summary:err.error.message,duration:5000})
     }})
    
  }


}
