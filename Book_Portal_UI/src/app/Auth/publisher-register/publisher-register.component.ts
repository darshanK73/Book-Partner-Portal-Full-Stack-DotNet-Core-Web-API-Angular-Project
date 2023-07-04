import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-publisher-register',
  templateUrl: './publisher-register.component.html',
  styleUrls: ['./publisher-register.component.css']
})
export class PublisherRegisterComponent implements OnInit{

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
      country:['',Validators.required],
      email:['',Validators.required],
      logo:['',Validators.required],
      prinfo:['',Validators.required]
    });
  }

  onSubmit(){
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
