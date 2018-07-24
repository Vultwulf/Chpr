import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Globals } from './globals';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private globals: Globals) { }

  tokenRequest(userName, password) {
    /// <summary>Service method to request the authentication token.</summary>  
    /// <param name="userName" type="text">The user name.</param>
    /// <param name="password" type="text">The user password.</param>  
    /// <returns type="Object">The Authentication object</returns>  
    var data = "username=" + userName + "&password=" + password + "&grant_type=password";
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True' });
    return this.http.post('/token', data, { headers: reqHeader });
  }

  getUserClaims() {
    /// <summary>Service method to get valid user claims.</summary>  
    /// <returns type="Object">The User Claims object</returns> 
    return this.http.get('/api/auth',
      { headers: new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('userToken') }) });
  }

}
