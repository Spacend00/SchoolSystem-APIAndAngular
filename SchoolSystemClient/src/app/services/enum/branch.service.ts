import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Branch {
  id: number,
  name: string,
}

@Injectable({
  providedIn: 'root',
})
export class BranchService {
  private http = inject(HttpClient);
  private apiUrl = "https://localhost:7038/api/enums";

  getBranchesList(): Observable<Branch[]> {
    return this.http.get<Branch[]>(`${this.apiUrl}/get-branches`);
  }

  getById(id: number): Observable<Branch> {
    return this.http.get<Branch>(`${this.apiUrl}/getby-id/${id}`)
  }
}
