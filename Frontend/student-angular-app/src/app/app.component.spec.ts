import { TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { Student } from 'src/models/student/student';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let student: Student;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [AppComponent],
    }).compileComponents();

    student = {
      id: '4',
      firstName: 'first name added',
      lastName: 'last name added',
      mail: 'email added',
      phone: 'phone added',
    };
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('student table should be visible', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const { debugElement } = fixture;
    const student = debugElement.query(By.css('app-students'));

    expect(student).toBeTruthy();
  });

  it('student form should be visible when add button is clicked', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const { debugElement } = fixture;

    const student = debugElement.query(By.css('app-students'));
    student.componentInstance.addStudent();
    fixture.detectChanges();

    const studentForm = debugElement.query(By.css('app-student-form'));
    expect(studentForm).toBeTruthy();
  });

  it('student form should be visible when edit button is clicked', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const { debugElement } = fixture;

    const studentComponent = debugElement.query(By.css('app-students'));
    studentComponent.componentInstance.editStudent(student);
    fixture.detectChanges();

    const studentForm = debugElement.query(By.css('app-student-form'));
    expect(studentForm).toBeTruthy();
  });

  it('open and close form component', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const { debugElement } = fixture;

    const studentComponent = debugElement.query(By.css('app-students'));
    studentComponent.componentInstance.addStudent();
    fixture.detectChanges();

    const studentForm = debugElement.query(By.css('app-student-form'));
    expect(studentForm).toBeTruthy();

    studentForm.componentInstance.closeForm();
    fixture.detectChanges();

    const studentFormClosed = debugElement.query(By.css('app-student-form'));
    expect(studentFormClosed).toBeFalsy();
  });
});
