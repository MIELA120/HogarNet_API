using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogarnet.DTO
{
    //Clases que tendrán interacción con Angular
    public class MenuDTO
    {

        public int IdMenu { get; set; }

        public string? NombreMenu { get; set; }

        public string? Icono { get; set; }

        public string? UrlMenu { get; set; }

    }
}
