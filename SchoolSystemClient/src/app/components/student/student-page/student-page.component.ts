import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from '../../../services/student/student.service';
import { NgClass } from '@angular/common';
import { CourseService } from '../../../services/course/course.service';
import { CustomJwtPayload, StudentGetByEmailAndIdResponse } from '../../../models/student/student.model';

@Component({
  selector: 'app-student-page',
  imports: [],
  templateUrl: './student-page.component.html',
  styleUrl: './student-page.component.scss',
})
export class StudentPageComponent implements OnInit {
  private service = inject(StudentService);
  private courseService = inject(CourseService);
  private fb = inject(FormBuilder);

  protected updateBody!: FormGroup;
  protected student = signal<StudentGetByEmailAndIdResponse | null>(null);
  
  ngOnInit(): void {
    this.updateBody = this.fb.group({
      id: [null, [Validators.required]],
      name: [null],
      surname: [null],
      age: [null, [Validators.min(15), Validators.max(130)]]
    });

    const id = this.getUserId(this.getDecodedToken());
    this.getStudent(id);
  }

  getStudent(id: string | null){
    this.service.getById(id).subscribe({
      next: (response) => {
        this.student.set(response);
      },
      error: (err) => {
        console.error("Öğrenci bulunamadı:", err);
        this.student.set(null);
      }
    });
  }

  getDecodedToken(): CustomJwtPayload | null {
    const token = localStorage.getItem('token');
    if(!token) return null;

    try{
      const payloadBase64 = token.split('.')[1];
      const decodedJson = atob(payloadBase64);
      return JSON.parse(decodedJson) as CustomJwtPayload;
    }catch(error){
      console.error("Token decoder error:", error);
      return null;
    }
  }

  getUserId(payload: CustomJwtPayload | null): string | null {
    return payload ? payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] : null;
  }
}
