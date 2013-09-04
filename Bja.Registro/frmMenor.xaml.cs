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
        public long IdFamilia { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private bool ControlPreliminar { get; set; }
        private Menor _menor = new Menor();
        public bool Resultado { get; set; }

        public frmMenor()
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
                ModeloMenor modelomenor = new ModeloMenor();

                _menor = modelomenor.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _menor.DocumentoIdentidad;
                cboTipoDocumentoIdentidad.SelectedValue = _menor.TipoDocumentoIdentidad;
                txtOficialia.Text = _menor.Oficialia;
                txtLibro.Text = _menor.Libro;
                txtPartida.Text = _menor.Partida;
                txtFolio.Text = _menor.Folio;
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
                txtLugarNacimiento.Text = _menor.LocalidadNacimiento;
                cboDepartamento.SelectedValue = _menor.IdDepartamento;
                RecuperarProvincias(_menor.IdDepartamento.ToString());
                cboProvincia.SelectedValue = _menor.IdProvincia;
                RecuperarMunicipios(_menor.IdProvincia.ToString());
                cboMunicipio.SelectedValue = _menor.IdMunicipio;
                if (TipoAccion == TipoAccion.Detalle)
                {
                    txtDocumentoIdentidad.IsEnabled = false;
                    cboTipoDocumentoIdentidad.IsEnabled = false;
                    txtOficialia.IsEnabled = false;
                    txtLibro.IsEnabled = false;
                    txtPartida.IsEnabled = false;
                    txtFolio.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtNombres.IsEnabled = false;
                    rdbFemenino.IsEnabled = false;
                    rdbMasculino.IsEnabled = false;
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
            else if (Convert.ToInt32(cboTipoDocumentoIdentidad.SelectedValue) == Convert.ToInt32(TipoDocumentoIdentidad.CertificadoNacimiento))
            {
                if (!(txtOficialia.Text.Length > 0))
                {
                    ok = true;
                    MessageBox.Show("Se requiere especificar oficialía.", "Error");
                }
                else if (!(txtLibro.Text.Length > 0))
                {
                    ok = true;
                    MessageBox.Show("Se requiere especificar libro.", "Error");
                }
                else if (!(txtPartida.Text.Length > 0))
                {
                    ok = true;
                    MessageBox.Show("Se requiere especificar partida.", "Error");
                }
                else if (!(txtFolio.Text.Length > 0))
                {
                    ok = true;
                    MessageBox.Show("Se requiere especificar folio.", "Error");
                }
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
                ModeloMenor modelomenor = new ModeloMenor();

                _menor.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                _menor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad; //cboTipoDocumentoIdentidad.SelectedValue;
                _menor.Oficialia = txtOficialia.Text;
                _menor.Libro = txtLibro.Text;
                _menor.Partida = txtPartida.Text;
                _menor.Folio = txtFolio.Text;
                _menor.PrimerApellido = txtPaterno.Text;
                _menor.SegundoApellido = txtMaterno.Text;
                _menor.Nombres = txtNombres.Text;
                _menor.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
                _menor.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
                _menor.Observaciones = "";
                _menor.IdDepartamento = Convert.ToInt64(cboDepartamento.SelectedValue);
                _menor.IdProvincia = Convert.ToInt64(cboProvincia.SelectedValue);
                _menor.IdMunicipio = Convert.ToInt64(cboMunicipio.SelectedValue);
                _menor.LocalidadNacimiento = txtLugarNacimiento.Text;
                if (rdbFemenino.IsChecked == true)
                    _menor.Sexo = "F";
                else if (rdbFemenino.IsChecked == false)
                    _menor.Sexo = "M";
                else
                    _menor.Sexo = "-";

                if (IdSeleccionado > 0)
                    modelomenor.Editar(IdSeleccionado, _menor);
                else
                {
                    ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
                    GrupoFamiliar grupofamiliar = new GrupoFamiliar();

                    modelomenor.Crear(_menor);

                    grupofamiliar.IdFamilia = IdFamilia;
                    grupofamiliar.IdMenor = _menor.Id;
                    grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Menor;

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
