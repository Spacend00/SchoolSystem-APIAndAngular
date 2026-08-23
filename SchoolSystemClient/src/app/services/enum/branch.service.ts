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
  private apiUrl = "https://localhost:7038/api/enums/get-branches";

  getBranches(): Observable<Branch[]> {
    return this.http.get<Branch[]>(this.apiUrl);
  }
}
