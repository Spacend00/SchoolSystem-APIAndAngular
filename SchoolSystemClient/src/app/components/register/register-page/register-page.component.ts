import { Component, inject, OnInit, signal } from '@angular/core';
import { RegisterService } from '../../../services/auth/register.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterRequestStudent, RegisterRequestTeacher } from '../../../models/auth/register.model';
import { Branch, BranchService } from '../../../services/enum/branch.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from "@angular/router";
import { first } from 'rxjs';
import { email } from '@angular/forms/signals';


type Role = 'student' | 'teacher';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss',
})
export class RegisterPageComponent implements OnInit {
  private service = inject(RegisterService);
  private branchService = inject(BranchService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  protected activeRole: Role = 'student';
  protected formBody!: FormGroup;
  protected branches = signal<Branch[] | null>(null);

  private mapStudentReq(formValue: any): RegisterRequestStudent{
    return {
      name: formValue.firstName,
      surname: formValue.lastName,
      age: formValue.age,
      schoolNumber: formValue.schoolNumber,
      email: formValue.email,
      password: formValue.password
    }
  }
  private mapTeacherReq(formValue: any): RegisterRequestTeacher{
    return {
      name: formValue.firstName,
      surname: formValue.lastName,
      age: formValue.age,
      branch: formValue.branch,
      email: formValue.email,
      password: formValue.password
    }
  }
  ngOnInit(): void {
    this.formBody = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      age: [null, [Validators.required, Validators.min(15), Validators.max(130)]],
      schoolNumber: [null],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      branch: [null]
    });

    this.getBranches();
  }

  getBranches(): void {
    this.branches.set(null);
    this.branchService.getBranchesList().subscribe({
      next: (response) => {
        this.branches.set(response);
      },
      error: (err) => {
        console.log(err);        
      }
    });  
  }

  setRole(inputRole: Role): void {
    this.activeRole = inputRole;
    this.formBody.reset();
  }

  onSumbit(): void {
    if(this.formBody.invalid){
      this.formBody.markAllAsTouched();
      return;
    }
    
    if(this.activeRole === 'student'){
      const request = this.mapStudentReq(this.formBody.value); 
      
      this.service.registerStudent(request).subscribe({
        next: (response) => {
          this.router.navigate(['/login'], {state: { email: response.email}});      
        },
        error: (err) => {
          console.log(err);          
        }
      });
    }

    if(this.activeRole === 'teacher'){
      const request = this.mapTeacherReq(this.formBody.value);
      
      this.service.registerTeacher(request).subscribe({
        next: (response) => {
          this.router.navigate(['/login'], {state: {email: response.email}});         
        },
        error: (err) => {
          console.log(err);          
        }
      });
    }
  }

}
