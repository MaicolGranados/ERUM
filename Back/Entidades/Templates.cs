using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Templates : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public double Costo { get; set; }
        public DateTime FechaVigencia { get; set; }
    }
}
