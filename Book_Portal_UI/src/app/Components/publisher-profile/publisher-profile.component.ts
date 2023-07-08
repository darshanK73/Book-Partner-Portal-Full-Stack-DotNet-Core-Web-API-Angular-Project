import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtService } from 'src/app/Services/jwt.service';

@Component({
  selector: 'app-publisher-profile',
  templateUrl: './publisher-profile.component.html',
  styleUrls: ['./publisher-profile.component.css']
})
export class PublisherProfileComponent implements OnInit {

  publisherUpdateForm!: FormGroup;
  publisherId!: string;
  publisher!: any;

  constructor(private fb: FormBuilder, private authService: AuthService,private cd: ChangeDetectorRef, private router: Router, private toast: NgToastService, private jwtService: JwtService) { }


  ngOnInit(): void {


    this.publisherUpdateForm = this.fb.group({
      username: new FormControl({ value: '', disabled: false }, Validators.required),
      password: new FormControl({ value: '', disabled: false }, Validators.required),
      email: new FormControl({ value: '', disabled: false }, Validators.required),
      pubName: new FormControl({ value: '', disabled: false }, Validators.required),
      city: new FormControl({ value: '', disabled: false }, Validators.required),
      state: new FormControl({ value: '', disabled: false }, Validators.required),
      country: new FormControl({ value: '', disabled: false }, Validators.required),
      logo: new FormControl({ value: null, disabled: false }, Validators.required),
      prInfo: new FormControl({ value: '', disabled: false }, Validators.required)
    });


    this.jwtService.getUserId().subscribe(val => {
      let u = this.authService.getUserIdFromToken();
      console.log(u);
      // console.log(u);
      this.publisherId = u || val
    
    });

    this.jwtService.getUser().subscribe(val => {
      let u = this.authService.getUserFromToken();
      console.log(u);
      console.log(val);
      this.publisher = u || val;
      this.publisherId = this.publisher.PubId;

      this.publisherUpdateForm.setValue({
        username: this.publisher.Username,
        password:'',
        email: this.publisher.Email,
        pubName:this.publisher.PubName,
        city: this.publisher.City,
        state: this.publisher.State,
        country: this.publisher.Country,
        logo: null,
        prInfo:''
      });
    });

    console.log(this.publisherId);
    console.log(this.publisher);
  }

  onFileChange(event: any) {

    var reader = new FileReader();
   
    var file = event.target.files[0];

    this.publisherUpdateForm.patchValue({
      logo: file
    });

    this.cd.markForCheck();
  };

  onSubmit() {
    if (this.publisherUpdateForm.valid) {
      var obj = this.publisherUpdateForm.value;
      var formData = new FormData();
      formData.append('pubName', obj.pubName);
      formData.append('city', this.publisherUpdateForm.value.city);
      formData.append('logo', obj.logo,obj.logo.name);
      formData.append('state', this.publisherUpdateForm.value.state);
      formData.append('username',this.publisherUpdateForm.value.username);
      formData.append('country', this.publisherUpdateForm.value.country);
      formData.append('password',this.publisherUpdateForm.value.password);
      formData.append('email', this.publisherUpdateForm.value.email);
      formData.append('prInfo', this.publisherUpdateForm.value.prinfo);


      this.authService.updatePublisher(this.publisherId, formData).subscribe({
        next: (res) => {
          console.log(res);
          this.toast.success({ detail: 'Success', summary: res.message, duration: 5000 });
          this.publisherUpdateForm.reset();
        },
        error: (err) => {
          console.log(err);
          this.toast.error({ detail: 'Error', summary: err.error, duration: 5000 });
        }
      });

    }

    else {
      console.log(this.publisherUpdateForm)
      this.validateFormField(this.publisherUpdateForm);
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
