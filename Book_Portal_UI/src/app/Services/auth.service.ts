import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorRegisterRequest } from '../Models/author-register-request';
import { LoginRequest } from '../Models/login-request';
import { LoginResponse } from '../Models/login-response';
import { PublisherRegisterRequest } from '../Models/publisher-register-request';
import { RegisterResponse } from '../Models/register-response';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgToastService } from 'ng-angular-popup';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:7022/api/auth";
  private userPayload:any;

  constructor(private http:HttpClient,private router:Router,private toast:NgToastService) { 
    this.userPayload = this.decodedToken();
  }

  login(request : LoginRequest) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`,request);
  }

  signupAuthor(request : AuthorRegisterRequest){
    return this.http.post<RegisterResponse>(`${this.baseUrl}/author/register`,request)
  }

  signupPublisher(request: FormData){
    return this.http.post<RegisterResponse>(`${this.baseUrl}/publisher/register`,request);
  }

  // public uplodeFile(file:File,pubId:string) {
  //   const headers = new HttpHeaders()
  //     .append('Content-Type', 'multipart/form-data');

  //     console.log("INside upload file");
  //     const formData: FormData = new FormData();
  //     formData.append('file',file);
  //     formData.append('PubId',pubId);
  //     formData.append('FileName',file.name);

  //   console.log("INside upload file");
  //   console.log(formData);

  //   return this.http.post(`${this.baseUrl}/UploadFile`,formData, { headers, observe: 'response' });
  // }

  storeToken(token:string)
  {
    localStorage.setItem("token",token);
  }

  logout(){
    localStorage.removeItem("token");
    this.toast.success({detail:"Success",summary:"Log out successfully!",duration:5000});
    this.router.navigate(["login"]);
  }

  getToken()
  {
    return localStorage.getItem("token");
  }

  isUserLoggedIn():boolean{
    return !!localStorage.getItem("token");
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getRoleFromToken(){
    if(this.userPayload)
      return this.userPayload.role;
  }

  getEmailFromToken(){
    if(this.userPayload)
      return this.userPayload.email;
  }

  getUserFromToken(){
    if(this.userPayload){
      return JSON.parse(this.userPayload.JSON);
    }
  }

  getUserIdFromToken(){
    if(this.userPayload){
      console.log(JSON.parse(this.userPayload.JSON));
      return JSON.parse(this.userPayload.JSON).AuId;
    }
  }
}

