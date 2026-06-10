using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Costos : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public double Valor { get; set; }
    }
}
