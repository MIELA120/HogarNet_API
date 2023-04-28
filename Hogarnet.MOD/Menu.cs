using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string? NombreMenu { get; set; }

    public string? Icono { get; set; }

    public string? UrlMenu { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; } = new List<MenuRol>();
}
