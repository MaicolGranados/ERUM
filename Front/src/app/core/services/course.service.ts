import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { environment } from '../../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private http = inject(HttpClient);
  private authService = inject(AuthService);

  private apiUrl = environment.apiUrl;

  getCourses(): Observable<any> {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get(`${this.apiUrl}/api/GetCourses`, { headers });
  }

  createCourse(request: any){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post(
      `${this.apiUrl}/api/CreateCourse`,
      request,
      { headers }
    );
  }

  deleteCourse(idCourse: number) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete(
      `${this.apiUrl}/api/DeleteCourse`,
      {
        headers,
        body: {
          IdCourse: idCourse
        }
      }
    );
  }

  updateCourse(request: any) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/UpdateCourse`,
      request,
      { headers }
    );
}
}
