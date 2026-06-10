using Aplicacion;
using Aplicacion.DTOs.Auth;
using Aplicacion.Interfaces.Repositorio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.DTOs;
using Microsoft.Extensions.Options;
using Aplicacion.Interfaces.Negocio;

namespace Negocio
{
    public class Auth : IAuth
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolRepository _role;
        private readonly Configuracion _configuracion;
        public Auth(IUserRepository userRepository,  IOptions<Configuracion> configuracion, IRolRepository role)
        {
            _userRepository = userRepository;
            _configuracion = configuracion.Value;
            _role = role;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository
                .GetUserByNameAsync(
                    request.Username
                );

            if (user == null)
                return null;

            user.LastLogin = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            if (!user.IsActive)
                return null;

            if (!ValidatePasswordHash(request.Password, user.PasswordHash))
                return null;

            var role = await _role.GetByIdAsync(user.RolesId);

            if (role == null)
                return null;

            user.Roles = role;

            return new LoginResponseDto
            {
                Token = GenerateJWTToken(user),
                Expiration = DateTime.Now.AddMinutes(15)
            };
        }

        private bool ValidatePasswordHash(string password, string passwordHash)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return isValid;
        }

        private string GenerateJWTToken(Entidades.User user)
        {
            var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuracion.SecretKey)
                    );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Roles.NameRole),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuracion.JwtIssuer,
                audience: _configuracion.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
