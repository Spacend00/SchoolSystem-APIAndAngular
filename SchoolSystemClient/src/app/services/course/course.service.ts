import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CourseResponse, CourseUpdateRequest } from '../../models/course/course.model';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  private http = inject(HttpClient);
  private apiUrl = "https://localhost:7038/api/course";

  getAll(): Observable<CourseResponse> {
    return this.http.get<CourseResponse>(`${this.apiUrl}/getall`);
  }

  getAllActive(): Observable<CourseResponse> {
    return this.http.get<CourseResponse>(`${this.apiUrl}/getall-active`);
  }

  getById(id: string): Observable<CourseResponse> {
    return this.http.get<CourseResponse>(`${this.apiUrl}/getby-id/${id}`);
  }

  update(data: CourseUpdateRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, data);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }
}
