using GalaSoft.MvvmLight.Command;
using ProyectoAutos1.Catalagos;
using ProyectoAutos1.Models;
using ProyectoAutos1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoAutos1.ViewModels
{
    public class VehiculosViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Auto> ListaAutos { get; set; } = new ObservableCollection<Auto>();

        public VehiculoCatalago CatalagoVehiculos = new();
        public Auto auto { get; set; } 
        public string Error { get; set; }
        public Accion Operacion { get; set; }
        public ICommand VerRegistrarVehiculoCommand { get; set; }
        public ICommand VerEliminarVehiculoCommand { get; set; }
        public ICommand RegresarCommnd { get; set; }
        public ICommand RegistrarVehiculoCommand { get; set; }
        public ICommand EliminarVehiculoCommand { get; set; }


        public VehiculosViewModel()
        {
            
            ActualizarBD();
            VerRegistrarVehiculoCommand = new RelayCommand(VerRegistrarVehiculo);
            VerEliminarVehiculoCommand = new RelayCommand<int>(VerEliminarVehiculo);
            RegresarCommnd = new RelayCommand(Regresar);
            RegistrarVehiculoCommand = new RelayCommand(RegistrarAuto);
            EliminarVehiculoCommand = new RelayCommand(EliminarVehiculo);
        }

        private void EliminarVehiculo()
        {
            if (auto != null)
            {
                CatalagoVehiculos.DeleteAuto(auto);
                ActualizarBD();
                Regresar();
            }
        }

        private void VerEliminarVehiculo(int id)
        {
            auto = CatalagoVehiculos.GetIdAuto(id);
            if (auto != null)
            {
                Operacion = Accion.EliminarAutos;
                Actualizar();
            }
        }

        private void ActualizarBD()
        {
            ListaAutos.Clear();
            foreach (var item in CatalagoVehiculos.GetAutos())
            {
                ListaAutos.Add(item);
            }
            Actualizar();
        }

        private void RegistrarAuto()
        {
            if (auto != null)
            {
                Error = "";
                if (CatalagoVehiculos.Validar(auto, out List<string> lista))
                {
                    CatalagoVehiculos.CreateAuto(auto);
                    ActualizarBD();
                    Operacion = Accion.VerAutos;

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
            Operacion = Accion.VerAutos;
            Actualizar();
        }

        private void VerRegistrarVehiculo()
        {
            auto = new();
            Operacion = Accion.AgregarAuto;
            Actualizar();
        }


        void Actualizar(string? Propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propiedad));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
