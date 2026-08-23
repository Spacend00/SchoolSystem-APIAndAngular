import { Component, signal } from '@angular/core';
import { LoginPageComponent } from "./components/login/login-page/login-page.component";
import { RegisterPageComponent } from "./components/register/register-page/register-page.component";

@Component({
  selector: 'app-root',
  imports: [ RegisterPageComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
