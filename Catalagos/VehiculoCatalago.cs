using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ProyectoAutos1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutos1.Catalagos
{
    public class VehiculoCatalago
    {
        AutomovilesContext contenedor = new();

        public IEnumerable<Auto> GetAutos()
        {
            return contenedor.Auto.OrderBy(x => x.Marca);
        }

        public void CreateAuto(Auto a)
        {
            contenedor.Database.ExecuteSqlRaw($"CALL spRegistrarAuto ('{a.Marca}','{a.Modelo}',{a.Año},'{a.Precio}','{a.Imagen}','{a.Motor}','{a.Transmision}')"); 
            contenedor.SaveChanges();
        }

        public void DeleteAuto(Auto a)
        {
            contenedor.Database.ExecuteSqlRaw($"CALL spEliminarAuto ({a.Id})");
           // contenedor.Auto.Remove(a);
            contenedor.SaveChanges();
        }

        public Auto? GetIdAuto(int id)
        {
            return contenedor.Auto.Find(id);
        }

        public bool Validar(Auto a, out List<string> lista)
        {
            lista = new List<string>();

            if (a is not null)
            {
                if (string.IsNullOrWhiteSpace(a.Marca))
                {
                    lista.Add("La marca del auto no puede estar vacía, por favor complete el campo.");
                }
                else if (a.Marca.Length > 30)
                {
                    lista.Add("La marca del auto ha superado el tamaño de caracteres permitidos.");
                }
                if (string.IsNullOrWhiteSpace(a.Modelo))
                {
                    lista.Add("El modelo del auto no puede estar vacío, por favor complete el campo.");
                }
                else if (a.Modelo.Length > 30)
                {
                    lista.Add("El modelo del auto ha superado el tamaño de caracteres permitidos.");
                }
                if (a.Año == 0)
                {
                    lista.Add("Escriba el año del vehículo correctamente.");
                }
                if (a.Precio == 0 || a.Precio < 0)
                {
                    lista.Add("El precio debe ser mayor a $0.");
                }
                if (string.IsNullOrWhiteSpace(a.Imagen))
                {
                    lista.Add("La imagen del auto no puede estar vacía, por favor complete el campo.");
                }
                else if (!Uri.TryCreate(a.Imagen, UriKind.Absolute, out var uni))
                {
                    lista.Add("Escriba una URL válida.");
                }
                if (string.IsNullOrWhiteSpace(a.Motor))
                {
                    lista.Add("EL motor del auto no puede estar vacío, por favor complete el campo.");
                }
                else if (a.Motor.Length > 20)
                {
                    lista.Add("El motor del auto ha superado el tamaño de caracteres permitidos.");
                }
                else if (a.Motor.ToUpper() != "GASOLINA" && a.Motor.ToUpper() != "DIESEL" && a.Motor.ToUpper() != "HIBRIDO")
                {
                    lista.Add("El motor solo puede ser de tipo: 'Gasolina', 'Diesel' o 'Hibrido'");
                }
                if (string.IsNullOrWhiteSpace(a.Transmision))
                {
                    lista.Add("La transmisión del auto no puede estar vacío, por favor complete el campo.");
                }
                else if (a.Transmision.Length > 15)
                {
                    lista.Add("La transmisión del auto ha superado el tamaño de caracteres permitidos.");
                }
                else if (a.Transmision.ToUpper() != "AUTOMATICA" && a.Transmision.ToUpper() != "MANUAL" && a.Transmision.ToUpper() != "AUTOMÁTICA")
                {
                    lista.Add("La transmisión solo puede ser 'Manual' o 'Automática'");
                }



            }
            return lista.Count == 0;
        }
    }
}
