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
using OnBreakLibrary;

namespace OnBreak
{
    /// <summary>
    /// Lógica de interacción para ListaContrato.xaml
    /// </summary>
    public partial class ListaContrato : Window
    {
        
        private ContratoCollection _contratoCollection = new ContratoCollection();
        private List<Contrato> actualContrato = new List<Contrato>();

        public static ListaContrato ventana = null;

        public static ListaContrato getInstance()
        {
            if (ventana == null)
            {
                ventana = new ListaContrato();
            }

            return ventana;
        }

        public ListaContrato()
        {
            InitializeComponent();
            cboTipoContrato.ItemsSource = _contratoCollection.ListarTipoEvento();
            CargarGrilla();
            NotificationCenter.Subscribe("contrato_changed", CargarGrilla);
        }

        private void CargarGrilla()
        {
            Dispatcher.Invoke(() =>
            {
                dgContratos.ItemsSource = null;
                dgContratos.ItemsSource = _contratoCollection.ListarContratos();
            });
        }



        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
            dgContratos.SelectedIndex = -1;
            txtRut.Text = null;
            txtContratoId.Text = null;
            cboTipoContrato.SelectedIndex = -1;
        }

        private void TxtRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgContratos.ItemsSource = null;
            dgContratos.ItemsSource = this._contratoCollection.BuscarContratosPorRut(txtRut.Text);
        }

        private void TxtContratoId_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgContratos.ItemsSource = null;
            dgContratos.ItemsSource = this._contratoCollection.BuscarContratoPorNumeroContrato(txtContratoId.Text);
        }

        private void BtnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            if(cboTipoContrato.SelectedIndex == -1)
            {
                MessageBox.Show("No existe filtro seleccionado, seleccione uno");
            }
            else
            {
                dgContratos.ItemsSource = this._contratoCollection.BuscarContratosPorTipo(int.Parse(cboTipoContrato.SelectedValue.ToString()));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ventana = null;
        }

        private void DgContratos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgContratos.SelectedValue != null)
            {
                Contrato selected_contrato = (Contrato)dgContratos.SelectedValue;
                NotificationCenter.Notify("contrato_selected", selected_contrato);
            };
        }
    }
}
