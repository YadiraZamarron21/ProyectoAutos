using ProyectoAutos1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoAutos1.Views.VentasView
{
    /// <summary>
    /// Interaction logic for VentasView.xaml
    /// </summary>
    public partial class VentasView : UserControl
    {
        public VentasView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vw = this.DataContext as VentasViewModel;

            if(vw != null)
            {
                vw.FiltarVentasCommand.Execute(null);
            }
        }
    }
}
