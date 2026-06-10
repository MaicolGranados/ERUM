import { Component, OnInit, inject } from '@angular/core';
import { UsersService } from '../../../core/services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule,FormsModule,MatTooltipModule],
  templateUrl: './users.html'
})
export class Users implements OnInit {

  private usersService = inject(UsersService);

  users: any[] = [];
  filteredUsers: any[] = [];

  searchText: string = '';

  editingUserId: number | null = null;

  roles = [
    { id: 1, nombre: 'Administrator' },
    { id: 2, nombre: 'Operator' },
    { id: 3, nombre: 'User' }
  ];


  editModel = {
    idUser: 0,
    email: '',
    idRol: 0,
    activo: false
  };

  createModel = {
    username: '',
    email: '',
    idRol: 1
  };

  ngOnInit(): void {
    this.loadUsers();
  }

  showCreateModal = false;

  newUser(): void {

    this.showCreateModal = true;

  }

  closeModal(): void {

    this.showCreateModal = false;

  }

  saveNewUser(): void {

  console.log(this.createModel);

  const request = {
      Name: this.createModel.username,
      Email: this.createModel.email,
      IdRol: this.createModel.idRol
      };

    this.usersService.createUser(request)
      .subscribe({
        next: () => {

          this.showCreateModal = false;
          this.loadUsers();

          Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Usuario creado exitosamente.',
            confirmButtonColor: '#fea419'
          });

        },
        error: err => {

          console.error(err);

        }
      });



  }

  loadUsers(): void {

    this.usersService.getUsers().subscribe({
      next: (response) => {

        this.users = response.message;
        this.filteredUsers = [...this.users];

      },
      error: (err) => {

        console.error(err);

      }
    });

  }

  filterUsers(): void {

    const search = this.searchText.toLowerCase().trim();

    if (!search) {

      this.filteredUsers = [...this.users];
      return;

    }

    this.filteredUsers = this.users.filter(user =>
      user.username?.toLowerCase().includes(search) ||
      user.email?.toLowerCase().includes(search) ||
      user.roles?.nameRole?.toLowerCase().includes(search)
    );

  }

  editUser(user: any): void {

    this.editingUserId = user.id;

    this.editModel = {
      idUser: user.id,
      email: user.email.trim(),
      idRol: user.rolesId,
      activo: user.isActive
    };

  }

  cancelEdit(): void {

    this.editingUserId = null;

  }

  saveUser(): void {

    const request = {
      IdUser: this.editModel.idUser,
      Email: this.editModel.email,
      IdRol: this.editModel.idRol,
      Activo: this.editModel.activo
    };

    this.usersService.updateUser(request)
      .subscribe({
        next: () => {

          this.editingUserId = null;

          this.loadUsers();

        },
        error: err => {

          console.error(err);

        }
      });
  }

  resetPassword(id: number): void{

    Swal.fire({
    title: '¿Desea restablecer la clave del usuario?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Sí, restablecer',
    cancelButtonText: 'Cancelar'
    }).then(result => {

      if (result.isConfirmed) {


        this.usersService.resetPassword(id)
          .subscribe({
            next: () => {

              Swal.fire(
                  'Restablecido',
                  'Clave restablecida correctamente',
                  'success'
                );

                this.loadUsers();

            },
            error: err => {

              console.error(err);

            }
          });

      }

    });

  }

  deleteUser(id: number): void {

     Swal.fire({
    title: '¿Desea eliminar este usuario?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Sí, eliminar',
    cancelButtonText: 'Cancelar'
    }).then(result => {

      if (result.isConfirmed) {

        this.usersService.deleteUser(id)
        .subscribe({
          next: () => {

            Swal.fire(
              'Eliminado',
              'Usuario eliminado correctamente',
              'success'
            );

            this.loadUsers();

          },
          error: err => {

            console.error(err);

          }
        });

      }

    });

  }
}
