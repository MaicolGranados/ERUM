import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  private apiUrl = environment.apiUrl;

  login(UserName: string, Password: string): Observable<any> {

    return this.http.post<any>(`${this.apiUrl}/api/auth`, {
      UserName,
      Password
    }).pipe(
      tap(response => {

        localStorage.setItem('token', response.token);

      })
    );

  }

  getToken(): string | null {

    return localStorage.getItem('token');

  }

  logout(): void {

    localStorage.removeItem('token');

  }

  getUserInfo(): any {

    const token = this.getToken();

    if (!token) return null;

    return jwtDecode(token);

  }

}
