using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoAutos1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        VehiculosViewModel vehiculosviewmodel = new();
        VentasViewModel ventasviewmodel = new();
        EstadisticasViewModel estadisticasviewmodel = new();

        private object viewmodelactual;

        public object ViewModelActual
        {
            get { return viewmodelactual; }
            set
            {
                viewmodelactual = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        public ICommand NavegarAutosCommand { get; set; }
        public ICommand NavegarVentasCommand { get; set; }
        public ICommand NavegarEstadisticasCommand { get; set; }

        public MainViewModel()
        {
            ViewModelActual = vehiculosviewmodel;
            NavegarAutosCommand = new RelayCommand(NavegarVehiculos);
            NavegarVentasCommand = new RelayCommand(NavegarVentas);
            NavegarEstadisticasCommand = new RelayCommand(NavegarEstadisticas);
        }

        private void NavegarVentas()
        {
            ViewModelActual = ventasviewmodel;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void NavegarVehiculos()
        {
            ViewModelActual = vehiculosviewmodel;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void NavegarEstadisticas()
        {
            //estadisticasviewmodel.ActualizarTodo();
            ViewModelActual = estadisticasviewmodel;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
