﻿using Bja.Entidades;
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
            if (IdSeleccionado == 0)
            {
                this.cboTipoDocumentoIdentidad.SelectedIndex = -1;
                this.dtpFechaNacimiento.SelectedDate = DateTime.Today;
                this.cboDepartamento.SelectedIndex = -1;
            }
            else
            {
                ModeloTutor modelotutor = new ModeloTutor();

                _tutor = modelotutor.Recuperar(IdSeleccionado);
                txtDocumentoIdentidad.Text = _tutor.DocumentoIdentidad;
                cboTipoDocumentoIdentidad.SelectedValue = _tutor.TipoDocumentoIdentidad;
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
                if (TipoAccion == TipoAccion.Detalle)
                {
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
                this.txtDocumentoIdentidad.Focus();
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;

            this.Close();
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
                ModeloTutor modelotutor = new ModeloTutor();

                _tutor.DocumentoIdentidad = txtDocumentoIdentidad.Text;
                _tutor.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad; //cboTipoDocumentoIdentidad.SelectedValue;
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

                if (IdSeleccionado > 0)
                    modelotutor.Editar(IdSeleccionado, _tutor);
                else
                {
                    ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
                    GrupoFamiliar grupofamiliar = new GrupoFamiliar();

                    modelotutor.Crear(_tutor);

                    grupofamiliar.IdFamilia = IdFamilia;
                    grupofamiliar.IdReferencial = _tutor.Id;
                    grupofamiliar.TipoGrupoFamiliar = TipoGrupoFamiliar.Tutor;

                    modelogrupofamiliar.Crear(grupofamiliar);
                }

                Resultado = true;

                this.Close();
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
