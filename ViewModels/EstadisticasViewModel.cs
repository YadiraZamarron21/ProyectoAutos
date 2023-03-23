using GalaSoft.MvvmLight.Command;
using ProyectoAutos1.Catalagos;
using ProyectoAutos1.Models;
using ProyectoAutos1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoAutos1.ViewModels
{
    public class EstadisticasViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<CantidadMarcaAutos> ListaCantidadMarcaAutos { get; set; } = new();
        public ObservableCollection<Auto> ListaEstadisticas { get; set; } = new();
        public ObservableCollection<Venta> ListaEstadisticasVentas { get; set; } = new();

        EstadisticasCatalago cestadisticas = new();

        public Accion Operacion { get; set; }
        public double AutoAñoReciente { get; set; }
        public double AutoAñoViejo { get; set; }
        public ICommand VerAñoVehiculoCommand { get; set; }
        public ICommand VerEstadisticasMarcaCommand { get; set; }
        public ICommand VerVehiculosGasolinaCommand { get; set; }
        public ICommand VerVehiculosDieselCommand { get; set; }
        public ICommand VerVehiculosHibridoCommand { get; set; }
        public ICommand VerVentasXTipoPagoCommand { get; set; }


        public EstadisticasViewModel()
        {
            //ActualizarTodo();
            VerAñoVehiculoCommand = new RelayCommand(VerAñoReciente);
            VerEstadisticasMarcaCommand = new RelayCommand(VerAutosxMarca);
            VerVehiculosGasolinaCommand = new RelayCommand(VerVehiculosGasolina);
            VerVehiculosDieselCommand = new RelayCommand(VerVehiculosDiesel);
            VerVehiculosHibridoCommand = new RelayCommand(VerVehiculosHibrido);
            VerVentasXTipoPagoCommand = new RelayCommand(VerVentasxTipoPago);
        }

        private void VerVentasxTipoPago()
        {
            Operacion = Accion.VerVentasxTipoPago;

            ListaEstadisticasVentas.Clear();

            foreach (var item in cestadisticas.GetVentaXTipoPago())
            {
                ListaEstadisticasVentas.Add(item);
            }

            Actualizar();
        }

        private void VerVehiculosHibrido()
        {
            Operacion = Accion.VerVehiculosHibrido;

            ListaEstadisticas.Clear();

            foreach (var item in cestadisticas.GetVehiculosHibrido())
            {
                ListaEstadisticas.Add(item);
            }

            Actualizar();
        }

        private void VerVehiculosDiesel()
        {
            Operacion = Accion.VerVehiculosDiesel;

            ListaEstadisticas.Clear();

            foreach (var item in cestadisticas.GetVehiculosDiesel())
            {
                ListaEstadisticas.Add(item);
            }

            Actualizar();
        }

        private void VerVehiculosGasolina()
        {
            Operacion = Accion.VerVehiculosGasolina;

            ListaEstadisticas.Clear();

            foreach (var item in cestadisticas.GetVehiculosGasolina())
            {
                ListaEstadisticas.Add(item);
            }
            Actualizar();
        }

        private void VerAutosxMarca()
        {
            Operacion = Accion.VerVehiculosXMarca;
            ListaCantidadMarcaAutos.Clear();

            foreach (var item in cestadisticas.GetCantidadAutosXMarca())
            {
                ListaCantidadMarcaAutos.Add(item);
            }

            Actualizar();
        }

        private void VerAñoReciente()
        {
            Operacion = Accion.VerAñoVehiculo;
            AutoAñoReciente = cestadisticas.GetAutoAñoReciente();
            AutoAñoViejo = cestadisticas.GetAutoAñoViejo();
            Actualizar();
        }
        public void Actualizar(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
