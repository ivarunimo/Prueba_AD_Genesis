import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DebitCards } from 'src/app/models/DebitCards';
@Injectable({
  providedIn: 'root'
})
export class DebitCardService {

  private apiUrl = 'http://localhost:5057/api/DebitCard';

  constructor(private http: HttpClient) { }

  getDebitCards(userId: number): Observable<DebitCards[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });
    return this.http.get<DebitCards[]>(`${this.apiUrl}/${userId}`, { headers });
  }
}

