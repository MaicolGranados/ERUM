using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class GenericResponseDto
    {
        public int code { get; set; }
        public object message { get; set; } = string.Empty;
    }
}
