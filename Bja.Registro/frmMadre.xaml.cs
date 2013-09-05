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
        private GrupoFamiliar _grupofamiliar = new GrupoFamiliar();
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
            }
            else
            {
                ModeloMadre modelomadre = new ModeloMadre();

                _madre = modelomadre.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _madre.DocumentoIdentidad;
                switch (_madre.TipoDocumentoIdentidad)
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

                ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();

                _grupofamiliar = modelogrupofamiliar.RecuperarPorMadreDeFamilia(IdFamilia, IdSeleccionado);

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
            ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
            GrupoFamiliar gf = new GrupoFamiliar();
            gf = modelogrupofamiliar.RecuperarTitularHabilitado(IdFamilia);
            bool ok = false;
            bool OK = false;

            if (!(txtDocumentoIdentidad.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar documento de identidad.", "Error");
            }
            else if ((Convert.ToInt64(cboTipoDocumentoIdentidad.SelectedIndex) < 0))
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
            else if ((Convert.ToInt64(cboDepartamento.SelectedIndex) < 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar departamento.", "Error");
            }
            else if ((Convert.ToInt64(cboProvincia.SelectedIndex) < 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar provincia.", "Error");
            }
            else if ((Convert.ToInt64(cboMunicipio.SelectedIndex) < 0))
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
                if (txtPaterno.Text != txtMaternoFamilia.Text)
                {
                    if (MessageBox.Show("El apellido paterno de la madre no es igual al apellido materno de la familia. ¿Desea continua?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        ok = true;
                }
            }

            if (gf == null)
            {
                if (ok == false)
                {
                    DateTime FechitaDeNacimiento;
                    DateTime FechitaActual;

                    FechitaDeNacimiento = dtpFechaNacimiento.SelectedDate.Value;
                    FechitaActual = DateTime.Now;
                    FechitaActual = FechitaActual.AddYears(-16);

                    if (FechitaDeNacimiento > FechitaActual)
                    {
                        if (MessageBox.Show("La fecha de nacimiento de la madre es: " + string.Format("{0:dd/MM/yyyy}", FechitaDeNacimiento) + ", lo cual a la fecha determina que es menor de edad. ¿Desea registrar ahora un nuevo titular de pago para la familia?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            this.Cursor = Cursors.Wait;
                            frmTutor objTutorWindow = new frmTutor();
                            objTutorWindow.IdFamilia = IdFamilia;
                            objTutorWindow.IdSeleccionado = 0;
                            objTutorWindow.TipoAccion = TipoAccion.Nuevo;
                            objTutorWindow.Owner = this;
                            objTutorWindow.ShowDialog();
                            this.Cursor = Cursors.Arrow;
                            if (objTutorWindow.Resultado == true)
                            {
                                GrupoFamiliar grupofamiliar = new GrupoFamiliar();

                                grupofamiliar = modelogrupofamiliar.RecuperarPorTutorDeFamilia(IdFamilia, objTutorWindow.IdSeleccionado);
                                grupofamiliar.TitularPagoVigente = true;
                                modelogrupofamiliar.Editar(grupofamiliar.Id, grupofamiliar);

                                OK = true;
                            }
                            objTutorWindow = null;
                        }
                    }
                }
            }

            if (ok == false)
            {
                ModeloMadre modelomadre = new ModeloMadre();

                _madre.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                switch (cboTipoDocumentoIdentidad.SelectedIndex)
                {
                    case 0:
                        _madre.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad;
                        break;
                    case 1:
                        _madre.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CertificadoNacimiento;
                        break;
                    case 2:
                        _madre.TipoDocumentoIdentidad = TipoDocumentoIdentidad.Pasaporte;
                        break;
                }
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
                    modelomadre.Crear(_madre);
                    IdSeleccionado = _madre.Id;

                    _grupofamiliar.IdFamilia = IdFamilia;
                    _grupofamiliar.IdMadre = _madre.Id;
                    _grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Madre;
                    if (gf == null)
                    {
                        if (OK == false)
                            _grupofamiliar.TitularPagoVigente = true;
                    }

                    modelogrupofamiliar.Crear(_grupofamiliar);
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
