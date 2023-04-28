using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
