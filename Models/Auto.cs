using System;
using System.Collections.Generic;

namespace ProyectoAutos1.Models;

public partial class Auto
{
    public int Id { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Año { get; set; }

    public decimal Precio { get; set; }

    public string Imagen { get; set; } = null!;

    public string Motor { get; set; } = null!;

    public string Transmision { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; } = new List<Venta>();
}
