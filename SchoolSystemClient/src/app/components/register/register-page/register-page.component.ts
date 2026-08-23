import { Component, inject, OnInit } from '@angular/core';
import { RegisterService } from '../../../services/auth/register.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterRequestStudent, RegisterRequestTeacher } from '../../../models/auth/register.model';
import { Branch, BranchService } from '../../../services/enum/branch.service';
import { CommonModule } from '@angular/common';


type Role = 'student' | 'teacher';

@Component({
  selector: 'app-register-page',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss',
})
export class RegisterPageComponent implements OnInit {
  private service = inject(RegisterService);
  private branchService = inject(BranchService);
  private fb = inject(FormBuilder);

  protected activeRole: Role = 'student';
  protected formBody!: FormGroup;
  protected branches!: Branch[];

  ngOnInit(): void {
    this.formBody = this.fb.group({
      firstName: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(8)]],
      branch: [null]
    });
  }

  getBranches(): void {
    if(this.branches != null) return;
    this.branchService.getBranches().subscribe({
      next: (response) => {
        this.branches = response
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
      const request: RegisterRequestStudent = this.formBody.value as RegisterRequestStudent;
      this.service.registerStudent(request).subscribe({
        next: (response) => {
          console.log(response);          
        },
        error: (err) => {
          console.log(err);          
        }
      });
    }

    if(this.activeRole === 'teacher'){
      const request: RegisterRequestTeacher = this.formBody.value as RegisterRequestTeacher;
      this.service.registerTeacher(request).subscribe({
        next: (response) => {
          console.log(response);          
        },
        error: (err) => {
          console.log(err);          
        }
      });
    }
  }

}
