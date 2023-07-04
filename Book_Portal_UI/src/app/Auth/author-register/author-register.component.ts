import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-author-register',
  templateUrl: './author-register.component.html',
  styleUrls: ['./author-register.component.css']
})
export class AuthorRegisterComponent implements OnInit{

  registerForm!:FormGroup;

  constructor(private fb:FormBuilder) {}

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
    alert("hello");
    if(this.registerForm.valid){
      console.log(this.registerForm.value);
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
