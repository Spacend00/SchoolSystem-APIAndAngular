import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentGetAllActiveResponse, StudentGetAllResponse, StudentGetByEmailAndIdResponse, StudentUpdateRequest } from '../../models/student/student.model';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private http = inject(HttpClient);
  private apiUrl = "https://localhost:7038/api/students";

  getAll(): Observable<StudentGetAllResponse[]> {
    return this.http.get<StudentGetAllResponse[]>(`${this.apiUrl}/getall`);
  }

  getAllActive(): Observable<StudentGetAllActiveResponse[]> {
    return this.http.get<StudentGetAllActiveResponse[]>(`${this.apiUrl}/getall-active`);
  }

  getById(id: string): Observable<StudentGetByEmailAndIdResponse> {
    return this.http.get<StudentGetByEmailAndIdResponse>(`${this.apiUrl}/getby-id/${id}`);
  } 

  getByEmail(email: string): Observable<StudentGetByEmailAndIdResponse> {
    return this.http.get<StudentGetByEmailAndIdResponse>(`${this.apiUrl}/getby-email/${email}`);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }

  update(data: StudentUpdateRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, data);
  }
}
