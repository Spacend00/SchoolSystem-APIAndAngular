import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { RegisterPageComponent } from './components/register/register-page/register-page.component';
import { StudentPageComponent } from './components/student/student-page/student-page.component';
import { TeacherPageComponent } from './components/teacher/teacher-page/teacher-page.component';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch: 'full'},
    {path: 'login', component: LoginPageComponent},
    {path: 'register', component: RegisterPageComponent},
    {path: 'student-management', component: StudentPageComponent},
    {path: 'teacher', component: TeacherPageComponent},
    {path: '**', redirectTo: 'login'}
];
