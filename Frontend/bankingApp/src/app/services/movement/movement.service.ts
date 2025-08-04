import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movements } from 'src/app/models/Movements';

@Injectable({
  providedIn: 'root'
})
export class MovementService {

  private apiUrl = 'http://localhost:5057/api/Movements';

  constructor(private http: HttpClient) { }

  getMovements(accountId: number): Observable<Movements[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });
    return this.http.get<Movements[]>(`${this.apiUrl}/Account/${accountId}`, { headers });
  }

  getUserMovements(user_id: number): Observable<Movements[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });
    return this.http.get<Movements[]>(`${this.apiUrl}/User/${user_id}`, { headers });
  }
}

