import { Component, ViewChild } from '@angular/core';
import { Student } from 'src/models/student/student';
import { StudentsComponent } from 'src/pages/students/students.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  @ViewChild(StudentsComponent)
  studentComponent!: StudentsComponent;

  public studentFormIsVisible: boolean = false;
  public declare student: Student;

  ngOnChanges() {}

  public addStudent(student: Student): void {
    this.student = student;
    this.studentFormIsVisible = true;
  }
  public editStudent(student: Student): void {
    this.student = student;
    this.studentFormIsVisible = true;
  }

  public closeForm(): void {
    this.studentFormIsVisible = false;
  }

  public updateStudentTable(student: Student): void {
    this.studentComponent.AddStudent(student);
  }
}
