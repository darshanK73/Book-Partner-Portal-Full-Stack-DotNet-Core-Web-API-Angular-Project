import { HttpHeaders, HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { PublisherRegisterRequest } from 'src/app/Models/publisher-register-request';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-publisher-register',
  templateUrl: './publisher-register.component.html',
  styleUrls: ['./publisher-register.component.css']
})
export class PublisherRegisterComponent implements OnInit {

  registerForm!: FormGroup;
  // selectedFile: any;


  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private cd: ChangeDetectorRef,private toast:NgToastService) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      password1: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      email: ['', Validators.required],
      logo: [null, Validators.required],
      prinfo: ['', Validators.required]
    });
  }

  // matchPassword:ValidatorFn = (control:AbstractControl): ValidationErrors | null =>{

  //   let password = control.get('password');
  //   let password1 = control.get('password1');

  //   if(password && password?.value !== password1?.value){
  //     return {
  //       passwordMatchError:true
  //     }
  //   }
  //   return null;
  // }

  // MustMatch(p1:string,p2:string){
  //   return (formGroup:FormGroup)=>{
  //     const control = formGroup.controls[p1];
  //     const matchingControl = formGroup.controls[p2];
  //     if(matchingControl.errors && !matchingControl.errors['MustMatch'])
  //     {
  //       return;
  //     }
  //     if(control.value !== matchingControl.value){
  //       return matchingControl.setErrors({MustMatch:true});
  //     }
  //     else 
  //     {
  //       return matchingControl.setErrors(null);
  //     }
  //   }
  // }
  
  onSubmit() {
    if (this.registerForm.valid) {
      var obj = this.registerForm.value;
      var formData = new FormData();
      formData.append('pubName', `${obj.firstName} ${obj.lastName}`);
      formData.append('city', this.registerForm.value.city);
      formData.append('logo', obj.logo,obj.logo.name);
      formData.append('state', this.registerForm.value.state);
      formData.append('username',this.registerForm.value.username);
      formData.append('country', this.registerForm.value.country);
      formData.append('password',this.registerForm.value.password);
      formData.append('email', this.registerForm.value.email);
      formData.append('prInfo', this.registerForm.value.prinfo);
      
      this.authService.signupPublisher(formData).subscribe({
        next: (res) => {
          this.toast.success({detail:'Success',summary:res.message, duration:5000});
          this.registerForm.reset();
          this.router.navigate(["login"])
        },
        error: (err) => {
          this.toast.error({detail:'Error',summary:err.error.message, duration:5000});
        }
      });
    }
    else {
      this.validateFormField(this.registerForm);
    }
  }

  onFileChange(event: any) {

    var reader = new FileReader();
   
    var file = event.target.files[0];

    this.registerForm.patchValue({
      logo: file
    });

    this.cd.markForCheck();
  };

  private validateFormField(loginForm: FormGroup) {
    Object.keys(loginForm.controls).forEach(field => {
      const control = loginForm.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true })
      }
      else if (control instanceof FormGroup) {
        this.validateFormField(control);
      }
    })
  }

}
