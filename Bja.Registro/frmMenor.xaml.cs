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
    /// Lógica de interacción para frmMenor.xaml
    /// </summary>
    public partial class frmMenor : Window
    {
        public long IdSeleccionado { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private Menor _menor = new Menor();

        public frmMenor()
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
                ModeloMenor modelomenor = new ModeloMenor();

                _menor = modelomenor.Recuperar(IdSeleccionado);
                txtDocIde.Text = _menor.DocumentoIdentidad;
                cboTipoDocIde.SelectedIndex = Convert.ToInt32(_menor.IdTipoDocumentoIdentidad);
                txtPaterno.Text = _menor.PrimerApellido;
                txtMaterno.Text = _menor.SegundoApellido;
                txtNombres.Text = _menor.Nombres;
                if (_menor.Sexo == "F")
                    rdbFemenino.IsChecked = true;
                else if (_menor.Sexo == "M")
                    rdbMasculino.IsChecked = true;
                dtpFechaNacimiento.SelectedDate = _menor.FechaNacimiento;
                if (_menor.Defuncion == true)
                    chkDefuncion.IsChecked = true;
                txtLugarNacimiento.Text = _menor.IdLocalidadNacimiento;
                txtObservaciones.Text = _menor.Observaciones;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            ModeloMenor modelomenor = new ModeloMenor();

            _menor.DocumentoIdentidad = txtDocIde.Text;
            _menor.IdTipoDocumentoIdentidad = Convert.ToInt32(cboTipoDocIde.SelectedValue);
            _menor.PrimerApellido = txtPaterno.Text;
            _menor.SegundoApellido = txtMaterno.Text;
            _menor.Nombres = txtNombres.Text;
            _menor.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
            _menor.IdLocalidadNacimiento = txtLugarNacimiento.Text;
            _menor.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
            _menor.Observaciones = txtObservaciones.Text;
            if (rdbFemenino.IsChecked == true)
                _menor.Sexo = "F";
            else if (rdbFemenino.IsChecked == false)
                _menor.Sexo = "M";
            else
                _menor.Sexo = "-";

            if (IdSeleccionado > 0)
                modelomenor.Editar(IdSeleccionado, _menor);
            else
                modelomenor.Crear(_menor);

            this.Close();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
