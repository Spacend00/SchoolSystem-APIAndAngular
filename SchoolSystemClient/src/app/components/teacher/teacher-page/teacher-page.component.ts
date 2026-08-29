import { Component, inject, OnInit, signal } from '@angular/core';
import { TeacherService } from '../../../services/teacher/teacher.service';
import { CourseService } from '../../../services/course/course.service';
import { CourseResponse, CourseResponseById, CreateCourseRequest, CreateCourseResponse } from '../../../models/course/course.model';
import { TeacherResponse } from '../../../models/teacher/teacher.model';
import { FormBuilder, FormGroup, Validators, ɵInternalFormsSharedModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

export interface JWTPayload {  
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string; 
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
}

@Component({
  selector: 'app-teacher-page',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss',
})
export class TeacherPageComponent implements OnInit {
  private teacherService = inject(TeacherService);
  private courseService = inject(CourseService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  protected courses = signal<CourseResponse[] | null>(null);
  protected teacher = signal<TeacherResponse | null>(null);
  protected teachersCourses = signal<CourseResponse[] | null>(null);
  protected course = signal<CourseResponseById | null>(null);
  protected formBody!: FormGroup;
  protected courseId = signal<string | null>(null);

  ngOnInit(): void {
    this.getTeacher();
    this.getAllActiveCourses();

    this.formBody = this.fb.group({
      name: [null, [Validators.required]],
      description: [null, [Validators.required]],
      imageData: [null, [Validators.required]],
      goal: [null, [Validators.required]],
      summary: [null, [Validators.required]],
      targetGroup: [null, [Validators.required]],
      gains: [null, [Validators.required]],
      requirements: [null, [Validators.required]]
    });    
  }

  private MapCreateCourse(form: any): CreateCourseRequest {
    return {
      teacherId: this.getTeacherId(this.getDecodedToken()),
      name: form.name,
      description: form.description,
      imageData: form.imageData,
      goal: form.goal,
      summary: form.summary,
      targetGroup: form.targetGroup,
      gains: form.gains,
      requirements: form.requirements
    }
  }

  createCourse(): void {
    
    const request = this.MapCreateCourse(this.formBody.value);
    
    this.courseService.create(request).subscribe({
      next: (response) => {
        this.courseId.set(response.id);
      },
      error: (err) => {
        console.log(err);        
      }
    });
  }

  getAllActiveCourses(): void {
    this.courses.set(null);
    this.courseService.getAllActive().subscribe({
      next: (response) => {
        this.courses.set(response);        
      },
      error: (err) => {
        console.log("Kurslar listelenemedi:", err);        
      }
    });
  }

  getCoursesByTeacherId(): void {
    this.teachersCourses.set(null);
    const id = this.getTeacherId(this.getDecodedToken());
    if(id){
      this.courseService.getByTeacherId(id).subscribe({
        next: (response) => {
          this.teachersCourses.set(response);
        },
        error: (err) => {
          console.log("Öğretmenin kursları alınırken bir hata oluştu:", err);          
        }
      });
    }
  }

  getTeacher(): void {
    const id = this.getTeacherId(this.getDecodedToken());
    this.teacherService.getById(id).subscribe({
      next: (response) => {
        this.teacher.set(response);
      },
      error: (err) => {
        console.log("Öğretmen bilgileri alınamadı", err);        
      }
    });
  }

  getCourse(id: string | null): void {
    this.course.set(null);
    if(id){
      this.courseService.getById(id).subscribe({
        next: (response) => {
          this.course.set(response);
        },
        error: (err) => {
          console.log("Kurs bulunamadı:", err);          
        }
      });
    }
  }

  getDecodedToken(): JWTPayload | null {
    const token = localStorage.getItem('token');
    if(!token) return null;

    try{
      const payloadBase64 = token.split('.')[1];
      const decodedJson = atob(payloadBase64);
      return JSON.parse(decodedJson) as JWTPayload;
    }catch(error){
      console.error("Token decoder error:", error);
      return null;
    }
  }

  getTeacherId(payload: JWTPayload | null): string | null {
    return payload ? payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] : null;
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  deleteCourse(id: any): void {
    this.courseService.delete(id).subscribe({
      next: (response) => {

      },
      error: (err) => {
        console.log("Kurs silinemedi:", err);        
      }
    });
  }

}
