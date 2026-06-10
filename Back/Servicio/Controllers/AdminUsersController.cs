using Aplicacion.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Interfaces.Negocio;

namespace AcreditacionErumApi.Controllers
{
    [Authorize]
    public class AdminUsersController : Controller
    {
        private readonly IAdministracionUsuario _administracionUsuario;
        public AdminUsersController(IAdministracionUsuario administracionUsuario)
        {
            _administracionUsuario = administracionUsuario;
        }

        [HttpGet("api/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _administracionUsuario.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UsuarioRequestDto userRequest)
        {
            try
            {
                return Ok(await _administracionUsuario.CreateUser(userRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] UsuarioRequestDto userRequest)
        {
            try
            {
                return Ok(await _administracionUsuario.DeleteUser(userRequest.IdUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UsuarioRequestDto userRequest)
        {
            try
            {
                return Ok(await _administracionUsuario.UpdateUser(userRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/ResetPassword")]
        public async Task<IActionResult> ResetPassword(int userId, string password = "")
        {
            try
            {
                return Ok(await _administracionUsuario.ResetPassword(userId, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
