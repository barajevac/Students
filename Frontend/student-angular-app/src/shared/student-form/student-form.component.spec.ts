import { HttpClientModule } from '@angular/common/http';
import { expressionType } from '@angular/compiler/src/output/output_ast';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StudentService } from 'src/services/student.service';

import { StudentFormComponent } from './student-form.component';

describe('StudentFormComponent', () => {
  let component: StudentFormComponent;
  let fixture: ComponentFixture<StudentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudentFormComponent],
      providers: [StudentService],
      imports: [HttpClientModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('set form to edit mode', () => {
    component.studentInput = {
      id: '1',
      firstName: 'first name 1',
      lastName: 'last name 1',
      mail: 'email 1',
      phone: 'phone 1',
    };

    component.ngOnInit();
    expect(component.isEditing).toEqual(true);
    expect(component.studentForm.get('firstName')?.value).toEqual(
      'first name 1'
    );
    expect(component.studentForm.get('lastName')?.value).toEqual('last name 1');
    expect(component.studentForm.get('mail')?.value).toEqual('email 1');
    expect(component.studentForm.get('phone')?.value).toEqual('phone 1');
  });

  it('set form to add mode', () => {
    component.ngOnInit();
    expect(component.isEditing).toEqual(false);
    expect(component.studentForm.get('firstName')?.value).toEqual('');
    expect(component.studentForm.get('lastName')?.value).toEqual('');
    expect(component.studentForm.get('mail')?.value).toEqual('');
    expect(component.studentForm.get('phone')?.value).toEqual('');
  });

  it('close', () => {
    expect(fixture.componentInstance.closeForm()).toBeFalsy();
  });
});
