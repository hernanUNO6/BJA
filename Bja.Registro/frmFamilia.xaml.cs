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
    /// Lógica de interacción para frmFamilia.xaml
    /// </summary>
    public partial class frmFamilia : Window
    {

        public long IdSeleccionado { get; set; }
        public TipoAccion TipoAccion { get; set; }
        public bool Resultado { get; set; }
        public long Id { get; set; }
        private bool ControlPreliminar { get; set; }
        private Familia _familia = new Familia();
        
        public frmFamilia()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ControlPreliminar = false;

            ModeloDepartamento modelodepartamento = new ModeloDepartamento();
            this.cboDepartamento.ItemsSource = modelodepartamento.Listar();
            this.cboDepartamento.DisplayMemberPath = "Descripcion";
            this.cboDepartamento.SelectedValuePath = "Id";
            if (IdSeleccionado == 0)
            {
                this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
                this.cboDepartamento.SelectedIndex = -1;
            }
            else
            {
                ModeloFamilia modelofamilia = new ModeloFamilia();

                _familia = modelofamilia.Recuperar(IdSeleccionado);
                dtpFechaInscripcion.SelectedDate = _familia.FechaInscripcion;
                txtPaterno.Text = _familia.PrimerApellido;
                txtMaterno.Text = _familia.SegundoApellido;
                txtLugar.Text = _familia.Localidad;
                cboDepartamento.SelectedValue = _familia.IdDepartamento;
                RecuperarProvincias(_familia.IdDepartamento.ToString());
                cboProvincia.SelectedValue = _familia.IdProvincia;
                RecuperarMunicipios(_familia.IdProvincia.ToString());
                cboMunicipio.SelectedValue = _familia.IdMunicipio;
                if (TipoAccion == TipoAccion.Detalle)
                {
                    dtpFechaInscripcion.IsEnabled = false;
                    txtPaterno.IsEnabled = false;
                    txtMaterno.IsEnabled = false;
                    txtLugar.IsEnabled = false;
                    cboDepartamento.IsEnabled = false;
                    cboProvincia.IsEnabled = false;
                    cboMunicipio.IsEnabled = false;
                    cmdAceptar.IsEnabled = false;
                }
            }
            ControlPreliminar = true;
            if ((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion))
                this.dtpFechaInscripcion.Focus(); 
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

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            if (!(txtPaterno.Text.Length > 0) && !(txtMaterno.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar apellidos de familia.", "Error");
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
            else if (!(txtLugar.Text.Length > 0))
            {
                ok = true;
                MessageBox.Show("Se requiere especificar lugar.", "Error");
            }

            if (ok == false)
            {
                ModeloFamilia modelofamilia = new ModeloFamilia();

                _familia.IdEstablecimientoSalud = 1;
                _familia.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
                _familia.PrimerApellido = txtPaterno.Text;
                _familia.SegundoApellido = txtMaterno.Text;
                _familia.Observaciones = "";
                _familia.IdDepartamento = Convert.ToInt64(cboDepartamento.SelectedValue);
                _familia.IdProvincia = Convert.ToInt64(cboProvincia.SelectedValue);
                _familia.IdMunicipio = Convert.ToInt64(cboMunicipio.SelectedValue);
                _familia.Localidad = txtLugar.Text;

                if (IdSeleccionado > 0)
                    modelofamilia.Editar(IdSeleccionado, _familia);
                else
                    modelofamilia.Crear(_familia);

                Id = _familia.Id;
                Resultado = true;

                this.Close();
            }
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Id = 0;
            Resultado = false;

            this.Close();
        }

    }
}
