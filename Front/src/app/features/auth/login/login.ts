import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
})
export class Login {
  showLogin = false;
  private authService = inject(AuthService);
  private router = inject(Router);

  user = '';
  password = '';

  login(): void {
    this.authService.login(this.user, this.password).subscribe({
      next: (response) => {
        console.log(response);

        this.router.navigate(['/admin']);
      },

      error: (error) => {
        console.error(error);

        alert('Credenciales inválidas');
      },
    });
  }
}
