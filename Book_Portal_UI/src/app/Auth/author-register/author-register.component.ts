import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthorRegisterRequest } from 'src/app/Models/author-register-request';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-author-register',
  templateUrl: './author-register.component.html',
  styleUrls: ['./author-register.component.css']
})
export class AuthorRegisterComponent implements OnInit{

  registerForm!:FormGroup;

  constructor(private fb:FormBuilder, private authService:AuthService,private router:Router,private toast:NgToastService) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName:['',Validators.required],
      lastName:['',Validators.required],
      username:['',Validators.required],
      password:['',Validators.required],
      password1:['',Validators.required],
      city:['',Validators.required],
      state:['',Validators.required],
      zip:['',Validators.required],
      address:['',Validators.required],
      email:['',Validators.required],
      phone:['',Validators.required]
    });
  }

  onSubmit(){
    if(this.registerForm.valid){
      var obj = this.registerForm.value;
      let request = new AuthorRegisterRequest();
      request.username = obj.username;
      request.password = obj.password;
      request.address = obj.address;
      request.auFname = obj.firstName;
      request.auLname = obj.lastName;
      request.city = obj.city;
      request.state = obj.state;
      request.email = obj.email;
      request.phone = obj.phone;
      request.zip = obj.zip;
      request.address = obj.address;

     
    }
    else{
      this.validateFormField(this.registerForm);
    }
  }

  private validateFormField(loginForm:FormGroup){
    Object.keys(loginForm.controls).forEach(field => {
      const control = loginForm.get(field);
      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true})
      }
      else if(control instanceof FormGroup){
        this.validateFormField(control);
      }
    })
  }
}
