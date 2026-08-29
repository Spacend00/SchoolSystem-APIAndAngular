import { Component, inject, OnInit } from '@angular/core';
import { LoginService } from '../../../services/auth/login.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest } from '../../../models/auth/login.model';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from "@angular/router";

type Role = 'student' | 'teacher';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, RouterModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent implements OnInit{
  private service = inject(LoginService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  protected activeRole: Role = 'student';
  protected formBody!: FormGroup;
  
  ngOnInit(): void {
    this.formBody = this.fb.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(8)]]
    });
  }
  setRole(inputRole: Role){
    this.activeRole = inputRole;
    this.formBody.reset();
  }

  onSumbit(): void {
    if(this.formBody.invalid){
      this.formBody.markAllAsTouched();
      return;
    }

    const request: LoginRequest = this.formBody.value as LoginRequest;
    const login$ = this.activeRole === 'student' ? this.service.loginStudent(request) : this.service.loginTeacher(request);

    login$.subscribe({      
      next: (response) => {
        localStorage.setItem('token', response.token);
        this.formBody.reset();
        this.activeRole === 'student'? this.router.navigate(["/student-management"]): this.router.navigate(["/teacher"]);
      },
      error: (err) => {
        console.log("Giriş başarısız:", err);        
      }
    });
  }

  isAvaibleToken(): boolean {
      const token = localStorage.getItem('token');
      if(token){
        return true;
      }else{
        return false;
      }
    }
}
