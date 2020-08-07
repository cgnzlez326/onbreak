using OnBreakLibrary;
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
using System.Windows.Shapes;


namespace OnBreak
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        private ClienteCollection _clienteCollection = new ClienteCollection();
        private ContratoCollection _contratoCollection = new ContratoCollection();

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.getInstance().Show();
            MainWindow.getInstance().Activate();
        }
     
        private void btnlistado_Click(object sender, RoutedEventArgs e)
        {
            Listado.getInstance().Show();
            Listado.getInstance().Activate();
        }

        private void btnadministrarContrato_Click(object sender, RoutedEventArgs e)
        {
            AdministrarContrato.getInstance().Show();
            AdministrarContrato.getInstance().Activate();
        }

        private void btnlistar_Click(object sender, RoutedEventArgs e)
        {
            ListaContrato.getInstance().Show();
            ListaContrato.getInstance().Activate();
        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
      
