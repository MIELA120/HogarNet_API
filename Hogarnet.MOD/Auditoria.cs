using System;
using System.Collections.Generic;

namespace Hogarnet.MOD;

public partial class Auditoria
{
    public int IdAuditoria { get; set; }

    public string? Usuario { get; set; }

    public string? Accion { get; set; }

    public string? Detalle { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
