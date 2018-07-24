import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getPosts() {
    /// <summary>Service method to get all posts.</summary>  
    /// <returns type="JSON">JSON Array of post objects.</returns> 
    return this.http.get('/api/posts');
  }

  savePost(text) {
    /// <summary>Service method to get all posts.</summary>
    /// <param type="string>Post text to be saved to the server.</param>
    /// <returns type="Any">Server response codes.</returns> 
    var reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('userToken')
    });
    return this.http.post('/api/posts', {text:text}, { headers: reqHeader });
  }

}
