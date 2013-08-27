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
        private ControlMadre _controlmadre = new ControlMadre();
        private ControlMenor _controlmenor = new ControlMenor();
        public long IdMadre { get; set; }
        public long IdMenor { get; set; }
        public long IdTutor { get; set; }
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

                _controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);

                this.dtpFechaProgramada.SelectedDate = _controlmadre.FechaProgramada;
                if (_controlmadre.EstadoPago == TipoEstadoPago.NoAsignable)
                {
                    this.chkDescartar.IsChecked = true; 
                    this.txtPeso.Text = "0";
                    this.txtTalla.Text = "0";
                    this.dtpFechaControl.SelectedDate = DateTime.Now;
                }
                else
                {
                    this.chkDescartar.IsChecked = true;
                    this.txtPeso.Text = Convert.ToString(_controlmadre.PesoKg);
                    this.txtTalla.Text = Convert.ToString(_controlmadre.TallaCm);
                    this.dtpFechaControl.SelectedDate = _controlmadre.FechaControl;
                }
                this.lblNumeroControl.Content = _controlmadre.NumeroControl;
            }
            else
            {
                ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                _controlmenor = modelocontrolmenor.Recuperar(IdSeleccionado);
                this.dtpFechaProgramada.SelectedDate = _controlmenor.FechaProgramada;
                this.txtPeso.Text = Convert.ToString(_controlmenor.PesoKg);
                this.txtTalla.Text = Convert.ToString(_controlmenor.TallaCm);
                this.dtpFechaControl.SelectedDate = _controlmenor.FechaControl;
                this.lblNumeroControl.Content = _controlmenor.NumeroControl;
            }
            if (TipoAccion == TipoAccion.Detalle)
            {
                this.chkDescartar.IsEnabled = false;
                this.dtpFechaProgramada.IsEnabled = false;
                this.txtTalla.IsEnabled = false;
                this.txtPeso.IsEnabled = false;
                this.dtpFechaControl.IsEnabled = false;
                this.cmdAceptar.IsEnabled = false;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (TipoControl == TipoControl.Madre)
            {
                ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

                _controlmadre.IdTutor = IdTutor;

                if (this.chkDescartar.IsChecked == true)
                {
                    _controlmadre.PesoKg = 0;
                    _controlmadre.TallaCm = 0;
                    _controlmadre.FechaControl = DateTime.Now;
                    _controlmadre.EstadoPago = TipoEstadoPago.NoAsignable;
                }
                else
                {
                    _controlmadre.FechaProgramada = this.dtpFechaProgramada.SelectedDate.Value;
                    _controlmadre.PesoKg = Convert.ToSingle(this.txtPeso.Text);
                    _controlmadre.TallaCm = Convert.ToInt32(this.txtTalla.Text);
                    _controlmadre.FechaControl = this.dtpFechaControl.SelectedDate.Value;
                    _controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                }

                modelocontrolmadre.Editar(IdSeleccionado, _controlmadre);
            }
            else
            {
                ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                _controlmenor.IdMadre = IdMadre;
                _controlmenor.IdTutor = IdTutor;
                _controlmenor.FechaProgramada = this.dtpFechaProgramada.SelectedDate.Value;
                _controlmenor.PesoKg = Convert.ToSingle(this.txtPeso.Text);
                _controlmenor.TallaCm = Convert.ToInt32(this.txtTalla.Text);
                _controlmenor.FechaControl = this.dtpFechaControl.SelectedDate.Value;

                modelocontrolmenor.Editar(IdSeleccionado, _controlmenor);
            }

            Resultado = true;

            this.Close();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;

            this.Close();
        }

    }
}
