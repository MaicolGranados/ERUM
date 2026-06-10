using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class CursoRequestDto
    {
        //Cursos
        public int IdCourse { get; set; }
        public string CodeCourse { get; set; } = null!;
        public string NameCourse { get; set; } = null!;
        public string DescriptionCourse { get; set; } = null!;
        public int Validity { get; set; }

        //SubCategorias
        public int IdSubCategory { get; set; }
        public string NameSubCategory { get; set; } = null!;

        //Categorias
        public int IdCategory { get; set; }
        public string NameCategory { get; set; } = null!;

        //Costos
        public int IdCost { get; set; }
        public string CodeCost { get; set; } = null!;
        public double ValueCost { get; set; }
    }
}
