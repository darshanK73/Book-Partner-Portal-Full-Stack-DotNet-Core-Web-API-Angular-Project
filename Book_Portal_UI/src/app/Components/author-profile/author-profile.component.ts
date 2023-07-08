import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthorRegisterRequest } from 'src/app/Models/author-register-request';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-author-profile',
  templateUrl: './author-profile.component.html',
  styleUrls: ['./author-profile.component.css']
})
export class AuthorProfileComponent implements OnInit {

  authorUpdateForm!: FormGroup;
  authorId!: string;
  author!: any;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toast: NgToastService, private jwtService: JwtService) { }


  ngOnInit(): void {


    this.authorUpdateForm = this.fb.group({
      username: new FormControl({ value: '', disabled: false }, Validators.required),
      password: new FormControl({ value: '', disabled: false }, Validators.required),
      firstName: new FormControl({ value: '', disabled: false }, Validators.required),
      lastName: new FormControl({ value: '', disabled: false }, Validators.required),
      city: new FormControl({ value: '', disabled: false }, Validators.required),
      state: new FormControl({ value: '', disabled: false }, Validators.required),
      zip: new FormControl({ value: '', disabled: false }, Validators.required),
      address: new FormControl({ value: '', disabled: false }, Validators.required),
      email: new FormControl({ value: '', disabled: false }, Validators.required),
      phone: new FormControl({ value: '', disabled: false }, Validators.required),
    });


    this.jwtService.getUserId().subscribe(val => {
      let u = this.authService.getUserIdFromToken();
      console.log(u);
      // console.log(u);
      this.authorId = u || val
    });

    this.jwtService.getUser().subscribe(val => {
      let u = this.authService.getUserFromToken();
      console.log(u);
      console.log(val);
      this.author = u || val;

      this.authorUpdateForm.setValue({
        username: this.author.Username,
        password:'',
        firstName: this.author.AuFname,
        lastName: this.author.AuLname,
        city: this.author.City,
        state: this.author.State,
        zip: this.author.Zip,
        address: this.author.Address,
        email: this.author.Email,
        phone: this.author.Phone
      });
    });

    console.log(this.authorId);
    console.log(this.author);
  }

  onSubmit() {
    if (this.authorUpdateForm.valid) {
      console.log(this.authorUpdateForm.value);

      var obj = this.authorUpdateForm.value;
      let request = new AuthorRegisterRequest();

      request.username = obj.username;
      request.password = obj.password;
      request.email = obj.email;
      request.auFname = obj.firstName;
      request.auLname = obj.lastName;
      request.city = obj.city;
      request.phone = obj.phone;
      request.address = obj.address;
      request.state = obj.state;
      request.zip = obj.zip;

      console.log(request);


      this.authService.updateAuthor(this.authorId, request).subscribe({
        next: (res) => {
          this.toast.success({ detail: 'Success', summary: res.message, duration: 5000 });
          this.authorUpdateForm.reset();
        },
        error: (err) => {
          this.toast.error({ detail: 'Error', summary: err.error.message, duration: 5000 });
        }
      });

    }

    else {
      console.log(this.authorUpdateForm)
      this.validateFormField(this.authorUpdateForm);
    }
  }

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
