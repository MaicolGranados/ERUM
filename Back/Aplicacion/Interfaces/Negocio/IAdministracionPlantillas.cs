using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Negocio
{
    public interface IAdministracionPlantillas
    {
        Task<GenericResponseDto> GetAllTemplatesAsync();
        Task<GenericResponseDto> AddTemplateAsync(TemplateRequestDto templateRequest);
        Task<GenericResponseDto> UpdateTemplateAsync(TemplateRequestDto templateRequest);
        Task<GenericResponseDto> DeleteTemplateAsync(int templateId);
    }
}
