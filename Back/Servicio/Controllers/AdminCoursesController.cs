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

        //Categorias

        [HttpGet("api/GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _administracionCursos.GetAllCategoriasAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.AddCategoriaAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.DeleteCategoriaAsync(courseRequest.IdCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.UpdateCategoriaAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //SubCategorias

        [HttpGet("api/GetSubCategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            try
            {
                return Ok(await _administracionCursos.GetAllSubCategoriaAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.AddSubCategoriaAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteSubCategory")]
        public async Task<IActionResult> DeleteSubCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.DeleteSubCategoriaAsync(courseRequest.IdSubCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateSubCategory")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.UpdateSubCategoriaAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Cursos
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
        public async Task<IActionResult> CreateCourse([FromBody] CursoRequestDto courseRequest)
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
        public async Task<IActionResult> DeleteCourse([FromBody] CursoRequestDto courseRequest)
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
        public async Task<IActionResult> UpdateCourse([FromBody] CursoRequestDto courseRequest)
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

        //Costos

        [HttpGet("api/GetCosts")]
        public async Task<IActionResult> GetCosts()
        {
            try
            {
                return Ok(await _administracionCursos.GetAllCostosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateCost")]
        public async Task<IActionResult> CreateCost([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.AddCostoAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteCost")]
        public async Task<IActionResult> DeleteCost([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.DeleteCostoAsync(courseRequest.IdCost));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateCost")]
        public async Task<IActionResult> UpdateCost([FromBody] CursoRequestDto courseRequest)
        {
            try
            {
                return Ok(await _administracionCursos.UpdateCostoAsync(courseRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
