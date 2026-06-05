using Aplicacion.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Negocio
{
    public interface IAuth
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
