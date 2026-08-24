import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TeacherResponse, TeacherUpdateRequest } from '../../models/teacher/teacher.model';

@Injectable({
  providedIn: 'root',
})
export class TeacherService {
  private http = inject(HttpClient);
  private apiUrl = "https://localhost:7038/api/teacher";

  getAll(): Observable<TeacherResponse> {
    return this.http.get<TeacherResponse>(`${this.apiUrl}/all`);
  }

  getAllActive(): Observable<TeacherResponse> {
    return this.http.get<TeacherResponse>(`${this.apiUrl}/all-active`);
  }

  getById(id: string): Observable<TeacherResponse> {
    return this.http.get<TeacherResponse>(`${this.apiUrl}/getby-id/${id}`);
  }

  getByEmail(email: string): Observable<TeacherResponse> {
    return this.http.get<TeacherResponse>(`${this.apiUrl}/getby-email/${email}`);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  update(data: TeacherUpdateRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, data);
  }
}
