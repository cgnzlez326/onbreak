using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
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
using System.Windows.Shapes;
using OnBreakLibrary;

namespace OnBreak
{
    /// <summary>
    /// Lógica de interacción para AdministrarContrato.xaml
    /// </summary>
    public partial class AdministrarContrato : Window
    {
        private ClienteCollection _clienteCollection = new ClienteCollection();
        private ContratoCollection _contratoCollection = new ContratoCollection();
        private Thread hiloRespaldo;

        public static AdministrarContrato ventana = null;


        public static AdministrarContrato getInstance()
        {
            if (ventana == null)
            {
                ventana = new AdministrarContrato();
            }

            return ventana;
        }

        public AdministrarContrato()
        {
            InitializeComponent();
            cboTipoEvento.ItemsSource = _contratoCollection.ListarTipoEvento();
            NotificationCenter.Subscribe("contrato_selected", CargarContrato);
            //cboModalidadEvento.ItemsSource = contratoCollection.ListarModalidadServicio();

            Task tarea2 = new Task(() =>
            {
                hiloRespaldo = Thread.CurrentThread;

                while (true)
                {
                    Thread.Sleep(300000);
                    Respaldar();
                }
            });

            tarea2.Start();

            FileCache filecache = new FileCache(new ObjectBinder());
            if (filecache["tiempo_respaldo_contrato"] != null)
            {
                lblRespaldo.Content = filecache["tiempo_respaldo_contrato"].ToString();
            }
        }

        private void Respaldar()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    Contrato contrato = new Contrato();
                    if (cboModalidadEvento.SelectedValue != null)
                    {
                        contrato.ModalidadServicioContrato = _contratoCollection.BuscarValoresPorModalidad(cboModalidadEvento.SelectedValue.ToString());
                    }

                    if (Regex.IsMatch(txtCantAsist.Text, @"^\d{1,}$"))
                    {
                        contrato.CantAsist = Int32.Parse(txtCantAsist.Text);
                    }
                    
                    if (Regex.IsMatch(txtCantPersonalAdd.Text, @"^\d{1,}$"))
                    {
                        contrato.PersonalAdicional = Int32.Parse(txtCantPersonalAdd.Text);
                    }
  
                    contrato.Direccion = txtDireccion.Text;
                    contrato.Rut = txtRut.Text;
                    contrato.NombreEvento = txtEvento.Text;

                    if (txtValorTotal.Text != "")
                    {
                        contrato.Total = double.Parse(txtValorTotal.Text);
                    }

                    if (dpInicioEvento.Text != "" )
                    {
                        contrato.InicioEvento = DateTime.Parse(dpInicioEvento.Text);
                    }

                    if (dpTerminoEvento.Text != "")
                    {
                        contrato.TerminoEvento = DateTime.Parse(dpTerminoEvento.Text);
                    }
                    
                    contrato.Observaciones = txtObs.Text;


                    FileCache filecache = new FileCache(new ObjectBinder());
                    String ahora = DateTime.Now.ToString("dd-MM-yyy HH:mm");
                    filecache["contrato"] = contrato;
                    filecache["tiempo_respaldo_contrato"] = ahora;
                    lblRespaldo.Content = ahora;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            });
        }

        private void CargarContrato(object contrato)
        {
            Dispatcher.Invoke(() =>
            {
                if (contrato != null)
                {
                    Contrato selected_contrato = (Contrato)contrato;
                    BtnClear_Click(new object(), new RoutedEventArgs());
                    txtRut.Text = selected_contrato.Rut.ToString();
                    BtnBuscarRut_Click(new object(), new RoutedEventArgs());
                    txtNroContrato.Text = selected_contrato.NumeroContrato.ToString();
                    BtnBuscarContrato_Click(new object(), new RoutedEventArgs());
                }
            });
        }

        private void BtnBuscarRut_Click(object sender, RoutedEventArgs e)
        {
            Cliente c1 = new Cliente();
            if (String.IsNullOrWhiteSpace(txtRut.Text))
            {
                MessageBox.Show("El rut está vacío, no se pudo efectuar la búsqueda");
            }
            else
            {
                c1 = this._clienteCollection.BuscarCliente(txtRut.Text);
                if (c1 == null)
                {
                    MessageBox.Show("Usuario No encontrado");
                }
                else
                {
                    txtNombreContacto.Text = c1.Nombre_contacto;
                    if (this._contratoCollection.BuscarContratosPorRut(txtRut.Text) != null)
                    {
                        txtNroContrato.IsReadOnly = false;
                    }
                }
            }
        }

        private void BtnBuscarContrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            if (String.IsNullOrWhiteSpace(txtNroContrato.Text))
            {
                MessageBox.Show("El contrato está vacío, no se pudo efectuar la búsqueda");
            }
            else
            {
                contrato = this._contratoCollection.BuscarContrato(txtNroContrato.Text);
                if (contrato == null)
                {
                    MessageBox.Show("Contrato no encontrado");
                }
                else
                {
                    if (contrato.Rut != txtRut.Text)
                    {
                        MessageBox.Show("Contrato encontrado no se asocia al rut indicado");
                        return;
                    }
                    txtEvento.Text = contrato.NombreEvento;
                    cboTipoEvento.SelectedValue = contrato.ModalidadServicioContrato.IdEventoEmpresa.Id;
                    CboModalidadEvento_IsEnabledChanged(new object(), new DependencyPropertyChangedEventArgs());
                    cboModalidadEvento.IsEnabled = true;
                    cboModalidadEvento.SelectedValue = contrato.ModalidadServicioContrato.Id;
                    txtDireccion.Text = contrato.Direccion;
                    txtPrecioBase.Text = contrato.ModalidadServicioContrato.ValorBase.ToString();
                    txtPersonalBase.Text = contrato.ModalidadServicioContrato.PersonalBase.ToString();
                    txtCantAsist.Text = contrato.CantAsist.ToString();
                    txtCantPersonalAdd.Text = contrato.PersonalAdicional.ToString();
                    txtValorTotal.Text = contrato.Total.ToString();
                    dpInicioEvento.Text = contrato.InicioEvento.ToString();
                    dpTerminoEvento.Text = contrato.TerminoEvento.ToString();
                    txtObs.Text = contrato.Observaciones;
                    btnTerminar.IsEnabled = true;
                }
            }
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            int v_asistentes = 0;
            float v_asistentes_cena = 0;
            float v_personaladicional = 0;
            float v_total = 0;

            if (String.IsNullOrWhiteSpace(txtPrecioBase.Text) ||
                String.IsNullOrWhiteSpace(txtCantAsist.Text) ||
                String.IsNullOrWhiteSpace(txtCantPersonalAdd.Text))
            {
                MessageBox.Show("Faltan parámetros para calcular el precio final");
            }
            else
            {
                if (Regex.IsMatch(txtCantAsist.Text, @"^\d{1,}$") && Regex.IsMatch(txtCantPersonalAdd.Text, @"^\d{1,}$"))
                {
                    if (cboTipoEvento.SelectedValue != null)
                    {
                        if (int.Parse(cboTipoEvento.SelectedValue.ToString()) == 10)
                        {
                            if (Int32.Parse(txtCantAsist.Text.ToString()) <= 20)
                            {
                                v_asistentes = 3;
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 20 && Int32.Parse(txtCantAsist.Text) <= 50)
                            {
                                v_asistentes = 5;
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 50)
                            {
                                v_asistentes = 5 + ((int)((Int32.Parse(txtCantAsist.Text) - 50) / 20) * 2);
                            }

                            if (Int32.Parse(txtCantPersonalAdd.Text) == 2)
                            {
                                v_personaladicional = 2;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 3)
                            {
                                v_personaladicional = 3;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 4)
                            {
                                v_personaladicional = 3.5f;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) > 4)
                            {
                                v_personaladicional = 3.5f + 0.5f * (float)(Int32.Parse(txtCantPersonalAdd.Text) - 4);
                            }
                        }
                        else if (int.Parse(cboTipoEvento.SelectedValue.ToString()) == 20)
                        {
                            if (Int32.Parse(txtCantAsist.Text.ToString()) <= 20)
                            {
                                v_asistentes = 4;
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 20 && Int32.Parse(txtCantAsist.Text) <= 50)
                            {
                                v_asistentes = 6;
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 50)
                            {
                                v_asistentes = 6 + ((int)((Int32.Parse(txtCantAsist.Text) - 50) / 20) * 2);
                            }

                            if (Int32.Parse(txtCantPersonalAdd.Text) == 2)
                            {
                                v_personaladicional = 4;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 3)
                            {
                                v_personaladicional = 5;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 4)
                            {
                                v_personaladicional = 5.5f;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) > 4)
                            {
                                v_personaladicional = 3.5f + 0.5f * (float)(Int32.Parse(txtCantPersonalAdd.Text) - 4);
                            }
                        }
                        else if (int.Parse(cboTipoEvento.SelectedValue.ToString()) == 30)
                        {
                            if (Int32.Parse(txtCantAsist.Text.ToString()) <= 20)
                            {
                                v_asistentes_cena = 1.5f * float.Parse(txtCantAsist.Text.ToString());
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 20 && Int32.Parse(txtCantAsist.Text) <= 50)
                            {
                                v_asistentes_cena = 1.2f * float.Parse(txtCantAsist.Text.ToString());
                            }
                            else if (Int32.Parse(txtCantAsist.Text) > 50)
                            {
                                v_asistentes_cena = float.Parse(txtCantAsist.Text.ToString());
                            }

                            if (Int32.Parse(txtCantPersonalAdd.Text) == 2)
                            {
                                v_personaladicional = 3;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 3)
                            {
                                v_personaladicional = 4;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) == 4)
                            {
                                v_personaladicional = 5f;
                            }
                            else if (Int32.Parse(txtCantPersonalAdd.Text) > 4)
                            {
                                v_personaladicional = 5f + 0.5f * (float)(Int32.Parse(txtCantPersonalAdd.Text) - 4);
                            }
                        }
                    }


                    v_total = float.Parse(txtPrecioBase.Text) + (float)v_asistentes + v_asistentes_cena + v_personaladicional;
                    txtValorTotal.Text = v_total.ToString();
                }
                else
                {
                    MessageBox.Show("No se puede calcular si las cajas 'Precio Base', 'Cantidad Asistentes' o 'Cant. Personal Adicional' poseen texto no numérico");
                }
                /*revisar el cálculo con double*/
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCantAsist.Text = null;
            txtCantPersonalAdd.Text = null;
            txtDireccion.Text = null;
            txtEvento.Text = null;
            txtNombreContacto.Text = null;
            txtNroContrato.Text = null;
            txtObs.Text = null;
            txtPrecioBase.Text = null;
            txtPersonalBase.Text = null;
            txtRut.Text = null;
            txtValorTotal.Text = null;
            cboTipoEvento.SelectedIndex = -1;
            cboModalidadEvento.SelectedIndex = -1;
            dpInicioEvento.Text = null;
            dpTerminoEvento.Text = null;
            btnTerminar.IsEnabled = false;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCantAsist.Text) ||
                String.IsNullOrWhiteSpace(txtCantPersonalAdd.Text) ||
                String.IsNullOrWhiteSpace(txtDireccion.Text) ||
                String.IsNullOrWhiteSpace(txtEvento.Text) ||
                String.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                String.IsNullOrWhiteSpace(txtPrecioBase.Text) ||
                String.IsNullOrWhiteSpace(txtRut.Text) ||
                String.IsNullOrWhiteSpace(txtValorTotal.Text) ||
                cboTipoEvento.SelectedIndex == -1 ||
                String.IsNullOrWhiteSpace(dpInicioEvento.Text) ||
                String.IsNullOrWhiteSpace(dpTerminoEvento.Text))
            {
                MessageBox.Show("Falta uno o más campos por rellenar");
            }
            else
            {
                if (txtEvento.Text.Length< 3 || txtEvento.Text.Length > 50)
                {
                    MessageBox.Show("Texto inferior a 3 caracteres o mayor a 50");
                    return;
                }
                if (int.Parse(txtCantAsist.Text.ToString()) < 1) {
                    MessageBox.Show("Cantidad de asistentes debe ser mayor a 0");
                }
                else
                {
                    /*precio base y tipo evento off*/
                    Contrato contrato = new Contrato();

                    ModalidadServicio mserv = new ModalidadServicio()
                    {
                        Id = cboModalidadEvento.SelectedValue.ToString(),
                        ValorBase = int.Parse(txtPrecioBase.Text.ToString()),
                    };

                    contrato.CantAsist = Int32.Parse(txtCantAsist.Text);
                    contrato.PersonalAdicional = Int32.Parse(txtCantPersonalAdd.Text);
                    contrato.Direccion = txtDireccion.Text;
                    contrato.Rut = txtRut.Text;
                    contrato.NombreEvento = txtEvento.Text;
                    contrato.ModalidadServicioContrato = mserv;
                    contrato.Total = double.Parse(txtValorTotal.Text);
                    contrato.InicioEvento = DateTime.Parse(dpInicioEvento.Text);
                    contrato.TerminoEvento = DateTime.Parse(dpTerminoEvento.Text);
                    contrato.NumeroContrato = DateTime.Now.ToString("yyyyMMddHHmmss");
                    contrato.CreacionContrato = DateTime.Now;
                    contrato.TerminoContrato = DateTime.Parse("1753/1/1 00:00:00");
                    contrato.Vigente = true;
                    contrato.Observaciones = txtObs.Text;
                    if (DateTime.Parse(dpInicioEvento.Text).Day < DateTime.Now.Day)
                    {
                        MessageBox.Show("El día de inicio es menor a hoy");
                        return;
                    }
                    else if (DateTime.Parse(dpTerminoEvento.Text).Day < DateTime.Now.Day)
                    {
                        MessageBox.Show("El día de término es menor a hoy");
                        return;
                    }
                    else if (DateTime.Parse(dpTerminoEvento.Text) < DateTime.Parse(dpInicioEvento.Text))
                    {
                        MessageBox.Show("El día de término es menor al día de inicio");
                        return;
                    }
                    
                    if (this._contratoCollection.GuardarContrato(contrato))
                    {
                        txtNroContrato.Text = contrato.NumeroContrato;
                        MessageBox.Show($"Contrato guardado exitosamente \nEl Número de contrato es {txtNroContrato.Text}");
                        NotificationCenter.Notify("contrato_changed");
                    }
                    else
                    {
                        MessageBox.Show("El contrato no se ha podido guardar. Esperar un minuto tras creación");
                    }
                }

            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnTerminar_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            contrato = this._contratoCollection.BuscarContrato(txtNroContrato.Text);

            contrato.Vigente = false;
            contrato.TerminoContrato = DateTime.Now;
            if (_contratoCollection.TerminarContrato(contrato))
            {
                MessageBox.Show($"Contrato {contrato.NumeroContrato} finalizado");
                NotificationCenter.Notify("contrato_changed");
                BtnClear_Click(new object(), new RoutedEventArgs());
            }
            
        }

        private void CboTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoEvento.SelectedValue != null)
            {
                cboModalidadEvento.IsEnabled = false;
                cboModalidadEvento.IsEnabled = true;
            }
            
        }

        private void CboModalidadEvento_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (cboTipoEvento.SelectedValue != null)
            {
                cboModalidadEvento.ItemsSource = _contratoCollection.ListarModalidadPorTipoEvento(int.Parse(cboTipoEvento.SelectedValue.ToString()));
            }
        }

        private void CboModalidadEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModalidadServicio modalidadServicio = new ModalidadServicio();
            if (cboModalidadEvento.SelectedValue != null)
            {
                modalidadServicio = _contratoCollection.BuscarValoresPorModalidad(cboModalidadEvento.SelectedValue.ToString());

                txtPrecioBase.Text = modalidadServicio.ValorBase.ToString();
                txtPersonalBase.Text = modalidadServicio.PersonalBase.ToString();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ventana = null;
        }

        private void BtnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            FileCache filecache = new FileCache(new ObjectBinder());

            if (filecache["contrato"] != null)
            {
                Contrato contrato = (Contrato)filecache["contrato"];
                txtRut.Text = contrato.Rut;
                BtnBuscarRut_Click(new object(), new RoutedEventArgs());
                txtEvento.Text = contrato.NombreEvento;
                if (contrato.ModalidadServicioContrato != null)
                {
                    cboTipoEvento.SelectedValue = contrato.ModalidadServicioContrato.IdEventoEmpresa.Id;
                    CboModalidadEvento_IsEnabledChanged(new object(), new DependencyPropertyChangedEventArgs());
                    cboModalidadEvento.IsEnabled = true;
                    cboModalidadEvento.SelectedValue = contrato.ModalidadServicioContrato.Id;
                }
                txtDireccion.Text = contrato.Direccion;
                txtCantAsist.Text = contrato.CantAsist.ToString();
                txtCantPersonalAdd.Text = contrato.PersonalAdicional.ToString();

                if (contrato.InicioEvento != null)
                {
                    dpInicioEvento.Text = contrato.InicioEvento.ToString();
                }
                
                if (contrato.TerminoEvento != null)
                {
                    dpTerminoEvento.Text = contrato.TerminoEvento.ToString();
                }
                
                txtObs.Text = contrato.Observaciones;
            }
            else
            {
                MessageBox.Show("No hay respaldo disponible");
            }
        }
    }
}
