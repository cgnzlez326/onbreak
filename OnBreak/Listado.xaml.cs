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
using OnBreakLibrary;

namespace OnBreak
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : Window
    {
        private ClienteCollection _clienteCollection = new ClienteCollection();

        public static Listado ventana = null;

        public static Listado getInstance()
        {
            if (ventana == null)
            {
                ventana = new Listado();
            }

            return ventana;
        }

        public Listado()
        {
            InitializeComponent();
            cboActividadEmpresa.ItemsSource = _clienteCollection.ListarActividadEmpresa();
            cboTipoEmpresa.ItemsSource = _clienteCollection.ListarTipoEmpresa();
            NotificationCenter.Subscribe("client_changed", CargarGrilla);
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            Dispatcher.Invoke(() =>
            {
                dgClientes.ItemsSource = null;
                dgClientes.ItemsSource = this._clienteCollection.ListarClientes();
            });
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = this._clienteCollection.BuscarClientePorRut(txtRut.Text);
        }

        private void BtnFiltrarActividad_Click(object sender, RoutedEventArgs e)
        {
            if(cboActividadEmpresa.SelectedIndex == -1)
            {
                MessageBox.Show("No se ha seleccionado ningún filtro para actividad");
            }
            else
            {
                dgClientes.ItemsSource = this._clienteCollection.BuscarClientePorRubro(int.Parse(cboActividadEmpresa.SelectedValue.ToString()));
            }
        }

        private void BtnFiltar_Click(object sender, RoutedEventArgs e)
        {
            if (cboTipoEmpresa.SelectedIndex == -1)
            {
                MessageBox.Show("No se ha seleccionado ningún filtro para tipo");
            }
            else
            {
                dgClientes.ItemsSource = this._clienteCollection.BuscarClientePorTipoEmpresa(int.Parse(cboTipoEmpresa.SelectedValue.ToString()));
            }
        }

        private void BtnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
            cboTipoEmpresa.SelectedIndex = -1;
            cboActividadEmpresa.SelectedIndex = -1;
            txtRut.Text = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ventana = null;
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientes.SelectedValue != null)
            {
                Cliente selected_cliente = (Cliente)dgClientes.SelectedValue;
                NotificationCenter.Notify("client_selected", selected_cliente);
            }
        }
    }
}
