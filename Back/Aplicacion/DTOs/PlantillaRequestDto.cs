using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class PlantillaRequestDto
    {
        public int IdTemplate { get; set; }
        public int IdCourse { get; set; }
        public string Html { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
    }
}
