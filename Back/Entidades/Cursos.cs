using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cursos : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Vigencia { get; set; }
        public int idSubCategoria { get; set; }
        public SubCategoria SubCategoria { get; set; } = null!;
        public int idCosto { get; set; }
        public Costos Costos { get; set; } = null!;
    }
}
