import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transfers } from 'src/app/models/Transfers';

@Injectable({
  providedIn: 'root'
})
export class TransferService {

  constructor(
    private http: HttpClient
  ) { }

  createTransfer(transfer: Transfers): Observable<Transfers> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });
    return this.http.post<Transfers>('http://localhost:5057/api/Transfers', transfer, { headers });
  }

  getTransfersByAccountId(accountId: number): Observable<Transfers[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
      'Content-Type': 'application/json'
    });
    return this.http.get<Transfers[]>(`http://localhost:5057/api/Transfers/account/${accountId}`, { headers });
  }

}
