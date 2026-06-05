import { Component, inject } from '@angular/core';
import { RouterOutlet,RouterLink } from "@angular/router";
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout{

  private authService = inject(AuthService)
  private router = inject(Router);

  userName: string = '';

  isSidebarOpen = false;

  ngOnInit(): void {
    const userInfo = this.authService.getUserInfo();
    this.userName = userInfo['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  }

  toggleSidebar(): void {

    this.isSidebarOpen = !this.isSidebarOpen;

  }

  closeSidebar(): void {

    this.isSidebarOpen = false;

  }

  logout(): void {

    this.authService
      .logout();

    this.router.navigate(['/']);
  }
}
