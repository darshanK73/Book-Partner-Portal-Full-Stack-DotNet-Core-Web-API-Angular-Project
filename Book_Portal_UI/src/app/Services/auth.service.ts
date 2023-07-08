import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorRegisterRequest } from '../Models/author-register-request';
import { LoginRequest } from '../Models/login-request';
import { LoginResponse } from '../Models/login-response';
import { PublisherRegisterRequest } from '../Models/publisher-register-request';
import { MessageResponse } from '../Models/message-response';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgToastService } from 'ng-angular-popup';
import { environment } from 'src/environments/environment';
import { JwtService } from './jwt.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = environment.baseUrl;

  private userPayload:any;

  constructor(private http:HttpClient,private router:Router,private toast:NgToastService,private jwtService:JwtService) { 
    this.userPayload = this.decodedToken();
  }

  login(request : LoginRequest) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`,request);
  }

  signupAuthor(request : AuthorRegisterRequest){
    return this.http.post<MessageResponse>(`${this.baseUrl}/auth/author/register`,request)
  }

  updateAuthor(id:string,request : AuthorRegisterRequest){
    return this.http.put<MessageResponse>(`${this.baseUrl}/auth/author/update/${id}`,request);
  }

  signupPublisher(request: FormData){
    return this.http.post<MessageResponse>(`${this.baseUrl}/auth/publisher/register`,request);
  }

  updatePublisher(id:string,request : FormData){
    console.log(id);
    console.log(request);
    return this.http.put<MessageResponse>(`${this.baseUrl}/auth/publisher/update/${id}`,request);
  }

  storeToken(token:string)
  {
    localStorage.removeItem("token");
    localStorage.setItem("token",token);
  }

  logout(){
    localStorage.removeItem("token");
    this.jwtService.removeEmail();
    this.jwtService.removeRole();
    this.jwtService.removeUser();
    this.jwtService.removeUserId();
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
    this.userPayload = this.decodedToken();
      return this.userPayload.role;
  }

  getEmailFromToken(){
    this.userPayload = this.decodedToken();
      return this.userPayload.email;
  }

  getUserFromToken(){
    this.userPayload = this.decodedToken();
      return JSON.parse(this.userPayload.JSON);
  }

  getUserIdFromToken(){
    this.userPayload = this.decodedToken();
      console.log(JSON.parse(this.userPayload.JSON));
      return JSON.parse(this.userPayload.JSON).AuId;
    
  }
}

