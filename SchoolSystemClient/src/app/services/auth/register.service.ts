import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterRequestStudent, RegisterRequestTeacher, RegisterResponse } from '../../models/auth/register.model';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  private http = inject(HttpClient);
  private apiUrl = "https://localhost:7038/api/register"

  registerStudent(data: RegisterRequestStudent): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/student`, data);
  }

  registerTeacher(data: RegisterRequestTeacher): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/teacher`, data);
  }
}
