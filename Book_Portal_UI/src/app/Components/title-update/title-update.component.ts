import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorId } from 'src/app/Models/author-id';
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
  originalTitle!:any;
  titleId!:string;
  authorIds:AuthorId[] = [];

  constructor(private autherService: AuthorService,private fb:FormBuilder, private router: Router,private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.titleId = this.route.snapshot.params['titleId'];
    
    this.autherService.getTitleFromId(this.titleId).subscribe({next:(res)=>{
      this.originalTitle = res;
      console.log(this.originalTitle);

    },error:(err)=> {
        console.log(err.errror.message);
    }});

    this.autherService.getAllAuthorsIds().subscribe({next:(res)=>{
      this.authorIds = res;
      console.log(this.authorIds);
    },error:(err)=>{
      console.log(err.error.message);
    }})


    this.titleUpdateForm = this.fb.group({
      titleId:[,Validators.required],
      pubId:['',Validators.required],
      title1:['',Validators.required],
      price:['',Validators.required],
      advance:['',Validators.required],
      pubdate:[null,Validators.required],
      royalty:['',Validators.required],
      ytdSales:['',Validators.required],
      type:['',Validators.required],
      royaltyper:['',Validators.required],
      auOrd:['',Validators.required],
      auIds:[[],Validators.required],
      notes:['',Validators.required]
    });
  }

  onSubmit(){
    alert("hello");
    if(this.titleUpdateForm.value.valid)
    {
      console.log(this.titleUpdateForm)
    }
    else
    {
      console.log(this.titleUpdateForm);
    }
  }
}