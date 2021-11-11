import { HttpClientModule } from '@angular/common/http';
import { createComponentDefinitionMap } from '@angular/compiler/src/render3/partial/component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { Student } from 'src/models/student/student';
import { StudentService } from 'src/services/student.service';

import { StudentsComponent } from './students.component';

describe('StudentsComponent', () => {
  let component: StudentsComponent;
  let fixture: ComponentFixture<StudentsComponent>;

  let students: Array<Student>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudentsComponent],
      providers: [StudentService],
      imports: [HttpClientModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    students = [
      {
        id: '1',
        firstName: 'first name 1',
        lastName: 'last name 1',
        mail: 'email 1',
        phone: 'phone 1',
      },
      {
        id: '2',
        firstName: 'first name 2',
        lastName: 'last name 2',
        mail: 'email 2',
        phone: 'phone 2',
      },
      {
        id: '3',
        firstName: 'first name 3',
        lastName: 'last name 3',
        mail: 'email 3',
        phone: 'phone 3',
      },
    ];
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('fetch data', () => {
    spyOn(component.studentService, 'getAllStudents').and.callFake(() => {
      return of(students);
    });

    component.ngOnInit();
    expect(component.students.length).toEqual(3);
    expect(component.students).toEqual(students);
    expect(component.students[0].id).toEqual('1');
  });

  it('add Student', () => {
    spyOn(component.studentService, 'getAllStudents').and.callFake(() => {
      return of(students);
    });

    let studentToAdd: Student = {
      id: '4',
      firstName: 'first name 4',
      lastName: 'last name 4',
      mail: 'email 4',
      phone: 'phone 4',
    };

    component.ngOnInit();
    component.AddStudent(studentToAdd);

    expect(component.students.length).toEqual(4);
    expect(component.students[3]).toEqual(studentToAdd);
  });

  it('Delete Student', () => {
    spyOn(component.studentService, 'getAllStudents').and.callFake(() => {
      return of(students);
    });

    let studentToDelete: Student = students[students.length - 1];
    spyOn(component.studentService, 'deleteStudent').and.callFake(() => {
      return of(studentToDelete);
    });

    component.ngOnInit();
    component.deleteStudent(studentToDelete.id);
    expect(component.students.length).toEqual(2);
    expect(component.students.indexOf(studentToDelete)).toEqual(-1);
  });
});
