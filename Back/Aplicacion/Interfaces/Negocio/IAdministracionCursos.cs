using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;

namespace Aplicacion.Interfaces.Negocio
{
    public interface IAdministracionCursos
    {
        Task<GenericResponseDto> GetAllCursosAsync();
        Task<GenericResponseDto> GetCursoByIdAsync(int id);
        Task<GenericResponseDto> AddCursoAsync(CourseRequestDto cursoRequest);
        Task<GenericResponseDto> UpdateCursoAsync(CourseRequestDto cursoRequest);
        Task<GenericResponseDto> DeleteCursoAsync(int cursoId);
    }
}
