import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { StudentPageComponent } from "./components/student/student-page/student-page.component";

@Component({
  selector: 'app-root',
  imports: [RouterModule, RouterOutlet, StudentPageComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
