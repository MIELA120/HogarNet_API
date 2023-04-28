using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogarnet.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }

        public int? IdCategoria { get; set; }

        public int? IdMarca { get; set; }
        
        public string? DescripcionCategoria { get; set; }

        public string? DescripcionMarca { get; set; }

        public int? Stock { get; set; }

        public string? Precio { get; set; }

        public string? RutaImagen { get; set; }

        public string? NombreImagen { get; set; }

        public int? IdEstado { get; set; }

    }
}
