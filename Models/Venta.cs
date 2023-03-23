using System;
using System.Collections.Generic;

namespace ProyectoAutos1.Models;

public partial class Venta
{
    public int Idventa { get; set; }

    public string NombreComprador { get; set; } = null!;

    public string DireccionComprador { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Ocupacion { get; set; }

    public string TipoPago { get; set; } = null!;

    public DateTime FechaCompra { get; set; } = DateTime.Now;

    public int IdAuto { get; set; }

    public virtual Auto IdAutoNavigation { get; set; } = null!;
}
