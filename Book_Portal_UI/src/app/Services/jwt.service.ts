import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  private role$ = new BehaviorSubject<string>("");
  private email$ = new BehaviorSubject<string>("");
  private user$ = new BehaviorSubject<any>({});
  private userId$ = new BehaviorSubject<string>("");

  constructor() { }

  public getRole(){
    return this.role$.asObservable();
  }

  public setRole(role:string){
    this.role$.next(role);
  }

  public removeRole(){
    this.role$ = new BehaviorSubject<string>("");
  }

  public setUserId(user:string)
  {
    let u= JSON.parse(user);
    console.log(u);
    this.userId$.next(u.AuId);
  }

  public getUserId()
  {
    return this.userId$.asObservable();
  }

  public removeUserId(){
    this.userId$ = new BehaviorSubject<string>("");
  }

  public getEmail(){
    return this.email$.asObservable();
  }

  public setEmail(email:string){
    this.email$.next(email);
  }

  public removeEmail(){
    this.email$ = new BehaviorSubject<string>("");
  }

  public getUser(){
    return this.user$.asObservable();
  }

  public setUser(user:string){
    this.user$.next(JSON.parse(user));
  }

  public removeUser(){
    this.user$ = new BehaviorSubject<any>({});
  }
}
