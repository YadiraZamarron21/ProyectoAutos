using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProyectoAutos1.Catalagos;
using ProyectoAutos1.Models;
using ProyectoAutos1.Views;

namespace ProyectoAutos1.ViewModels
{
    public class VentasViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Venta> ListaVentas { get; set; } = new();
        public ObservableCollection<Auto> ListaAutos { get; set; } = new();

        public VentasCatalago ventascatalago = new();
        public VehiculoCatalago vehiculocatalago = new();
        public Accion Operacion { get; set; }
        public string? Error { get; set; }
        public Venta venta { get; set; } = new();
        public Auto? auto { get; set; } = new();
        public ICommand VerRegistrarVentaCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public ICommand RegistrarVentaCommand { get; set; }
        public ICommand FiltarVentasCommand { get; set; }
        public ICommand VerEliminarVentaCommand { get; set; }
        public ICommand EliminarVentaCommand { get; set; } 


        public VentasViewModel()
        {
            CargarVehiculos();
            FiltrarVentas();
            ActualizarBD();
            Operacion = Accion.VerVentas;
            VerRegistrarVentaCommand = new RelayCommand(VerRegistrarVenta);
            RegresarCommand = new RelayCommand(Regresar);
            RegistrarVentaCommand = new RelayCommand(RegistrarVenta);
            FiltarVentasCommand = new RelayCommand(FiltrarVentas);
            VerEliminarVentaCommand = new RelayCommand<int>(VerEliminarVenta);
            EliminarVentaCommand = new RelayCommand(EliminarVenta); 

        }

        private void EliminarVenta()
        {
            if (venta != null)
            {
                ventascatalago.DeleteVenta(venta);
                ActualizarBD();
                Regresar();
            }
        }

        private void VerEliminarVenta(int id)
        {
            venta = ventascatalago.GetIdVenta(id);
            if (venta != null)
            {
                Operacion = Accion.EliminarVenta;
                Actualizar();
            }
        }

        private void FiltrarVentas()
        {
           if(ListaVentas != null)
           {
                ListaVentas.Clear();

                if(venta != null && auto != null)
                {
                    var ventas = ventascatalago.GetventasByIdVehiculo(auto.Id);

                    foreach (var item in ventas)
                    {
                        ListaVentas.Add(item);
                    }
                   // Actualizar();
                }
           }
        }

        void CargarVehiculos()
        {
            ListaAutos.Clear();
            foreach (var item in vehiculocatalago.GetAutos())
            {
                ListaAutos.Add(item);
            }
            auto = ListaAutos.FirstOrDefault();
            Actualizar();
        }

        void ActualizarBD()
        {
            ListaVentas.Clear();
            foreach (var item in ventascatalago.GetVentas())
            {
                ListaVentas.Add(item);
            }
            Actualizar();
        }

        private void RegistrarVenta()
        {
            if (venta != null)
            {
                Error = "";
                if (ventascatalago.Validar(venta, out List<string> lista))
                {
                    venta.IdAuto = auto.Id;
                    ventascatalago.CreateVenta(venta);
                    ActualizarBD();
                    Regresar();
                }
                else
                {
                    foreach (var item in lista)
                    {
                        Error = $"{Error} {item} {Environment.NewLine}";
                        Actualizar();
                    }

                }
            }

        }

        private void Regresar()
        {
            Operacion = Accion.VerVentas;
            Actualizar();
        }

        private void VerRegistrarVenta()
        {
            venta = new();
            Operacion = Accion.AgregarVenta;
            Actualizar();
        }


        void Actualizar(string? Propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
