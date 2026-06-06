using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces.Negocio;
using Aplicacion.Interfaces.Repositorio;

namespace Negocio
{
    public class AdministracionCursos : IAdministracionCursos
    {
        private readonly ICursosRepository _repositorioCursos;
        public AdministracionCursos(ICursosRepository repositorioCursos)
        {
            _repositorioCursos = repositorioCursos;
        }

        public async Task<GenericResponseDto> GetAllCursosAsync()
        {
            var cursos = await _repositorioCursos.GetAllCursosAsync();
            if (cursos == null)
                return new GenericResponseDto
                {
                    code = 404,
                    message = "No se encontraron cursos."
                };
            return new GenericResponseDto { code = 200, message = cursos };
        }
        public Task<GenericResponseDto> GetCursoByIdAsync(int id)
        {
            var curso = _repositorioCursos.GetCursoByIdAsync(id);
            if (curso == null)
                return Task.FromResult(new GenericResponseDto
                {
                    code = 404,
                    message = "Curso no encontrado."
                });
            return Task.FromResult(new GenericResponseDto { code = 200, message = curso });
        }
        public async Task<GenericResponseDto> AddCursoAsync(CourseRequestDto cursoRequest)
        {
            Entidades.Cursos cursos = new Entidades.Cursos
            {
                Codigo = cursoRequest.Code,
                Nombre = cursoRequest.Name,
                Descripcion = cursoRequest.Description,
                Vigencia = cursoRequest.Validity,
                Costo = cursoRequest.Cost
            };

            await _repositorioCursos.AddAsync(cursos);
            
            return new GenericResponseDto { code = 200, message = "Curso agregado exitosamente." };
        }

        public async Task<GenericResponseDto> UpdateCursoAsync(CourseRequestDto cursoRequest)
        {
            var cursoExistente = await _repositorioCursos.GetCursoByIdAsync(cursoRequest.IdCourse);

            if (cursoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Curso no encontrado."
            };

            cursoExistente.Codigo = cursoRequest.Code ?? cursoExistente.Codigo;
            cursoExistente.Nombre = cursoRequest.Name ?? cursoExistente.Nombre;
            cursoExistente.Descripcion = cursoRequest.Description ?? cursoExistente.Descripcion;
            cursoExistente.Vigencia = cursoRequest.Validity == 0 ? cursoExistente.Vigencia : cursoRequest.Validity;
            cursoExistente.Costo = cursoRequest.Cost == 0 ? cursoExistente.Costo : cursoRequest.Cost;
            await _repositorioCursos.UpdateAsync(cursoExistente);

            return new GenericResponseDto { code = 200, message = "Curso actualizado exitosamente." };
        }

        public async Task<GenericResponseDto> DeleteCursoAsync(int cursoId)
        {
            var cursoExistente = await _repositorioCursos.GetCursoByIdAsync(cursoId);

            if (cursoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Curso no encontrado."
            };

            await _repositorioCursos.DeleteAsync(cursoExistente);

            return new GenericResponseDto { code = 200, message = "Curso eliminado exitosamente." };
        }
    }
}
