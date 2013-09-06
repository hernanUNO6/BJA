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
        public long IdFamilia { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private bool ControlPreliminar { get; set; }
        private Tutor _tutor = new Tutor();
        private GrupoFamiliar _grupofamiliar = new GrupoFamiliar();
        public bool Resultado { get; set; }

        public frmTutor()
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

            ModeloTipoParentesco modelotipoparentesco = new ModeloTipoParentesco();

            this.cboTipoParentesco.ItemsSource = modelotipoparentesco.Listar();
            this.cboTipoParentesco.DisplayMemberPath = "Descripcion";
            this.cboTipoParentesco.SelectedValuePath = "Id";

            if (IdFamilia > 0)
            {
                ModeloFamilia modelofamilia = new ModeloFamilia();
                Familia familia = new Familia();

                familia = modelofamilia.Recuperar(IdFamilia);
                this.txtPaternoFamilia.Text = familia.PrimerApellido;
                this.txtMaternoFamilia.Text = familia.SegundoApellido;
            }

            if (IdSeleccionado == 0)
            {
                this.cboTipoDocumentoIdentidad.SelectedIndex = -1;
                this.dtpFechaNacimiento.SelectedDate = DateTime.Today;
                this.cboDepartamento.SelectedIndex = -1;

                this.cboTipoParentesco.SelectedIndex = -1;
            }
            else
            {
                ModeloTutor modelotutor = new ModeloTutor();

                _tutor = modelotutor.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _tutor.DocumentoIdentidad;
                switch (_tutor.TipoDocumentoIdentidad)
                {
                    case TipoDocumentoIdentidad.CarnetIdentidad:
                        cboTipoDocumentoIdentidad.SelectedIndex = 0;
                        break;
                    case TipoDocumentoIdentidad.CertificadoNacimiento:
                        cboTipoDocumentoIdentidad.SelectedIndex = 1;
                        break;
                    case TipoDocumentoIdentidad.Pasaporte:
                        cboTipoDocumentoIdentidad.SelectedIndex = 2;
                        break;
                }
                txtPaterno.Text = _tutor.PrimerApellido;
                txtMaterno.Text = _tutor.SegundoApellido;
                txtConyuge.Text = _tutor.TercerApellido; 
                txtNombres.Text = _tutor.Nombres;
                txtNombreCompleto.Text = _tutor.NombreCompleto;
                dtpFechaNacimiento.SelectedDate = _tutor.FechaNacimiento;
                if (_tutor.Sexo == "F")
                    rdbFemenino.IsChecked = true;
                else if (_tutor.Sexo == "M")
                    rdbMasculino.IsChecked = true;
                if (_tutor.Defuncion == true)
                    chkDefuncion.IsChecked = true;
                txtLugarNacimiento.Text = _tutor.LocalidadNacimiento;
                cboDepartamento.SelectedValue = _tutor.IdDepartamento;
                RecuperarProvincias(_tutor.IdDepartamento.ToString());
                cboProvincia.SelectedValue = _tutor.IdProvincia;
                RecuperarMunicipios(_tutor.IdProvincia.ToString());
                cboMunicipio.SelectedValue = _tutor.IdMunicipio;

                ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
                _grupofamiliar = modelogrupofamiliar.RecuperarPorTutorDeFamilia(IdFamilia, IdSeleccionado);

                cboTipoParentesco.SelectedValue = _grupofamiliar.IdTipoParentesco;

                if (TipoAccion == TipoAccion.Detalle)
                {
                    cboTipoParentesco.IsEnabled = false;
                    txtDocumentoIdentidad.IsEnabled = false;
                    cboTipoDocumentoIdentidad.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtConyuge.IsEnabled = false;
                    txtNombres.IsEnabled = false;
                    dtpFechaNacimiento.IsEnabled = false;
                    rdbFemenino.IsEnabled = false;
                    rdbMasculino.IsEnabled = false;
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
                this.cboTipoParentesco.Focus();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            IdSeleccionado = 0;
            Resultado = false;

            this.Close();
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            if ((Convert.ToInt64(cboTipoParentesco.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (!(txtDocumentoIdentidad.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar documento de identidad.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if ((Convert.ToInt64(cboTipoDocumentoIdentidad.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar tipo de documento de identidad.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (!(txtPaterno.Text.Length > 0) && !(txtMaterno.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar apellidos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (!(txtNombres.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (!(txtNombreCompleto.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar nombre completo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if ((Convert.ToInt64(cboDepartamento.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar departamento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if ((Convert.ToInt64(cboProvincia.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar provincia.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if ((Convert.ToInt64(cboMunicipio.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar municipio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (!(txtLugarNacimiento.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar lugar de nacimiento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }

            if (ok == false)
            {
                switch (MessageBox.Show("¿Desea guardar los datos correspondiente a este titular de pago?", "Consulta", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        ModeloTutor modelotutor = new ModeloTutor();
                        ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();

                        _tutor.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                        switch (cboTipoDocumentoIdentidad.SelectedIndex)
                        {
                            case 0:
                                _tutor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad;
                                break;
                            case 1:
                                _tutor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CertificadoNacimiento;
                                break;
                            case 2:
                                _tutor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.Pasaporte;
                                break;
                        }
                        _tutor.PrimerApellido = txtPaterno.Text;
                        _tutor.SegundoApellido = txtMaterno.Text;
                        _tutor.TercerApellido = txtConyuge.Text;
                        _tutor.Nombres = txtNombres.Text;
                        _tutor.NombreCompleto = txtNombreCompleto.Text;
                        _tutor.FechaNacimiento = dtpFechaNacimiento.SelectedDate.Value;
                        _tutor.Defuncion = (chkDefuncion.IsChecked == true) ? true : false;
                        _tutor.Observaciones = "";
                        _tutor.IdDepartamento = Convert.ToInt64(cboDepartamento.SelectedValue);
                        _tutor.IdProvincia = Convert.ToInt64(cboProvincia.SelectedValue);
                        _tutor.IdMunicipio = Convert.ToInt64(cboMunicipio.SelectedValue);
                        _tutor.LocalidadNacimiento = txtLugarNacimiento.Text;
                        if (rdbFemenino.IsChecked == true)
                            _tutor.Sexo = "F";
                        else if (rdbFemenino.IsChecked == false)
                            _tutor.Sexo = "M";
                        else
                            _tutor.Sexo = "-";

                        _grupofamiliar.IdTipoParentesco = Convert.ToInt64(cboTipoParentesco.SelectedValue); 

                        if (IdSeleccionado > 0)
                        {
                            modelotutor.Editar(IdSeleccionado, _tutor);

                            modelogrupofamiliar.Editar(_grupofamiliar.Id, _grupofamiliar); 
                        }
                        else
                        {
                            modelotutor.Crear(_tutor);
                            IdSeleccionado = _tutor.Id;

                            _grupofamiliar.IdFamilia = IdFamilia;
                            _grupofamiliar.IdTutor = _tutor.Id;
                            _grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Tutor;

                            modelogrupofamiliar.Crear(_grupofamiliar);
                        }

                        Resultado = true;

                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        IdSeleccionado = 0;
                        Resultado = false;
                        this.Close();
                        break;
                }
            }
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
