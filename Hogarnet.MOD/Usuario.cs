using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public int IdRol { get; set; }

    public string? Clave { get; set; }

    public int IdEstado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Carrito> Carritos { get; } = new List<Carrito>();

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
