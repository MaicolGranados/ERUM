using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces.Negocio;
using Aplicacion.Interfaces.Repositorio;

namespace Negocio
{
    public class AdministracionPlantillas : IAdministracionPlantillas
    {
        private readonly ITemplateRepository _templateRepository;
        public AdministracionPlantillas(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }
        public async Task<GenericResponseDto> GetAllTemplatesAsync()
        {
            var templates = await _templateRepository.GetAllTemplateAsync();

            if (templates == null)
                return new GenericResponseDto
                {
                    code = 404,
                    message = "No se encontraron plantillas."
                };

            return new GenericResponseDto { code = 200, message = templates };
        }

        public async Task<GenericResponseDto> AddTemplateAsync(PlantillaRequestDto templateRequest)
        {


            Entidades.Templates templates = new Entidades.Templates();
            
            templates.CursoId = templateRequest.IdCourse;
            templates.Html = templateRequest.Html;
            templates.Imagen = templateRequest.Imagen;

            await _templateRepository.AddAsync(templates);

            return new GenericResponseDto
            {
                code = 201,
                message = "Plantilla agregada exitosamente."
            };
        }

        public async Task<GenericResponseDto> UpdateTemplateAsync(PlantillaRequestDto templateRequest)
        {
            var template = await _templateRepository.GetTemplateByIdAsync(templateRequest.IdTemplate);
            if (template == null)
            {
                return new GenericResponseDto
                {
                    code = 404,
                    message = "Plantilla no encontrada."
                };
            }

            template.CursoId = templateRequest.IdCourse;
            template.Html = templateRequest.Html;
            template.Imagen = templateRequest.Imagen;
            template.UpdatedAt = DateTime.UtcNow;

            await _templateRepository.UpdateAsync(template);

            return new GenericResponseDto
            {
                code = 200,
                message = "Plantilla actualizada exitosamente."
            };
        }

        public async Task<GenericResponseDto> DeleteTemplateAsync(int templateId)
        {
            var template = await _templateRepository.GetTemplateByIdAsync(templateId);
            if (template == null)
            {
                return new GenericResponseDto
                {
                    code = 404,
                    message = "Plantilla no encontrada."
                };
            }

            await _templateRepository.DeleteAsync(template);

            return new GenericResponseDto
            {
                code = 200,
                message = "Plantilla eliminada exitosamente."
            };
        }
    }
}
