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
    /// Lógica de interacción para frmMadre.xaml
    /// </summary>
    public partial class frmMadre : Window
    {
        public long IdSeleccionado { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private Madre _madre = new Madre();

        public frmMadre()
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
                ModeloMadre modelomadre = new ModeloMadre();
                _madre = modelomadre.Recuperar(IdSeleccionado);
                txtDocIde.Text = _madre.DocumentoIdentidad;
                cboTipoDocIde.SelectedIndex = Convert.ToInt32(_madre.IdTipoDocumentoIdentidad);
                txtPaterno.Text = _madre.PrimerApellido;
                txtMaterno.Text = _madre.SegundoApellido;
                txtNombres.Text = _madre.Nombres;
                dtpFechaNacimiento.SelectedDate = _madre.FechaNacimiento;
                if (_madre.Defuncion == true)
                    chkDefuncion.IsChecked = true;
                txtLugarNacimiento.Text = _madre.IdLocalidadNacimiento;
                txtObservaciones.Text = _madre.Observaciones;
                if (TipoAccion == TipoAccion.Detalle)
                {
                    txtDocIde.IsEnabled = false;
                    cboTipoDocIde.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtConyuge.IsEnabled = false;
                    txtNombres.IsEnabled = false;
                    dtpFechaNacimiento.IsEnabled = false;
                    chkDefuncion.IsEnabled = false;
                    txtLugarNacimiento.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    cmdAceptar.IsEnabled = false;
                }
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloMadre modelomadre = new ModeloMadre();

            _madre.DocumentoIdentidad = txtDocIde.Text;
            _madre.IdTipoDocumentoIdentidad = Convert.ToInt32(cboTipoDocIde.SelectedValue);
            _madre.PrimerApellido = txtPaterno.Text;
            _madre.SegundoApellido = txtMaterno.Text;
            _madre.TercerApellido = txtConyuge.Text;
            _madre.Nombres = txtNombres.Text;
            _madre.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
            _madre.IdLocalidadNacimiento = txtLugarNacimiento.Text;
            _madre.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
            _madre.Observaciones = txtObservaciones.Text;

            if (IdSeleccionado > 0)
                modelomadre.Editar(IdSeleccionado, _madre);
            else
                modelomadre.Crear(_madre);

            this.Close();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
