using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Estado
{
    public int IdEstado { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Categoria> Categoria { get; } = new List<Categoria>();

    public virtual ICollection<Marca> Marcas { get; } = new List<Marca>();

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
