using ProyectoAutos1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutos1.Catalagos
{
    public class EstadisticasCatalago
    {

        AutomovilesContext context = new();

        public IEnumerable<CantidadMarcaAutos> GetCantidadAutosXMarca()
        {

            return context.Auto.GroupBy(x => x.Marca).
                    Select(x => new CantidadMarcaAutos
                    {
                        Marca = x.Key,
                        CantidadAutoMarca = x.Count()
                    });
        }

        public IEnumerable<Auto> GetVehiculosGasolina()
        {
            return context.Auto.Where(x => x.Motor == "Gasolina");
        }

        public IEnumerable<Auto> GetVehiculosDiesel()
        {
            return context.Auto.Where(x => x.Motor == "Diesel");
        }

        public IEnumerable<Auto> GetVehiculosHibrido()
        {
            return context.Auto.Where(x => x.Motor == "Hibrido");
        }

        public double GetAutoAñoReciente()
        {
            return context.Auto.Max(x => x.Año);
        }

        public double GetAutoAñoViejo()
        {
            return context.Auto.Min(x => x.Año);
        }

        public IEnumerable<Venta> GetVentaXTipoPago()
        {
            return context.Venta.Where(x => x.TipoPago == "Contado" || x.TipoPago == "Plazos");
        }



    }
}
