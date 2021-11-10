import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from 'src/models/student/student';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  constructor(private http: HttpClient) {}
  private baseUrl: string = 'https://localhost:44317/api/student/';

  public getAllStudents(): Observable<Array<Student>> {
    return this.http.get<Array<Student>>(this.baseUrl);
  }

  public createStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(this.baseUrl, student);
  }

  public deleteStudent(id: string): Observable<Student> {
    return this.http.delete<Student>(this.baseUrl + id);
  }

  public editStudent(student: Student): Observable<Student> {
    return this.http.put<Student>(this.baseUrl + student.id, student);
  }
}
