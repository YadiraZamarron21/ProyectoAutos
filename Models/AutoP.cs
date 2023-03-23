using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutos1.Models
{
    public partial class Auto
    {
        public string MensajeEliminarVehiculo
        {
            get
            {
                return $"¿Desea eliminar el vehículo de la marca {Marca} modelo {Modelo} año {Año}?";
            }
        }
    }
}
