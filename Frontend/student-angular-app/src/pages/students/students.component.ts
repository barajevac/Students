import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Student } from 'src/models/student/student';
import { StudentService } from 'src/services/student.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css'],
})
export class StudentsComponent implements OnInit {
  constructor(public studentService: StudentService) {}

  @Output() studentForAdding = new EventEmitter<any>();
  @Output() studentForEditing = new EventEmitter<Student>();

  public students: Array<Student> = [];

  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'email',
    'phone',
    'operation',
  ];

  ngOnInit(): void {
    this.getData();
  }

  public getData(): void {
    this.studentService.getAllStudents().subscribe((res) => {
      this.students = res;
    });
  }

  public addStudent(): void {
    this.studentForAdding.emit(null);
  }

  public editStudent(student: Student): void {
    this.studentForEditing.emit(student);
  }

  public deleteStudent(id: string): void {
    this.studentService.deleteStudent(id).subscribe(() => {
      this.students = this.students.filter((s) => {
        return s.id !== id;
      });
    });
  }

  public AddStudent(student: Student): void {
    this.students = [...this.students, student];
  }
}
