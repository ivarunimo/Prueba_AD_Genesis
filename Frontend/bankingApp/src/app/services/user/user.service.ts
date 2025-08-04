import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Users } from 'src/app/models/Users';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient

  ) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post('http://localhost:5057/api/Login', { username, password });
  }

  register(user: Users): Observable<any> {
    return this.http.post('http://localhost:5057/api/Users/', user);
  }

}
