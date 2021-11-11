import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Student } from 'src/models/student/student';
import { StudentService } from 'src/services/student.service';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css'],
})
export class StudentFormComponent implements OnInit {
  constructor(private studentService: StudentService) {}

  @Input()
  public declare studentInput: Student;

  @Output() closeFormEvent = new EventEmitter<boolean>();
  @Output() updateStudentTable = new EventEmitter<Student>();

  public isEditing: boolean = false;

  public studentForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [
      Validators.required,
      Validators.pattern('[a-zA-Z]*'),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.pattern('[a-zA-Z]*'),
    ]),
    mail: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('', [
      Validators.required,
      Validators.pattern('[+_0-9]*'),
    ]),
  });

  ngOnInit(): void {
    if (this.studentInput) {
      this.isEditing = true;
      this.InitFormForEditing();
    } else {
      this.isEditing = false;
    }
  }

  private InitFormForEditing(): void {
    this.studentForm.get('firstName')?.setValue(this.studentInput.firstName);
    this.studentForm.get('lastName')?.setValue(this.studentInput.lastName);
    this.studentForm.get('mail')?.setValue(this.studentInput.mail);
    this.studentForm.get('phone')?.setValue(this.studentInput.phone);
  }

  private setStudentForEdit(): void {
    this.studentInput.firstName = this.studentForm.get('firstName')?.value;
    this.studentInput.lastName = this.studentForm.get('lastName')?.value;
    this.studentInput.mail = this.studentForm.get('mail')?.value;
    this.studentInput.phone = this.studentForm.get('phone')?.value;
  }

  onSubmit() {
    if (this.isEditing) {
      this.setStudentForEdit();
      this.studentService.editStudent(this.studentInput).subscribe();
      this.closeForm();
    } else {
      this.studentService
        .createStudent(this.studentForm.value)
        .subscribe((res) => {
          this.updateStudentTable.emit(res);
          this.closeForm();
        });
    }
  }

  closeForm() {
    this.closeFormEvent.emit();
  }
}
