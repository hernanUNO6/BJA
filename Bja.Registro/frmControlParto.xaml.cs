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
        private ControlMadre _controlmadre = new ControlMadre();
        public long IdMadre { get; set; }
        public long IdTutor { get; set; }
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

            _controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);
            if (_controlmadre.EstadoPago == TipoEstadoPago.NoAsignable)
            {
                this.chkDescartar.IsChecked = true;
                this.dtpFechaControl.SelectedDate = DateTime.Now;
            }
            else
            {
                this.dtpFechaControl.SelectedDate = _controlmadre.FechaControl;
            }
            if (TipoAccion == TipoAccion.Detalle)
            {
                this.dtpFechaControl.IsEnabled = false;
                this.cmdAceptar.IsEnabled = false;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

            _controlmadre.IdTutor = IdTutor;
            if (this.chkDescartar.IsChecked == true)
            {
                _controlmadre.FechaControl = DateTime.Now;
                _controlmadre.EstadoPago = TipoEstadoPago.NoAsignable;
            }
            else 
            {
                _controlmadre.FechaControl = this.dtpFechaControl.SelectedDate.Value;
                _controlmadre.EstadoPago = TipoEstadoPago.NoAplicable;
            }

            modelocontrolmadre.Editar(IdSeleccionado, _controlmadre);

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
