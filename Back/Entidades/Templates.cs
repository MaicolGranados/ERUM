using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Templates : EntidadBase
    {
        public int CursoId { get; set; }
        public Cursos Curso { get; set; } = null!;
        public string Html { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
    }
}
