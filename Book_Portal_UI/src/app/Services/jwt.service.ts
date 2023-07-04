import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  private role$ = new BehaviorSubject<string>("");
  private email$ = new BehaviorSubject<string>("");
  private user$ = new BehaviorSubject<string>("");

  constructor() { }

  public getRole(){
    return this.role$.asObservable();
  }

  public setRole(role:string){
    this.role$.next(role);
  }

  public getEmail(){
    return this.email$.asObservable();
  }

  public setEmail(email:string){
    this.email$.next(email);
  }

  public getUser(){
    return this.user$.asObservable();
  }

  public setUser(user:string){
    this.user$.next(user);
  }
}
