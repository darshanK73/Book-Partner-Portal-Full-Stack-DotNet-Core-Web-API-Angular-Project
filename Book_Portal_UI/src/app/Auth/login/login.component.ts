import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm! : FormGroup;

  constructor(private fb:FormBuilder){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      role:['',Validators.required],
      username:['',Validators.required],
      password:['',Validators.required]
    })
  }

  onSubmit(){
    if(this.loginForm.valid){
      console.log(this.loginForm.value);
    }
    else{
      this.validateFormField(this.loginForm);
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
