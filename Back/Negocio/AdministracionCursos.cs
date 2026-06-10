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
        private readonly ICategoriaRepository _repositorioCategoria;
        private readonly ISubCategoriaRepository _repositorioSubCategoria;
        private readonly ICostoRepository _repositorioCostos;
        public AdministracionCursos(ICursosRepository repositorioCursos, ICategoriaRepository repositorioCategoria, ISubCategoriaRepository repositorioSubCategoria, ICostoRepository repositorioCostos)
        {
            _repositorioCursos = repositorioCursos;
            _repositorioCategoria = repositorioCategoria;
            _repositorioSubCategoria = repositorioSubCategoria;
            _repositorioCostos = repositorioCostos;
        }

        //Categorias
        public async Task<GenericResponseDto> GetAllCategoriasAsync()
        {
            var categorias = await _repositorioCategoria.GetAllCategoryAsync();
            
            if (categorias == null)
                return new GenericResponseDto
                {
                    code = 404,
                    message = "No se encontraron categorías."
                };
            
            return new GenericResponseDto { code = 200, message = categorias };
        }

        public Task<GenericResponseDto> GetCategoriaByIdAsync(int id)
        {
            var categoria = _repositorioCategoria.GetCategoryByIdAsync(id);

            if (categoria == null)
                return Task.FromResult(new GenericResponseDto
                {
                    code = 404,
                    message = "Categoría no encontrada."
                });
            
            return Task.FromResult(new GenericResponseDto { code = 200, message = categoria });
        }

        public async Task<GenericResponseDto> AddCategoriaAsync(CursoRequestDto cursoRequest)
        {
            Entidades.Categoria categoria = new Entidades.Categoria
            {
                Nombre = cursoRequest.NameCategory
            };

            await _repositorioCategoria.AddAsync(categoria);

            return new GenericResponseDto { code = 200, message = "Categoría agregada exitosamente." };
        }

        public async Task<GenericResponseDto> UpdateCategoriaAsync(CursoRequestDto cursoRequest)
        {
            var categoriaExistente = await _repositorioCategoria.GetCategoryByIdAsync(cursoRequest.IdCategory);

            if (categoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Categoría no encontrada."
            };

            categoriaExistente.Nombre = cursoRequest.NameCategory ?? categoriaExistente.Nombre;
            categoriaExistente.UpdatedAt = DateTime.UtcNow;

            await _repositorioCategoria.UpdateAsync(categoriaExistente);

            return new GenericResponseDto { code = 200, message = "Categoría actualizada exitosamente." };
        }

        public async Task<GenericResponseDto> DeleteCategoriaAsync(int categoriaId)
        {
            var categoriaExistente = await _repositorioCategoria.GetCategoryByIdAsync(categoriaId);

            if (categoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Categoría no encontrada."
            };

            var subcategorias = await _repositorioSubCategoria.GetSubCategoryByCategoryIdAsync(categoriaId);

            if (subcategorias == null) return new GenericResponseDto
            {
                code = 404,
                message = "Categoría no encontrada."
            };

            foreach (var subcategoria in subcategorias)
            {
                var cursos = await _repositorioCursos.GetCursosBySubCategoriaIdAsync(subcategoria.Id);

                if (cursos != null)
                {
                    foreach (var curso in cursos)
                    {
                        await _repositorioCursos.DeleteAsync(curso);
                    }
                }
                await _repositorioSubCategoria.DeleteAsync(subcategoria);
            }
            
            await _repositorioCategoria.DeleteAsync(categoriaExistente);

            return new GenericResponseDto { code = 200, message = "Categoría eliminada exitosamente." };
        }

        //SubCategorias
        public async Task<GenericResponseDto> GetAllSubCategoriaAsync()
        {
            var subcategorias = await _repositorioSubCategoria.GetAllSubCategoryAsync();

            if (subcategorias == null) return new GenericResponseDto
            {
                code = 404,
                message = "No se encontraron subcategorías."
            };

            return new GenericResponseDto { code = 200, message = subcategorias };
        }

        public Task<GenericResponseDto> GetSubCategoriaByIdAsync(int id)
        {
            var subcategoria = _repositorioSubCategoria.GetSubCategoryByIdAsync(id);

            if (subcategoria == null)
                return Task.FromResult(new GenericResponseDto
                {
                    code = 404,
                    message = "Subcategoría no encontrada."
                });

            return Task.FromResult(new GenericResponseDto { code = 200, message = subcategoria });
        }

        public async Task<GenericResponseDto> AddSubCategoriaAsync(CursoRequestDto cursoRequest)
        {
            var categoriaExistente = await _repositorioCategoria.GetCategoryByIdAsync(cursoRequest.IdCategory);

            if (categoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Categoría no encontrada."
            };

            Entidades.SubCategoria subcategorias = new Entidades.SubCategoria
            {
                Nombre = cursoRequest.NameSubCategory,
                idCategoria = cursoRequest.IdCategory
            };

            await _repositorioSubCategoria.AddAsync(subcategorias);

            return new GenericResponseDto { code = 200, message = "Subcategoría agregada exitosamente." };
        }

        public async Task<GenericResponseDto> UpdateSubCategoriaAsync(CursoRequestDto cursoRequest)
        {
            var subcategoriaExistente = await _repositorioSubCategoria.GetSubCategoryByIdAsync(cursoRequest.IdSubCategory);

            if (subcategoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Subcategoría no encontrada."
            };

            if (!cursoRequest.IdSubCategory.Equals(0))
            {
                var categoriaExistente = _repositorioCategoria.GetCategoryByIdAsync(cursoRequest.IdCategory);

                if (categoriaExistente == null) return new GenericResponseDto
                {
                    code = 404,
                    message = "Categoría no encontrada."
                };
            }

            subcategoriaExistente.Nombre = cursoRequest.NameSubCategory ?? subcategoriaExistente.Nombre;
            subcategoriaExistente.idCategoria = cursoRequest.IdCategory == 0 ? subcategoriaExistente.idCategoria : cursoRequest.IdCategory;
            subcategoriaExistente.UpdatedAt = DateTime.UtcNow;

            await _repositorioSubCategoria.UpdateAsync(subcategoriaExistente);

            return new GenericResponseDto { code = 200, message = "Subcategoría actualizada exitosamente." };
        }

        public async Task<GenericResponseDto> DeleteSubCategoriaAsync(int subcategoriaId)
        {
            var subcategoriaExistente = await _repositorioSubCategoria.GetSubCategoryByIdAsync(subcategoriaId);

            if (subcategoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Subcategoría no encontrada."
            };

            var cursos = await _repositorioCursos.GetCursosBySubCategoriaIdAsync(subcategoriaId);

            if (cursos != null)
            {
                foreach (var curso in cursos)
                {
                    await _repositorioCursos.DeleteAsync(curso);
                }
            }

            await _repositorioSubCategoria.DeleteAsync(subcategoriaExistente);

            return new GenericResponseDto { code = 200, message = "Subcategoría eliminada exitosamente." };
        }

        //Cursos
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
        public async Task<GenericResponseDto> AddCursoAsync(CursoRequestDto cursoRequest)
        {
            Entidades.Cursos cursos = new Entidades.Cursos
            {
                Codigo = cursoRequest.CodeCourse,
                Nombre = cursoRequest.NameCourse,
                Descripcion = cursoRequest.DescriptionCourse,
                Vigencia = cursoRequest.Validity,
                idSubCategoria = cursoRequest.IdSubCategory,
                idCosto = cursoRequest.IdCost
            };

            await _repositorioCursos.AddAsync(cursos);

            return new GenericResponseDto { code = 200, message = "Curso agregado exitosamente." };
        }

        public async Task<GenericResponseDto> UpdateCursoAsync(CursoRequestDto cursoRequest)
        {
            var cursoExistente = await _repositorioCursos.GetCursoByIdAsync(cursoRequest.IdCourse);

            if (cursoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Curso no encontrado."
            };

            var subcategoriaExistente = await _repositorioSubCategoria.GetSubCategoryByIdAsync(cursoRequest.IdSubCategory);

            if (subcategoriaExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Subcategoría no encontrada."
            };

            var costoExistente = await _repositorioCostos.GetCostByIdAsync(cursoRequest.IdCost);

            if (costoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Costo no encontrado."
            };

            cursoExistente.Codigo = cursoRequest.CodeCourse ?? cursoExistente.Codigo;
            cursoExistente.Nombre = cursoRequest.NameCourse ?? cursoExistente.Nombre;
            cursoExistente.Descripcion = cursoRequest.DescriptionCourse ?? cursoExistente.Descripcion;
            cursoExistente.Vigencia = cursoRequest.Validity == 0 ? cursoExistente.Vigencia : cursoRequest.Validity;
            cursoExistente.idSubCategoria = cursoRequest.IdSubCategory == 0 ? cursoExistente.idSubCategoria : cursoRequest.IdSubCategory;
            cursoExistente.idCosto = cursoRequest.IdCost == 0 ? cursoExistente.idCosto : cursoRequest.IdCost;
            cursoExistente.UpdatedAt = DateTime.UtcNow;

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

        //Costos
        public async Task<GenericResponseDto> GetAllCostosAsync()
        {
            var costos = await _repositorioCostos.GetAllCostAsync();
            if (costos == null)
                return new GenericResponseDto
                {
                    code = 404,
                    message = "No se encontraron costos."
                };
            return new GenericResponseDto { code = 200, message = costos };
        }

        public Task<GenericResponseDto> GetCostoByIdAsync(int id)
        {
            var costo = _repositorioCostos.GetCostByIdAsync(id);
            if (costo == null)
                return Task.FromResult(new GenericResponseDto
                {
                    code = 404,
                    message = "Costo no encontrado."
                });
            return Task.FromResult(new GenericResponseDto { code = 200, message = costo });
        }
        public async Task<GenericResponseDto> AddCostoAsync(CursoRequestDto cursoRequest)
        {
            Entidades.Costos cursos = new Entidades.Costos
            {
                Codigo = cursoRequest.CodeCost,
                Valor = cursoRequest.ValueCost
            };

            await _repositorioCostos.AddAsync(cursos);

            return new GenericResponseDto { code = 200, message = "Costo agregado exitosamente." };
        }

        public async Task<GenericResponseDto> UpdateCostoAsync(CursoRequestDto cursoRequest)
        {
            var costoExistente = await _repositorioCostos.GetCostByIdAsync(cursoRequest.IdCost);

            if (costoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Costo no encontrado."
            };

            costoExistente.Codigo = cursoRequest.CodeCost ?? costoExistente.Codigo;
            costoExistente.Valor = cursoRequest.ValueCost == 0 ? costoExistente.Valor : cursoRequest.ValueCost;
            costoExistente.UpdatedAt = DateTime.UtcNow;

            await _repositorioCostos.UpdateAsync(costoExistente);

            return new GenericResponseDto { code = 200, message = "Costo actualizado exitosamente." };
        }

        public async Task<GenericResponseDto> DeleteCostoAsync(int costoId)
        {
            var costoExistente = await _repositorioCostos.GetCostByIdAsync(costoId);

            if (costoExistente == null) return new GenericResponseDto
            {
                code = 404,
                message = "Costo no encontrado."
            };

            var cursos = await _repositorioCursos.GetCursosByCostoIdAsync(costoId);

            if (cursos != null)
            {
                foreach (var curso in cursos)
                {
                    await _repositorioCursos.DeleteAsync(curso);
                }
            }

            await _repositorioCostos.DeleteAsync(costoExistente);

            return new GenericResponseDto { code = 200, message = "Costo eliminado exitosamente." };
        }
    }
}
