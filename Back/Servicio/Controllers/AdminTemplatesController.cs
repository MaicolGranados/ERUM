using Microsoft.AspNetCore.Mvc;
using Aplicacion.DTOs;
using Aplicacion.Interfaces.Negocio;
using Microsoft.AspNetCore.Authorization;

namespace AcreditacionErumApi.Controllers
{
    [Authorize]
    public class AdminTemplatesController : Controller
    {
        private readonly IAdministracionPlantillas _administracionPlantillas;
        public AdminTemplatesController(IAdministracionPlantillas administracionPlantillas)
        {
            _administracionPlantillas = administracionPlantillas;
        }

        [HttpGet("api/GetTemplates")]
        public async Task<IActionResult> GetTemplates()
        {
            try
            {
                return Ok(await _administracionPlantillas.GetAllTemplatesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/CreateTemplate")]
        public async Task<IActionResult> CreateTemplate([FromBody] TemplateRequestDto templateRequest)
        {
            try
            {
                return Ok(await _administracionPlantillas.AddTemplateAsync(templateRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/SaveBackground")]
        public async Task<IActionResult> SaveBackground(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No se recibió archivo.");
                }

                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "templates");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new GenericResponseDto
                {
                    code = 200,
                    message = new
                    {
                        FileName = fileName,
                        Url = $"/templates/{fileName}"
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/DeleteTemplate")]
        public async Task<IActionResult> DeleteTemplate([FromBody] TemplateRequestDto templateRequest)
        {
            try
            {
                return Ok(await _administracionPlantillas.DeleteTemplateAsync(templateRequest.IdTemplate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("api/UpdateTemplate")]
        public async Task<IActionResult> UpdateTemplate([FromBody] TemplateRequestDto templateRequest)
        {
            try
            {
                return Ok(await _administracionPlantillas.UpdateTemplateAsync(templateRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
