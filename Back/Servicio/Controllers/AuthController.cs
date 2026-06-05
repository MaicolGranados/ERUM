using Aplicacion.DTOs.Auth;
using Aplicacion.Interfaces.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcreditacionErumApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("api/auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var result = await _auth.LoginAsync(request);

                if (result == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
