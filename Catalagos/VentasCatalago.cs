using Microsoft.EntityFrameworkCore;
using ProyectoAutos1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProyectoAutos1.Catalagos
{
    public class VentasCatalago
    {
        AutomovilesContext contenedor = new();

        public IEnumerable<Venta> GetVentas()
        {
            return contenedor.Venta.OrderBy(x => x.NombreComprador);
        }

        
        public IEnumerable<Venta> GetventasByIdVehiculo(int id)
        {
            return contenedor.Venta.Include(x => x.IdAutoNavigation).Where(x => x.IdAuto == id).OrderBy
                (x => x.IdAutoNavigation.Marca);
        }

        public Venta? GetIdVenta(int id) //FALTABA
        {
            return contenedor.Venta.Find(id);
        }



        public void CreateVenta(Venta v)
        {
            contenedor.Venta.Add(v);
            contenedor.SaveChanges();
        }

        public void DeleteVenta(Venta v) //FALTABA
        {
            contenedor.Venta.Remove(v);
            contenedor.SaveChanges();
        }


        public bool Validar(Venta v, out List<string> Lista)
        {
            Lista = new List<string>();

           
            if(v != null)
            {

                string patronNumTelefono = @"[0-9]{10}";
                Regex regular = new Regex(patronNumTelefono);

                if (string.IsNullOrWhiteSpace(v.NombreComprador))
                {
                    Lista.Add("Escriba el nombre del comprador del vehículo.");
                }
                else if(v.NombreComprador.Length > 50)
                {
                    Lista.Add("El nombre ha superado el tamaño de caracteres permitidos.");
                }
                if (string.IsNullOrWhiteSpace(v.DireccionComprador))
                {
                    Lista.Add("Escriba la dirección del comprador del vehículo.");
                }
                else if(v.DireccionComprador.Length > 50)
                {
                    Lista.Add("La dirección ha superado el tamaño de caracteres permitidos.");
                }
                if (string.IsNullOrWhiteSpace(v.Telefono))
                {
                    Lista.Add("Escriba el número teléfonico del comprador del vehículo");
                }
                else if(regular.IsMatch(v.Telefono) == false)
                {
                    Lista.Add("Escriba el número de teléfono en el formato correcto.");
                }
                if (string.IsNullOrWhiteSpace(v.TipoPago))
                {
                    Lista.Add("Escriba el tipo de pago realizado.");
                }
                else if (v.TipoPago.ToUpper() != "PLAZOS" && v.TipoPago.ToUpper() != "CONTADO")
                {
                    Lista.Add("Solo existen dos tipos de pago: Plazos y Contado");
                }

            }

            return Lista.Count == 0;

        }
        
    }
}

