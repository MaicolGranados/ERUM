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

        public async Task<GenericResponseDto> AddTemplateAsync(TemplateRequestDto templateRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponseDto> UpdateTemplateAsync(TemplateRequestDto templateRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponseDto> DeleteTemplateAsync(int templateId)
        {
            throw new NotImplementedException();
        }
    }
}
