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
    /// Lógica de interacción para frmTutor.xaml
    /// </summary>
    public partial class frmTutor : Window
    {
        public long IdSeleccionado { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private Tutor _tutor = new Tutor();

        public frmTutor()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SoporteCombo.cargarEnumerador(cboTipoDocIde, typeof(TipoDocumentoIdentidad));
            if (IdSeleccionado == 0)
            {
                this.cboTipoDocIde.SelectedIndex = 0;
                this.dtpFechaNacimiento.SelectedDate = DateTime.Today;
            }
            else
            {
                ModeloTutor modelotutor = new ModeloTutor();

                _tutor = modelotutor.Recuperar(IdSeleccionado);
                txtDocIde.Text = _tutor.DocumentoIdentidad;
                cboTipoDocIde.SelectedIndex = Convert.ToInt32(_tutor.IdTipoDocumentoIdentidad);
                txtPaterno.Text = _tutor.PrimerApellido;
                txtMaterno.Text = _tutor.SegundoApellido;
                txtNombres.Text = _tutor.Nombres;
                dtpFechaNacimiento.SelectedDate = _tutor.FechaNacimiento;
                if (_tutor.Sexo == "F")
                    rdbFemenino.IsChecked = true;
                else if (_tutor.Sexo == "M")
                    rdbMasculino.IsChecked = true;
                if (_tutor.Defuncion == true)
                    chkDefuncion.IsChecked = true;
                txtLugarNacimiento.Text = _tutor.IdLocalidadNacimiento;
                txtObservaciones.Text = _tutor.Observaciones;
                if (TipoAccion == TipoAccion.Detalle)
                {
                    txtDocIde.IsEnabled = false;
                    cboTipoDocIde.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtConyuge.IsEnabled = false;
                    txtNombres.IsEnabled = false;
                    dtpFechaNacimiento.IsEnabled = false;
                    rdbFemenino.IsEnabled = false;
                    rdbMasculino.IsEnabled = false;
                    chkDefuncion.IsEnabled = false;
                    txtLugarNacimiento.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    cmdAceptar.IsEnabled = false;
                }
            }
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloTutor modelotutor = new ModeloTutor();

            _tutor.DocumentoIdentidad = txtDocIde.Text;
            _tutor.IdTipoDocumentoIdentidad = Convert.ToInt32(cboTipoDocIde.SelectedValue);
            _tutor.PrimerApellido = txtPaterno.Text;
            _tutor.SegundoApellido = txtMaterno.Text;
            _tutor.TercerApellido = txtConyuge.Text;
            _tutor.Nombres = txtNombres.Text;
            _tutor.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
            _tutor.IdLocalidadNacimiento = txtLugarNacimiento.Text;
            _tutor.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
            _tutor.Observaciones = txtObservaciones.Text;
            if (rdbFemenino.IsChecked == true)
                _tutor.Sexo = "F";
            else if (rdbFemenino.IsChecked == false)
                _tutor.Sexo = "M";
            else
                _tutor.Sexo = "-";

            if (IdSeleccionado > 0)
                modelotutor.Editar(IdSeleccionado, _tutor);
            else
                modelotutor.Crear(_tutor);

            this.Close();
        }
    }
}
