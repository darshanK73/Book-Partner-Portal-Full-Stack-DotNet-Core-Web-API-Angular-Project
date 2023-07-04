import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
@Injectable({
    providedIn: 'root'
})
export class LogGuard implements CanActivate {
    
  constructor(private authService:AuthService,private router:Router,private toast:NgToastService){}

    canActivate(): boolean {
      if(!this.authService.isUserLoggedIn())
      {
        return true;
      }
      else {
        this.toast.warning({detail:"Warning",summary:"You are already logged In"});
        this.router.navigate(["home"]);
      }
      return false;
    }
}