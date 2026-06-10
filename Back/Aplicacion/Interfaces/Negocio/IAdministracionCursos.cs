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
        //Categorias
        Task<GenericResponseDto> GetAllCategoriasAsync();
        Task<GenericResponseDto> GetCategoriaByIdAsync(int id);
        Task<GenericResponseDto> AddCategoriaAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> UpdateCategoriaAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> DeleteCategoriaAsync(int categoriaId);

        //SubCategorias
        Task<GenericResponseDto> GetAllSubCategoriaAsync();
        Task<GenericResponseDto> GetSubCategoriaByIdAsync(int id);
        Task<GenericResponseDto> AddSubCategoriaAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> UpdateSubCategoriaAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> DeleteSubCategoriaAsync(int subcategoriaId);

        //Cursos
        Task<GenericResponseDto> GetAllCursosAsync();
        Task<GenericResponseDto> GetCursoByIdAsync(int id);
        Task<GenericResponseDto> AddCursoAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> UpdateCursoAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> DeleteCursoAsync(int cursoId);

        //Costos
        Task<GenericResponseDto> GetAllCostosAsync();
        Task<GenericResponseDto> GetCostoByIdAsync(int id);
        Task<GenericResponseDto> AddCostoAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> UpdateCostoAsync(CursoRequestDto cursoRequest);
        Task<GenericResponseDto> DeleteCostoAsync(int costoId);

    }
}
