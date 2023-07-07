import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm! : FormGroup;

  env = environment.baseUrl;
  

  constructor(private fb:FormBuilder,private authService:AuthService, private router:Router, private toast:NgToastService, private jwtService:JwtService){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      role:['',Validators.required],
      username:['',Validators.required],
      password:['',Validators.required]
    })

    console.log(this.env);
    console.log(this.jwtService.getUser());
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

  togglePasswordVisibility() {
    const passwordInput = document.getElementById("password") as HTMLInputElement;
    const eyeIcon = document.querySelector(".toggle-password") as HTMLElement;
  
    if (passwordInput.type === "password") {
      passwordInput.type = "text";
      eyeIcon.classList.remove("bi-eye-slash-fill");
      eyeIcon.classList.add("bi-eye-fill");
    } else {
      passwordInput.type = "password";
      eyeIcon.classList.remove("bi-eye-fill");
      eyeIcon.classList.add("bi-eye-slash-fill");
    }
  }

}
