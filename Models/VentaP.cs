using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutos1.Models
{
    public partial class Venta
    {
       public string MensajeEliminarVenta
        {
            get
            {
                return $"¿Desea eliminar la venta del cliente {NombreComprador}, vehículo {IdAutoNavigation.Marca} {IdAutoNavigation.Modelo}?";
            }
        }

    }
}
