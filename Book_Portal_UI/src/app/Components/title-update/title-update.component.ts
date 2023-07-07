import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthorId } from 'src/app/Models/author-id';
import { TitleUpdateRequest } from 'src/app/Models/title-update-request';
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
  flagValue = true;

  constructor(private autherService: AuthorService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute,private toast:NgToastService) {
   
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
      titleId: new FormControl({value:'',disabled:true},Validators.required),
      pubId: new FormControl({value:'',disabled:true},Validators.required),
      title1: new FormControl({value:'',disabled:false},Validators.required),
      price: new FormControl({value:'',disabled:false},Validators.required),
      advance: new FormControl({value:'',disabled:false},Validators.required),
      pubdate: new FormControl({value:'',disabled:true},Validators.required),
      royalty: new FormControl({value:'',disabled:false},Validators.required),
      ytdSales: new FormControl({value:'',disabled:false},Validators.required),
      type: new FormControl({value:'',disabled:false},Validators.required),
      royaltyper: new FormControl({value:'',disabled:false},Validators.required),
      auOrd: new FormControl({value:'',disabled:true},Validators.required),
      auIds: new FormControl({value:'',disabled:true},Validators.required),
      notes: new FormControl({value:'',disabled:false},Validators.required),
    });
  }

  onSubmit() {
    let titleRequest = new TitleUpdateRequest();
    titleRequest.title1 = this.titleUpdateForm.value.title1;
    titleRequest.price = this.titleUpdateForm.value.price
    titleRequest.advance = this.titleUpdateForm.value.advance
    titleRequest.royalty = this.titleUpdateForm.value.royalty
    titleRequest.ytdSales = this.titleUpdateForm.value.ytdSales
    titleRequest.type = this.titleUpdateForm.value.type
    titleRequest.royaltyper = this.titleUpdateForm.value.royaltyper
    titleRequest.notes = this.titleUpdateForm.value.notes

  
     this.autherService.updateTitleDetails(titleRequest,this.titleId).subscribe({next:(res)=>{
      this.toast.success({detail:'Success',summary:res.message,duration:5000});
     },error:(err)=>{
      this.toast.error({detail:'Error',summary:err.error.message,duration:5000});
     }})
    
  }
}