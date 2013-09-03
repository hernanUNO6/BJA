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
    /// Lógica de interacción para frmControlParto.xaml
    /// </summary>
    public partial class frmControlParto : Window
    {
        public long IdSeleccionado { get; set; }
        private ControlMadre controlmadre = new ControlMadre();
        public long IdMadre { get; set; }
        public long? IdTutor { get; set; }
        public long? IdTipoParentesco { get; set; }
        public TipoAccion TipoAccion { get; set; }
        public bool Resultado { get; set; }

        public frmControlParto()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

            controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);

            if (controlmadre.EstadoPago == TipoEstadoPago.NoAsignable)
            {
                this.chkDescartar.IsChecked = true;
                this.dtpFechaControl.SelectedDate = DateTime.Now;
                this.dtpFechaControl.IsEnabled = false;
                this.cboMedico.IsEnabled = false;
            }
            else
            {
                this.chkDescartar.IsChecked = false;
                this.dtpFechaControl.SelectedDate = controlmadre.FechaControl;
            }

            if (TipoAccion == TipoAccion.Detalle)
            {
                this.chkDescartar.IsEnabled = false;
                this.dtpFechaControl.IsEnabled = false;
                this.cboMedico.IsEnabled = false;
                this.cmdAceptar.IsEnabled = false;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

            controlmadre.IdTutor = IdTutor;
            controlmadre.IdTipoParentesco = IdTipoParentesco;

            if (this.chkDescartar.IsChecked == true)
            {
                controlmadre.FechaControl = DateTime.Now;
                controlmadre.EstadoPago = TipoEstadoPago.NoAsignable;
            }
            else
            {
                controlmadre.FechaControl = this.dtpFechaControl.SelectedDate.Value;
                controlmadre.EstadoPago = TipoEstadoPago.NoAplicable;
            }

            modelocontrolmadre.Editar(IdSeleccionado, controlmadre);

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
            this.dtpFechaControl.IsEnabled = false;
            this.cboMedico.IsEnabled = false;
        }

        private void chkDescartar_Unchecked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaControl.IsEnabled = true;
            this.cboMedico.IsEnabled = true;
        }

    }
}
