import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { environment } from '../../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private http = inject(HttpClient);
  private authService = inject(AuthService);

  private apiUrl = environment.apiUrl;

  getUsers(): Observable<any> {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get(`${this.apiUrl}/api/GetUsers`, { headers });
  }

  createUser(request: any){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post(
      `${this.apiUrl}/api/CreateUser`,
      request,
      { headers }
    );
  }

  resetPassword(idUser: number){
    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/ResetPassword?userId=${idUser}`,
      {
        headers
      }
    );
  }

  deleteUser(idUser: number) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete(
      `${this.apiUrl}/api/DeleteUser`,
      {
        headers,
        body: {
          IdUser: idUser
        }
      }
    );
  }

  updateUser(request: any) {

    const token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.patch(
      `${this.apiUrl}/api/UpdateUser`,
      request,
      { headers }
    );
}
}
