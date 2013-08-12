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
        public bool Tipo { get; set; }
        private ControlMadre _controlmadre = new ControlMadre();
        private ControlMenor _controlmenor = new ControlMenor();
        public long IdMadre { get; set; }
        public long IdMenor { get; set; }
        public long IdTutor { get; set; }
        public TipoAccion TipoAccion { get; set; }

        public frmControl()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
            {
                if (Tipo == false)
                {
                    ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

                    _controlmadre = modelocontrolmadre.Recuperar(IdSeleccionado);
                    dtpFechaProgramada.SelectedDate = _controlmadre.FechaProgramada;
                    txtPeso.Text = Convert.ToString(_controlmadre.PesoKg);
                    txtTalla.Text = Convert.ToString(_controlmadre.TallaCm);
                    dtpFechaControl.SelectedDate = _controlmadre.FechaControl;
                    lblNumeroControl.Content = _controlmadre.NumeroControl;
                    if (_controlmadre.IdTutor > 0)
                        rdbTutor.IsChecked = true;
                    else
                        rdbMadre.IsChecked = true;
                }
                else
                {
                    ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                    _controlmenor = modelocontrolmenor.Recuperar(IdSeleccionado);
                    dtpFechaProgramada.SelectedDate = _controlmenor.FechaProgramada;
                    txtPeso.Text = Convert.ToString(_controlmenor.PesoKg);
                    txtTalla.Text = Convert.ToString(_controlmenor.TallaCm);
                    dtpFechaControl.SelectedDate = _controlmenor.FechaControl;
                    lblNumeroControl.Content = _controlmenor.NumeroControl;
                    if (_controlmenor.IdTutor > 0)
                        rdbTutor.IsChecked = true;
                    else
                        rdbMadre.IsChecked = true;
                }
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Tipo == false)
            {
                ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();

                _controlmadre.IdTutor = IdTutor;
                _controlmadre.FechaProgramada = dtpFechaProgramada.SelectedDate.Value;
                _controlmadre.PesoKg = Convert.ToSingle(txtPeso.Text);
                _controlmadre.TallaCm = Convert.ToInt32(txtTalla.Text);
                _controlmadre.FechaControl = dtpFechaControl.SelectedDate.Value;

                modelocontrolmadre.Editar(IdSeleccionado, _controlmadre);
            }
            else
            {
                ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();

                _controlmenor.IdMadre = IdMadre;
                _controlmenor.IdTutor = IdTutor;
                _controlmenor.FechaProgramada = dtpFechaProgramada.SelectedDate.Value;
                _controlmenor.PesoKg = Convert.ToSingle(txtPeso.Text);
                _controlmenor.TallaCm = Convert.ToInt32(txtTalla.Text);
                _controlmenor.FechaControl = dtpFechaControl.SelectedDate.Value;

                modelocontrolmenor.Editar(IdSeleccionado, _controlmenor);
            }

            this.Close();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
