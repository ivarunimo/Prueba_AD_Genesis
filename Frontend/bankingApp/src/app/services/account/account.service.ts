import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Accounts } from 'src/app/models/Accounts';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient
  ) { }


  UserId_Account(user_id: number): Observable<Accounts[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });

    return this.http.get<Accounts[]>(`http://localhost:5057/api/Accounts/${user_id}`, { headers });
  }

  getAccountById(id: number): Observable<Accounts> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });

    return this.http.get<Accounts>(`http://localhost:5057/api/Accounts/GetAccount/${id}`, { headers });
  }
}



