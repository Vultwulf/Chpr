import { Injectable } from "@angular/core";

@Injectable()
export class Globals {
  isLoggedIn: boolean = false;

  // Method to logout
  Logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userName');
    this.isLoggedIn = false;
  }

}


