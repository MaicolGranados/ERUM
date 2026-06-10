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

//Categorias

  getCategories(): Observable<any> {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get(`${this.apiUrl}/api/GetCategories`, { headers });
  }

  createCategory(request: any){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post(
      `${this.apiUrl}/api/CreateCategory`,
      request,
      { headers }
    );
  }

  deleteCategory(idCategory: number) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete(
      `${this.apiUrl}/api/DeleteCategory`,
      {
        headers,
        body: {
          IdCategory: idCategory
        }
      }
    );
  }

  updateCategory(request: any) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/UpdateCategory`,
      request,
      { headers }
    );
  }

//SubCategorias

  getSubCategories(): Observable<any> {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get(`${this.apiUrl}/api/GetSubCategories`, { headers });
  }

  createSubCategory(request: any){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post(
      `${this.apiUrl}/api/CreateSubCategory`,
      request,
      { headers }
    );
  }

  deleteSubCategory(idSubCategory: number) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete(
      `${this.apiUrl}/api/DeleteSubCategory`,
      {
        headers,
        body: {
          IdSubCategory: idSubCategory
        }
      }
    );
  }

  updateSubCategory(request: any) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/UpdateSubCategory`,
      request,
      { headers }
    );
  }


//Costos

  getCosts(): Observable<any> {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get(`${this.apiUrl}/api/GetCosts`, { headers });
  }

  createCost(request: any){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post(
      `${this.apiUrl}/api/CreateCost`,
      request,
      { headers }
    );
  }

  deleteCost(idCost: number) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete(
      `${this.apiUrl}/api/DeleteCost`,
      {
        headers,
        body: {
          IdCost: idCost
        }
      }
    );
  }

  updateCost(request: any) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/UpdateCost`,
      request,
      { headers }
    );
  }

//Cursos

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
