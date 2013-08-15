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
    /// Lógica de interacción para frmControlPostParto.xaml
    /// </summary>
    public partial class frmControlPostParto : Window
    {
        public long IdSeleccionado { get; set; }
        private ControlMadre _controlmadre = new ControlMadre();
        public long IdMadre { get; set; }
        public long IdTutor { get; set; }
        public TipoAccion TipoAccion { get; set; }
        public bool Resultado { get; set; }

        public frmControlPostParto()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

            _controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);
            dtpFechaPrevista.SelectedDate = _controlmadre.FechaProgramada;
            dtpFechaControl.SelectedDate = _controlmadre.FechaControl;
            txtObservaciones.Text = _controlmadre.Observaciones;
            if (_controlmadre.IdTutor > 0)
                rdbTutor.IsChecked = true;
            else
                rdbMadre.IsChecked = true;
            if (TipoAccion == TipoAccion.Detalle)
            {
                dtpFechaPrevista.IsEnabled = false;
                dtpFechaControl.IsEnabled = false;
                txtObservaciones.IsEnabled = false;
                cmdAceptar.IsEnabled = false;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

            _controlmadre.IdTutor = IdTutor;
            _controlmadre.FechaProgramada = dtpFechaPrevista.SelectedDate.Value;
            _controlmadre.FechaControl = dtpFechaControl.SelectedDate.Value;
            _controlmadre.Observaciones = txtObservaciones.Text;

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
