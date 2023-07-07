import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm! : FormGroup;

  constructor(private fb:FormBuilder,private authService:AuthService, private router:Router, private toast:NgToastService, private jwtService:JwtService){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      role:['',Validators.required],
      username:['',Validators.required],
      password:['',Validators.required]
    })
  }

  onSubmit(){
    if(this.loginForm.valid){
      this.authService.login(this.loginForm.value).subscribe({
        next:(res)=> {
          this.authService.storeToken(res.token);
          this.toast.success({detail:'Success',summary:res.message, duration:5000});
          let payload = this.authService.decodedToken();
          this.jwtService.setEmail(payload.email);
          this.jwtService.setRole(payload.role);
          this.jwtService.setUser(payload.JSON);
          this.jwtService.setUserId(payload.JSON);
          this.loginForm.reset();
          this.router.navigate(["dashboard"])
        },
        error:(err) => {
          this.toast.error({detail:'Error',summary:err.error.message, duration:5000});
        }
      })
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
