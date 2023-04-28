using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdMarca { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public string? RutaImagen { get; set; }

    public string? NombreImagen { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Carrito> Carritos { get; } = new List<Carrito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }
}
