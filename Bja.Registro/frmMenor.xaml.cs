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
        private GrupoFamiliar _grupofamiliar = new GrupoFamiliar();
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
                ModeloMenor modelomenor = new ModeloMenor();

                _menor = modelomenor.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _menor.DocumentoIdentidad;
                switch (_menor.TipoDocumentoIdentidad)
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

                ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();

                _grupofamiliar = modelogrupofamiliar.RecuperarPorMenorDeFamilia(IdFamilia, IdSeleccionado);

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
            string s = "-";
            string ss = "";

            if (rdbFemenino.IsChecked == true)
            {
                s = "F";
                ss = "¿Desea guardar los datos correspondiente a esta niña?";
            }
            else if (rdbFemenino.IsChecked == false)
            {
                s = "M";
                ss = "¿Desea guardar los datos correspondiente a esta niño?";
            }

            if (!(txtDocumentoIdentidad.Text.Length > 0))
            {
                MessageBox.Show("Se requiere especificar documento de identidad.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if ((Convert.ToInt64(cboTipoDocumentoIdentidad.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar tipo de documento de identidad.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ok = true;
            }
            else if (Convert.ToInt32(cboTipoDocumentoIdentidad.SelectedValue) == Convert.ToInt32(TipoDocumentoIdentidad.CertificadoNacimiento))
            {
                if (!(txtOficialia.Text.Length > 0))
                {
                    MessageBox.Show("Se requiere especificar oficialía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = true;
                }
                else if (!(txtLibro.Text.Length > 0))
                {
                    MessageBox.Show("Se requiere especificar libro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = true;
                }
                else if (!(txtPartida.Text.Length > 0))
                {
                    MessageBox.Show("Se requiere especificar partida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = true;
                }
                else if (!(txtFolio.Text.Length > 0))
                {
                    MessageBox.Show("Se requiere especificar folio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = true;
                }
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
            else if (s == "-")
            {
                MessageBox.Show("Se requiere especificar sexo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if ((txtPaterno.Text != txtPaternoFamilia.Text) || (txtMaterno.Text != txtMaternoFamilia.Text))
                {
                    if (MessageBox.Show("El apellido paterno o materno no son iguales a los apellidos paterno o materno de la familia. ¿Desea continua?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        ok = true;
                }
            }

            if (ok == false)
            {
                switch (MessageBox.Show(ss, "Consulta", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        ModeloMenor modelomenor = new ModeloMenor();
                        ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();

                        _menor.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                        switch (cboTipoDocumentoIdentidad.SelectedIndex)
                        {
                            case 0:
                                _menor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad;
                                break;
                            case 1:
                                _menor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CertificadoNacimiento;
                                break;
                            case 2:
                                _menor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.Pasaporte;
                                break;
                        }
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
                        _menor.Sexo = s;

                        if (IdSeleccionado > 0)
                            modelomenor.Editar(IdSeleccionado, _menor);
                        else
                        {
                            modelomenor.Crear(_menor);
                            IdSeleccionado = _menor.Id;

                            _grupofamiliar.IdFamilia = IdFamilia;
                            _grupofamiliar.IdMenor = _menor.Id;
                            _grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Menor;

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

        private void cboTipoDocumentoIdentidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ControlPreliminar == true)
            {
                if (cboTipoDocumentoIdentidad.SelectedIndex == 1)
                {
                    this.txtOficialia.IsEnabled = true;
                    this.txtLibro.IsEnabled = true;
                    this.txtPartida.IsEnabled = true;
                    this.txtFolio.IsEnabled = true;
                }
                else
                {
                    this.txtOficialia.IsEnabled = false;
                    this.txtLibro.IsEnabled = false;
                    this.txtPartida.IsEnabled = false;
                    this.txtFolio.IsEnabled = false;
                }
            }
        }

    }
}
