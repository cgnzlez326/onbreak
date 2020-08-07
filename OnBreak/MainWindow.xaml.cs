using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Runtime.Caching;


namespace OnBreak
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ClienteCollection _clienteCollection = new ClienteCollection();
        private ContratoCollection _contratoCollection = new ContratoCollection();

        private Thread hiloRespaldo;

        public static MainWindow ventana = null;


        public static MainWindow getInstance()
        {
            if (ventana == null)
            {
                ventana = new MainWindow();
            }

            return ventana;
        }

        private int intento = new int();

        public MainWindow()
        {
            InitializeComponent();
            cboActividad.ItemsSource = _clienteCollection.ListarActividadEmpresa();
            cboTipoEmpresa.ItemsSource = _clienteCollection.ListarTipoEmpresa();
            CargarGrilla();
            NotificationCenter.Subscribe("client_selected", CargarCliente);
            btnGuardar.IsEnabled = true;
            intento = 0;

            Task tarea = new Task(() =>
            {
                hiloRespaldo = Thread.CurrentThread;

                while (true)
                {
                    Thread.Sleep(300000);
                    Respaldar();
                }
            });

            tarea.Start();

            FileCache filecache = new FileCache(new ObjectBinder());
            if (filecache["tiempo_respaldo"] != null)
            {
                lblRespaldo.Content = filecache["tiempo_respaldo"].ToString();
            }
        }


        public void Respaldar()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    Cliente cliente = new Cliente();
                    cliente.Rut = txtRut.Text;
                    cliente.Nombre_contacto = txtNombreContacto.Text;
                    cliente.Email_contacto = txtEmailContacto.Text;
                    cliente.Razon_social = txtRazonSocial.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Telefono = txtTelefono.Text;

                    if (cboActividad.SelectedValue != null)
                    {
                        cliente.ActividadEmpresaCliente = new ActividadEmpresa()
                        {
                            Id = int.Parse(cboActividad.SelectedValue.ToString())
                        };

                    }

                    if (cboTipoEmpresa.SelectedValue!= null)
                    {
                        cliente.TipoEmpresaCliente = new TipoEmpresa()
                        {
                            Id = int.Parse(cboTipoEmpresa.SelectedValue.ToString())
                        };
                    }

                    FileCache filecache = new FileCache(new ObjectBinder());
                    String ahora = DateTime.Now.ToString("dd-MM-yyy HH:mm");
                    filecache["cliente"] = cliente;
                    filecache["tiempo_respaldo"] = ahora;
                    lblRespaldo.Content = ahora;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            });
        }


        public void CargarCliente (object cliente)
        {
            Dispatcher.Invoke(() =>
            {
                if (cliente != null)
                {
                    Cliente selected_cliente = (Cliente)cliente;
                    txtRut.Text = selected_cliente.Rut;
                    btnBuscar_Click(new object(), new RoutedEventArgs());
                }
            });
        }

        private void CargarGrilla()
        {
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = this._clienteCollection.ListarClientes();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActividadEmpresa actividadEmp = new ActividadEmpresa()
                {
                    Id = int.Parse(cboActividad.SelectedValue.ToString())
                };

                TipoEmpresa tipoEmp = new TipoEmpresa()
                {
                    Id = int.Parse(cboTipoEmpresa.SelectedValue.ToString())
                };

                if (String.IsNullOrWhiteSpace(txtRut.Text) ||
                    String.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                    String.IsNullOrWhiteSpace(txtEmailContacto.Text) ||
                    String.IsNullOrWhiteSpace(txtRazonSocial.Text) ||
                    String.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    String.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    cboActividad.SelectedIndex == -1 ||
                    cboTipoEmpresa.SelectedIndex == -1)
                {
                    MessageBox.Show("Falta uno o más campos por rellenar");
                }
                else
                {
                    if ((txtDireccion.Text.Length < 3 || txtDireccion.Text.Length > 50) || (txtRazonSocial.Text.Length < 3 || txtRazonSocial.Text.Length > 50))
                    {
                        MessageBox.Show("El campo Dirección o Razón social no están dentro del máximo o mínimo (3 a 50 caracteres)");
                    }
                    else
                    {
                        Cliente cliente = new Cliente();
                        if (!Modulo11(txtRut.Text))
                        {
                            MessageBox.Show("Rut no cumple con el formato módulo 11");
                            if (intento == 3)
                            {
                                btnGuardar.IsEnabled = false;
                                return;
                            }
                            intento += 1;
                            return;
                        }

                        if (!MailValido(txtEmailContacto.Text))
                        {
                            MessageBox.Show("Mail no valido");
                            return;
                        };

                        cliente.Rut = txtRut.Text;
                        cliente.Nombre_contacto = txtNombreContacto.Text;
                        cliente.Email_contacto = txtEmailContacto.Text;
                        cliente.Razon_social = txtRazonSocial.Text;
                        cliente.Direccion = txtDireccion.Text;
                        cliente.Telefono = txtTelefono.Text;
                        cliente.TipoEmpresaCliente = tipoEmp;
                        cliente.ActividadEmpresaCliente = actividadEmp;

                        if (this._clienteCollection.GuardarClientes(cliente))
                        {
                            MessageBox.Show("Guardado existosamente");
                            NotificationCenter.Notify("client_changed");
                            btnLimpiar_Click(new object(), new RoutedEventArgs());
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido guardar, rut ya existente");
                        }
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Actividad de la empresa o tipo de empresa vacío");
            }

            this.CargarGrilla();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtRut.Text = "";
            txtNombreContacto.Text = "";
            txtEmailContacto.Text = "";
            txtDireccion.Text = "";
            txtRazonSocial.Text = "";
            txtTelefono.Text = "";
            cboActividad.SelectedIndex = -1;
            cboTipoEmpresa.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRut.Text;

            if (!Modulo11(rut))
            {
                MessageBox.Show("Rut no cumple con el formato módulo 11");
                return;
            }

            List<Contrato> ContratosPorRut = new List<Contrato>();

            if (this._clienteCollection.BuscarCliente(rut) != null)
            {
                ContratosPorRut = this._contratoCollection.BuscarContratosPorRut(rut);
                if (ContratosPorRut.FirstOrDefault() == null)
                {
                    this._clienteCollection.EliminarCliente(rut);
                    MessageBox.Show("Eliminado Exitosamente");
                    btnLimpiar_Click(new object(), new RoutedEventArgs());
                    NotificationCenter.Notify("client_changed");
                }
                else
                {
                    MessageBox.Show("El cliente tiene contratos asociados");
                }
            }
            else
            {
                MessageBox.Show("No existe el rut ingresado");
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRut.Text;
            Cliente cliente = _clienteCollection.BuscarCliente(rut);

            if (!Modulo11(rut))
            {
                MessageBox.Show("Rut no cumple con el formato módulo 11");
                return;
            }

            try
            {
                ActividadEmpresa actividadEmp = new ActividadEmpresa()
                {
                    Id = int.Parse(cboActividad.SelectedValue.ToString())
                };

                TipoEmpresa tipoEmp = new TipoEmpresa()
                {
                    Id = int.Parse(cboTipoEmpresa.SelectedValue.ToString())
                };

                if (cliente == null)
                {
                    MessageBox.Show("No existe el rut ingresado");
                    return;
                }
                else
                {

                    cliente.Nombre_contacto = txtNombreContacto.Text;
                    cliente.Email_contacto = txtEmailContacto.Text;
                    if (!MailValido(txtEmailContacto.Text))
                    {
                        MessageBox.Show("Mail no valido");
                        return;
                    };
                    cliente.Razon_social = txtRazonSocial.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.TipoEmpresaCliente = tipoEmp;
                    cliente.ActividadEmpresaCliente = actividadEmp;

                    if (this._clienteCollection.ModificarCliente(cliente))
                    {
                        MessageBox.Show("Cliente modificado con éxito");
                        NotificationCenter.Notify("client_changed");
                        btnLimpiar_Click(new object(), new RoutedEventArgs());
                        btnLimpiar_Click(new object(), new RoutedEventArgs());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Actividad de la empresa o tipo de empresa vacío");
            }


        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRut.Text;
            Cliente cliente1 = _clienteCollection.BuscarCliente(rut);
            if (cliente1 == null)
            {
                MessageBox.Show("No se ha encontrado el rut registrado");
                return;
            }
            txtNombreContacto.Text = cliente1.Nombre_contacto;
            txtEmailContacto.Text = cliente1.Email_contacto;
            txtDireccion.Text = cliente1.Direccion;
            txtRazonSocial.Text = cliente1.Razon_social;
            txtTelefono.Text = cliente1.Telefono.ToString();
            cboTipoEmpresa.SelectedValue = cliente1.TipoEmpresaCliente.Id;
            cboActividad.SelectedValue = cliente1.ActividadEmpresaCliente.Id;
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool Modulo11(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        public string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            int sds;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        public static bool MailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ventana = null;
        }

        private void BtnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            FileCache filecache = new FileCache(new ObjectBinder());

            if (filecache["cliente"] != null )
            {
                Cliente cliente1 = (Cliente)filecache["cliente"];
                txtRut.Text = cliente1.Rut.ToString();
                txtNombreContacto.Text = cliente1.Nombre_contacto;
                txtEmailContacto.Text = cliente1.Email_contacto;
                txtDireccion.Text = cliente1.Direccion;
                txtRazonSocial.Text = cliente1.Razon_social;
                txtTelefono.Text = cliente1.Telefono.ToString();
                cboTipoEmpresa.SelectedValue = cliente1.TipoEmpresaCliente.Id;
                cboActividad.SelectedValue = cliente1.ActividadEmpresaCliente.Id;
            }
            else
            {
                MessageBox.Show("No hay respaldo disponible");
            }
        }
    }
}
