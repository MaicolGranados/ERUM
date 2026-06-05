using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Negocio
{
    public interface IAdministracionUsuario
    {
        Task<GenericResponseDto> CreateUser(UserRequestDto userRequest);
        Task<GenericResponseDto> DeleteUser(int userId);
        Task<GenericResponseDto> GetAllUsers();
        Task<GenericResponseDto> UpdateUser(UserRequestDto userRequest);
        Task<GenericResponseDto> ResetPassword(int userId, string password);
    }
}
