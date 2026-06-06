using Aplicacion.DTOs;
using Aplicacion.Interfaces.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcreditacionErumApi.Controllers
{
    [Authorize]
    public class AdminCoursesController : Controller
    {
        private readonly IAdministracionCursos _administracionCursos;
        public AdminCoursesController(IAdministracionCursos administracionCursos)
        {
            _administracionCursos = administracionCursos;
        }

        [HttpGet("api/GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                return Ok(await _administracionCursos.GetAllCursosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.AddCursoAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteCourse")]
        public async Task<IActionResult> DeleteCourse([FromBody] CourseRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.DeleteCursoAsync(courseRequest.IdCourse));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.UpdateCursoAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
