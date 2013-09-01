using Bja.Entidades;
using Bja.Modelo;
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

namespace Bja.Registro
{
    /// <summary>
    /// Lógica de interacción para frmControl.xaml
    /// </summary>
    public partial class frmControl : Window
    {
        public long IdSeleccionado { get; set; }
        public TipoControl TipoControl { get; set; }
        private ControlMadre controlmadre = new ControlMadre();
        private ControlMenor controlmenor = new ControlMenor();
        public long IdMenor { get; set; }
        public long? IdMadre { get; set; }
        public long? IdTutor { get; set; }
        public long? IdTipoParentesco { get; set; }
        public TipoAccion TipoAccion { get; set; }
        public bool Resultado { get; set; }

        public frmControl()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (TipoControl == TipoControl.Madre)
            {
                ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

                controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);

                this.dtpFechaProgramada.SelectedDate = controlmadre.FechaProgramada;

                if (controlmadre.EstadoPago == TipoEstadoPago.NoAsignable)
                {
                    this.chkDescartar.IsChecked = true;
                    this.dtpFechaProgramada.IsEnabled = false;
                    this.dtpFechaControl.SelectedDate = DateTime.Now;
                    this.dtpFechaControl.IsEnabled = false;
                    this.txtPeso.Text = "0";
                    this.txtTalla.Text = "0";
                    this.cboMedico.IsEnabled = false;
                }
                else
                {
                    this.chkDescartar.IsChecked = false;
                    this.txtPeso.Text = Convert.ToString(controlmadre.PesoKg);
                    this.txtTalla.Text = Convert.ToString(controlmadre.TallaCm);
                    this.dtpFechaControl.SelectedDate = controlmadre.FechaControl;
                }
                this.lblNumeroControl.Content = controlmadre.NumeroControl;
            }
            else if (TipoControl == TipoControl.Menor)
            {
                ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                controlmenor = modelocontrolmenor.Recuperar(IdSeleccionado);

                this.dtpFechaProgramada.SelectedDate = controlmenor.FechaProgramada;

                if (controlmenor.EstadoPago == TipoEstadoPago.NoAsignable)
                {
                    this.chkDescartar.IsChecked = true;
                    this.dtpFechaProgramada.IsEnabled = false;
                    this.dtpFechaControl.SelectedDate = DateTime.Now;
                    this.dtpFechaControl.IsEnabled = false;
                    this.txtPeso.Text = "0";
                    this.txtTalla.Text = "0";
                    this.cboMedico.IsEnabled = false;
                }
                else
                {
                    this.txtPeso.Text = Convert.ToString(controlmenor.PesoKg);
                    this.txtTalla.Text = Convert.ToString(controlmenor.TallaCm);
                    this.dtpFechaControl.SelectedDate = controlmenor.FechaControl;
                    this.lblNumeroControl.Content = controlmenor.NumeroControl;
                }
                this.lblNumeroControl.Content = controlmenor.NumeroControl;
            }
            if (TipoAccion == TipoAccion.Detalle)
            {
                this.chkDescartar.IsEnabled = false;
                this.dtpFechaProgramada.IsEnabled = false;
                this.dtpFechaControl.IsEnabled = false;
                this.txtTalla.IsEnabled = false;
                this.txtPeso.IsEnabled = false;
                this.cboMedico.IsEnabled = false;
                this.cmdAceptar.IsEnabled = false;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (TipoControl == TipoControl.Madre)
            {
                ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

                controlmadre.IdTutor = IdTutor;
                controlmadre.IdTipoParentesco = IdTipoParentesco;

                if (this.chkDescartar.IsChecked == true)
                {
                    controlmadre.PesoKg = 0;
                    controlmadre.TallaCm = 0;
                    controlmadre.FechaControl = DateTime.Now;
                    controlmadre.EstadoPago = TipoEstadoPago.NoAsignable;
                }
                else
                {
                    controlmadre.FechaProgramada = this.dtpFechaProgramada.SelectedDate.Value;
                    controlmadre.PesoKg = Convert.ToSingle(this.txtPeso.Text);
                    controlmadre.TallaCm = Convert.ToInt32(this.txtTalla.Text);
                    controlmadre.FechaControl = this.dtpFechaControl.SelectedDate.Value;
                    controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                }

                modelocontrolmadre.Editar(IdSeleccionado, controlmadre);
            }
            else
            {
                ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                controlmenor.IdMadre = IdMadre;
                controlmenor.IdTutor = IdTutor;
                controlmenor.IdTipoParentesco = IdTipoParentesco;

                if (this.chkDescartar.IsChecked == true)
                {
                    controlmenor.PesoKg = 0;
                    controlmenor.TallaCm = 0;
                    controlmenor.FechaControl = DateTime.Now;
                    controlmenor.EstadoPago = TipoEstadoPago.NoAsignable;
                }
                else
                {
                    controlmenor.FechaProgramada = this.dtpFechaProgramada.SelectedDate.Value;
                    controlmenor.PesoKg = Convert.ToSingle(this.txtPeso.Text);
                    controlmenor.TallaCm = Convert.ToInt32(this.txtTalla.Text);
                    controlmenor.FechaControl = this.dtpFechaControl.SelectedDate.Value;
                    controlmenor.EstadoPago = TipoEstadoPago.NoPagado;
                }

                modelocontrolmenor.Editar(IdSeleccionado, controlmenor);
            }

            Resultado = true;

            this.Close();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;

            this.Close();
        }

        private void chkDescartar_Checked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaProgramada.IsEnabled = false;
            this.dtpFechaControl.IsEnabled = false;
            this.txtPeso.IsEnabled = false;
            this.txtTalla.IsEnabled = false;
            this.cboMedico.IsEnabled = false;
        }

        private void chkDescartar_Unchecked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaProgramada.IsEnabled = true;
            this.dtpFechaControl.IsEnabled = true;
            this.txtPeso.IsEnabled = true;
            this.txtTalla.IsEnabled = true;
            this.cboMedico.IsEnabled = true;
        }

    }
}
