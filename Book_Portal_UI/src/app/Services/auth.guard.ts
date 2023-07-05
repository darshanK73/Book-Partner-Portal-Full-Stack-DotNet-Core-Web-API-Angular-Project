import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    
  constructor(private authService:AuthService,private router:Router,private toast:NgToastService){}

    canActivate(): boolean {
        if(this.authService.isUserLoggedIn())
        {
          return true;
        }
        else {
          this.toast.warning({detail:"Warning",summary:"Please Log In First"})
          this.router.navigate(["login"]);
        }
        return false;
    }
}