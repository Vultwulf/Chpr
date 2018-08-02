import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { UserService } from '../user.service';
import { Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { HttpRequest } from 'selenium-webdriver/http';
import { Globals } from '../globals';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  constructor(private router: Router, private userService: UserService, private globals: Globals) { }

  // Class variables
  isLoginError = false;

  // Initialize
  ngOnInit() {
    // Revalidate token
    this.userService.getUserClaims().subscribe((data: any) => {
      this.globals.userClaims = data;
      this.globals.isLoggedIn = true;
      localStorage.setItem('userName', data.UserName);
    },
      (err: HttpErrorResponse) => {
        // If the user claims are no longer valid, log the user out
        if (err.statusText === "Unauthorized") {
          this.globals.Logout();
        }
      }
    );
  }

  // On Form Submit
  OnSubmit(userName, password) {
    this.userService.tokenRequest(userName, password).subscribe((data: any) => {
      // Save the token to local storage
      localStorage.setItem('userToken', data.access_token);

      //Log in was successful 
      this.isLoginError = false;

      // Reinitialize the ng
      this.ngOnInit();
    },
      (err: HttpErrorResponse) => {
        this.isLoginError = true;
        this.globals.isLoggedIn = false;
      });
  }

}
