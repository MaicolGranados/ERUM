using Aplicacion.DTOs;
using Aplicacion.Interfaces.Negocio;
using Aplicacion.Interfaces.Repositorio;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class AdministracionUsuario : IAdministracionUsuario
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolRepository _rolRepository;
        private readonly Configuracion _configuracion;
        public AdministracionUsuario(IUserRepository userRepository, IOptions<Configuracion> configuracion, IRolRepository rolRepository)
        {
            _userRepository = userRepository;
            _configuracion = configuracion.Value;
            _rolRepository = rolRepository;
        }
        public async Task<GenericResponseDto> CreateUser(UsuarioRequestDto userRequest)
        {
            if (userRequest == null) return new GenericResponseDto
            {
                code = 400,
                message = "El objeto userRequest no puede ser nulo."
            };

            var userExistente = await _userRepository.GetUserByNameAsync(userRequest.Name);

            if (userExistente != null) return new GenericResponseDto
            {
                code = 400,
                message = "El usuario ingresado ya existe."
            };

            var rolExistente = await _rolRepository.GetByIdAsync(userRequest.IdRol);

            if (rolExistente == null) return new GenericResponseDto
            {
                code = 400,
                message = "El rol ingresado no existe."
            };

            var user = new User
            {
                Username = userRequest.Name,
                Email = userRequest.Email,
                RolesId = userRequest.IdRol,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(_configuracion.DefaultPassword),
                IsActive = true
            };

            await _userRepository.AddAsync(user);

            return new GenericResponseDto
            {
                code = 200,
                message = "Usuario creado exitosamente."
            };
        }

        public async Task<GenericResponseDto> DeleteUser(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return new GenericResponseDto
            {
                code = 400,
                message = "Usuario no encontrado."
            };

            await _userRepository.DeleteAsync(user);

            return new GenericResponseDto
            {
                code = 200,
                message = "Usuario eliminado exitosamente."
            };
        }

        public async Task<GenericResponseDto> GetAllUsers()
        {
            var user = await _userRepository.GetAllUsersAsync();

            if (user == null) return new GenericResponseDto
            {
                code = 400,
                message = "No se encontraron usuarios."
            };

            user.ForEach(u => { u.PasswordHash = ""; });

            return new GenericResponseDto
            {
                code = 200,
                message = user
            };
        }

        public async Task<GenericResponseDto> UpdateUser(UsuarioRequestDto userRequest)
        {
            if (userRequest == null) return new GenericResponseDto
            {
                code = 400,
                message = "El request no puede ser nulo."
            };

            var userExistente = await _userRepository.GetUserByIdAsync(userRequest.IdUser);
            if (userExistente == null) return new GenericResponseDto
            {
                code = 400,
                message = "El usuario ingresado no existe."
            };

            var rolExistente = await _rolRepository.GetByIdAsync(userRequest.IdRol);
            if (rolExistente == null) return new GenericResponseDto
            {
                code = 400,
                message = "El rol ingresado no existe."
            };

            userExistente.Email = userRequest.Email;
            userExistente.RolesId = userRequest.IdRol;
            userExistente.IsActive = userRequest.Activo;
            userExistente.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(userExistente);

            return new GenericResponseDto
            {
                code = 200,
                message = "Usuario actualizado exitosamente."
            };
        }

        public async Task<GenericResponseDto> ResetPassword(int userId, string password)
        {
            var userExistente = await _userRepository.GetUserByIdAsync(userId);
            if (userExistente == null) return new GenericResponseDto
            {
                code = 400,
                message = "El usuario ingresado no existe."
            };

            string newPassword = string.IsNullOrEmpty(password) ? _configuracion.DefaultPassword : password;
            
            userExistente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(userExistente);

            return new GenericResponseDto
            {
                code = 200,
                message = "Contraseña restablecida exitosamente."
            };
        }
    }
}
