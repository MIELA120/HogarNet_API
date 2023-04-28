using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? NombreMarca { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
