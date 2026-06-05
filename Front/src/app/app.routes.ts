import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Layout } from './features/admin/layout/layout';
import { Users } from './features/admin/users/users';
import { Courses } from './features/admin/courses/courses';
import { Templates } from './features/admin/templates/templates';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: Login
  },
  {
    path: 'admin',
    component: Layout,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'users',
        pathMatch: 'full'
      },
      {
        path: 'users',
        component: Users
      },
      {
        path: 'courses',
        component: Courses
      },
      {
        path: 'templates',
        component: Templates
      }
    ]
  }
];
