import { Injectable } from "@angular/core";

@Injectable()
export class Globals {
  isLoggedIn: boolean = false;
  userClaims: Object;

  // Method to logout
  Logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userName');
    this.isLoggedIn = false;
  }

}


