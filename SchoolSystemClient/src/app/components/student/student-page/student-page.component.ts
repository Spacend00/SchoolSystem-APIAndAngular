import { Component, inject, OnInit, signal } from '@angular/core';
import { TeacherService } from '../../../services/teacher/teacher.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentGetAllActiveResponse, StudentGetAllResponse, StudentGetByEmailAndIdResponse, StudentUpdateRequest } from '../../../models/student/student.model';
import { StudentService } from '../../../services/student/student.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-student-page',
  imports: [NgClass],
  templateUrl: './student-page.component.html',
  styleUrl: './student-page.component.scss',
})
export class StudentPageComponent implements OnInit {
  private service = inject(StudentService);
  private fb = inject(FormBuilder);

  protected isLoading: boolean = false;
  protected errorMessage: string | null = null;

  protected updateData!: FormGroup;
  protected students = signal<StudentGetAllResponse[]>([]);
  protected activeStudents = signal<StudentGetAllActiveResponse[]>([]);
  protected student = signal<StudentGetByEmailAndIdResponse | null>(null);
  protected inputId: string | null = null;
  protected inputEmail: string | null = null;

  clear(): void {
    this.student.set(null);
    this.students.set([]);
    this.activeStudents.set([]);
  }

  ngOnInit(): void {
    this.updateData = this.fb.group({
      id: ["", [Validators.required]],
      name: [null],
      surname: [null],
      age: [null, [Validators.min(15), Validators.max(130)]]
    });
  }

  loadStudents(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.clear();

    this.service.getAll().subscribe({
      next: (response) => {
        this.students.set(response);
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = "Öğrenciler yüklenirken bir hata oluştu!";
        this.isLoading = false;     
        console.log(`GetAll Hatası: ${err}`);           
      }
    });
  }

  loadActiveStudents(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.clear();

    this.service.getAllActive().subscribe({
      next: (response) => {
        this.activeStudents.set(response);
        this.isLoading = false;      
      },
      error: (err) => {
        this.errorMessage = "Aktif öğrenciler listelenemedi"
        this.isLoading = false;
        console.log("GetAllActive Hatası:", err);
      }
    });
  }

  loadStudentById(): void {    
    if(this.inputId){
      this.isLoading = true;
      this.errorMessage = null;
      this.clear();

      this.service.getById(this.inputId).subscribe({
        next: (response) => {
          this.student.set(response);
          this.isLoading = false;
        },
        error: (err) => {
          this.errorMessage = "Öğrenci bulunamadı";
          this.isLoading = false;
          console.log("GetById Hatası:", err); 
        }
      });
    }
  }

  loadStudentByEmail(): void {
    if(this.inputEmail){
      this.isLoading = true;
      this.errorMessage = null;
      this.clear();

      this.service.getByEmail(this.inputEmail).subscribe({
        next: (response) => {
          this.student.set(response);
          this.isLoading = false;
        },
        error: (err) => {
          this.errorMessage = "Öğrenci bulunamadı";
          this.isLoading = false;
          console.log("GetByEmail Hatası:", err);          
        }
      });
    }
  }

  deleteStudent(): void {
    if(this.inputId){
      this.isLoading = true;
      this.errorMessage = null;

      this.service.delete(this.inputId).subscribe({
        next: () => {
          alert("Öğrenci başarıyla silindi.");
          this.isLoading = false;
        },
        error: (err) => {
          this.errorMessage = "Öğrenci silinemedi.";
          this.isLoading = false;
          console.log("Delete Hatası:", err);          
        }
      });
    }
  }

  updateStudent(): void {
    if(this.updateData.valid){
      this.isLoading = true;
      this.errorMessage = null;
      const request = this.updateData.value as StudentUpdateRequest;

      this.service.update(request).subscribe({
        next: () => {
          this.isLoading = false;
          alert("Öğrenci güncellendi.");
        },
        error: (err) => {
          this.errorMessage = "Öğrenci güncellenemedi.";
          this.isLoading = false;
          console.log("Update Hatası:", err);         
        }
      });
    }
  }
}
