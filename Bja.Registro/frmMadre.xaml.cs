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
        public long IdFamilia { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private bool ControlPreliminar { get; set; }
        private Madre _madre = new Madre();
        public bool Resultado { get; set; }

        public frmMadre()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ControlPreliminar = false;

            SoporteCombo.cargarEnumerador(cboTipoDocumentoIdentidad, typeof(TipoDocumentoIdentidad));
            ModeloDepartamento modelodepartamento = new ModeloDepartamento();

            this.cboDepartamento.ItemsSource = modelodepartamento.Listar();
            this.cboDepartamento.DisplayMemberPath = "Descripcion";
            this.cboDepartamento.SelectedValuePath = "Id";

            if (IdSeleccionado == 0)
            {
                this.cboTipoDocumentoIdentidad.SelectedIndex = -1;
                this.dtpFechaNacimiento.SelectedDate = DateTime.Today;
                this.cboDepartamento.SelectedIndex = -1;
            }
            else
            {
                ModeloMadre modelomadre = new ModeloMadre();
                _madre = modelomadre.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _madre.DocumentoIdentidad;
                cboTipoDocumentoIdentidad.SelectedValue = _madre.TipoDocumentoIdentidad;
                txtPaterno.Text = _madre.PrimerApellido;
                txtMaterno.Text = _madre.SegundoApellido;
                txtNombres.Text = _madre.Nombres;
                txtNombreCompleto.Text = _madre.NombreCompleto;
                dtpFechaNacimiento.SelectedDate = _madre.FechaNacimiento;
                if (_madre.Defuncion == true)
                    chkDefuncion.IsChecked = true;
                txtLugarNacimiento.Text = _madre.LocalidadNacimiento;
                cboDepartamento.SelectedValue = _madre.IdDepartamento;
                RecuperarProvincias(_madre.IdDepartamento.ToString());
                cboProvincia.SelectedValue = _madre.IdProvincia;
                RecuperarMunicipios(_madre.IdProvincia.ToString());
                cboMunicipio.SelectedValue = _madre.IdMunicipio;
                if (TipoAccion == TipoAccion.Detalle)
                {
                    txtDocumentoIdentidad.IsEnabled = false;
                    cboTipoDocumentoIdentidad.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtConyuge.IsEnabled = false;
                    txtNombres.IsEnabled = false;
                    txtNombreCompleto.IsEnabled = false;
                    dtpFechaNacimiento.IsEnabled = false;
                    chkDefuncion.IsEnabled = false;
                    txtLugarNacimiento.IsEnabled = false;
                    cboDepartamento.IsEnabled = false;
                    cboProvincia.IsEnabled = false;
                    cboMunicipio.IsEnabled = false;
                    cmdAceptar.IsEnabled = false;
                }
            }
            ControlPreliminar = true;
            if ((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion))
                this.txtDocumentoIdentidad.Focus();
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            if (!(txtDocumentoIdentidad.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar documento de identidad.", "Error");
            }
            else if (!(Convert.ToInt64(cboTipoDocumentoIdentidad.SelectedValue) >= 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar tipo de documento de identidad.", "Error");
            }
            else if (!(txtPaterno.Text.Length > 0) && !(txtMaterno.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar apellidos.", "Error");
            }
            else if (!(txtNombres.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar nombre.", "Error");
            }
            else if (!(txtNombreCompleto.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar nombre completo.", "Error");
            }
            else if (!(Convert.ToInt64(cboDepartamento.SelectedValue) >= 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar departamento.", "Error");
            }
            else if (!(Convert.ToInt64(cboProvincia.SelectedValue) >= 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar provincia.", "Error");
            }
            else if (!(Convert.ToInt64(cboMunicipio.SelectedValue) >= 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar municipio.", "Error");
            }
            else if (!(txtLugarNacimiento.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar lugar de nacimiento.", "Error");
            }

            if (ok == false)
            {
                ModeloMadre modelomadre = new ModeloMadre();

                _madre.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                _madre.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad; //cboTipoDocumentoIdentidad.SelectedValue;
                _madre.PrimerApellido = txtPaterno.Text;
                _madre.SegundoApellido = txtMaterno.Text;
                _madre.TercerApellido = txtConyuge.Text;
                _madre.Nombres = txtNombres.Text;
                _madre.NombreCompleto = txtNombreCompleto.Text;
                _madre.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
                _madre.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
                _madre.Observaciones = "";
                _madre.IdDepartamento = Convert.ToInt64(cboDepartamento.SelectedValue);
                _madre.IdProvincia = Convert.ToInt64(cboProvincia.SelectedValue);
                _madre.IdMunicipio = Convert.ToInt64(cboMunicipio.SelectedValue);
                _madre.LocalidadNacimiento = txtLugarNacimiento.Text;

                if (IdSeleccionado > 0)
                    modelomadre.Editar(IdSeleccionado, _madre);
                else
                {
                    ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
                    GrupoFamiliar grupofamiliar = new GrupoFamiliar();

                    modelomadre.Crear(_madre);

                    grupofamiliar.IdFamilia = IdFamilia;
                    grupofamiliar.IdMadre = _madre.Id;
                    grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Madre;

                    modelogrupofamiliar.Crear(grupofamiliar);
                }

                Resultado = true;

                this.Close();
            }
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;

            this.Close();
        }

        void RecuperarProvincias(string Valor)
        {
            ModeloProvincia modeloprovincia = new ModeloProvincia();
            cboProvincia.ItemsSource = modeloprovincia.GetProvinciasPorDepartamento(Valor);
            cboProvincia.DisplayMemberPath = "Descripcion";
            cboProvincia.SelectedValuePath = "Id";
        }

        private void cboDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ControlPreliminar == true)
            {
                cboProvincia.ItemsSource = null;
                cboMunicipio.ItemsSource = null;
                RecuperarProvincias(cboDepartamento.SelectedValue.ToString());
            }
        }

        void RecuperarMunicipios(string Valor)
        {
            ModeloMunicipio modelomunicipio = new ModeloMunicipio();
            cboMunicipio.ItemsSource = modelomunicipio.GetMunicipiosPorProvincias(Valor);
            cboMunicipio.DisplayMemberPath = "Descripcion";
            cboMunicipio.SelectedValuePath = "Id";
        }

        private void cboProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ControlPreliminar == true)
            {
                cboMunicipio.ItemsSource = null;
                if (Convert.ToInt64(cboProvincia.SelectedValue) > 0)
                    RecuperarMunicipios(cboProvincia.SelectedValue.ToString());
                cboMunicipio.SelectedIndex = -1;
            }
        }

    }
}
