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
    /// Lógica de interacción para frmCorresponsabilidadMadre.xaml
    /// </summary>
    public partial class frmCorresponsabilidadMadre : Window
    {
        long IdMadre { get; set; }
        long IdTutor { get; set; }
        public long IdSeleccionado { get; set; }
        public TipoAccion TipoAccion { get; set; }
        int CantidadDeControles { get; set; }  //debe definirse en la tabla parámetros.

        public frmCorresponsabilidadMadre()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CantidadDeControles = 4;

            SoporteCombo.cargarEnumerador(this.cboTipoParentesco, typeof(TipoParentesco));

            //this.lblDepartamento.Content = "";
            //this.lblEstablecimiento.Content = "";
            this.lblNombresMadre.Content = "";
            this.lblFechaNacimientoMadre.Content = "";
            this.lblNombreTutor.Content = "";
            this.lblFechaNacimientoTutor.Content = "";

            if (IdSeleccionado == 0)
            {
                this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
                this.dtpFechaFUM.SelectedDate = DateTime.Today;
                this.dtpFechaUltimoParto.SelectedDate = DateTime.Today;
                this.dtpFechaSalida.SelectedDate = DateTime.Today;
                this.rdbNueva.IsChecked = true;
                this.txtNumeroEmbarazo.Text = "0";
                this.cmdDetallesMadre.IsEnabled = false;
                this.cmdModificarMadre.IsEnabled = false;
                this.chkTutor.IsEnabled = false;
                this.cboTipoParentesco.SelectedIndex = -1;
                this.cboTipoParentesco.IsEnabled = false;
                this.cmdSeleccionarTutor.IsEnabled = false;
                this.cmdDetallesTutor.IsEnabled = false;
                this.cmdModificarTutor.IsEnabled = false;
            }
        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void RecuperarMadre()
        {
            ModeloMadre modelomadre = new ModeloMadre();
            Madre madre = new Madre();
            madre = modelomadre.Recuperar(IdMadre);
            this.lblNombresMadre.Content = madre.NombreCompleto;
            this.lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
        }

        void RecuperarTutor()
        {
            ModeloTutor modelotutor = new ModeloTutor();
            Tutor tutor = new Tutor();
            tutor = modelotutor.Recuperar(IdTutor);
            this.lblNombreTutor.Content = tutor.NombreCompleto;
            this.lblFechaNacimientoTutor.Content = string.Format("{0:dd/MM/yyyy}", tutor.FechaNacimiento);
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;

            ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
            CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

            if (this.chkTutor.IsChecked ==true)
            {
                if (!(IdTutor > 0))
                {
                    MessageBox.Show("Se requiere especificar tutor.", "Error");
                    ok = true;
                }
                else if (Convert.ToInt64(cboTipoParentesco.SelectedValue) <= 0)
                {
                    MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
                    ok = true;
                }
            }
            if (ok == false)
            {
                if (IdSeleccionado == 0)
                {
                    if (IdMadre > 0)
                    {
                        corresponsabilidadmadre.IdEstablecimientoSalud = 1;
                        if (rdbNueva.IsChecked == true)
                            corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Nueva;
                        else if (rdbTransferencia.IsChecked == true)
                            corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Transferencia;

                        corresponsabilidadmadre.FechaInscripcion = this.dtpFechaInscripcion.SelectedDate.Value;
                        corresponsabilidadmadre.IdMadre = IdMadre;
                        if (this.chkTutor.IsChecked == true)
                        {
                            corresponsabilidadmadre.IdTutor = IdTutor;
                            if (Convert.ToInt64(cboTipoParentesco.SelectedValue) > 0)
                                corresponsabilidadmadre.TipoParentesco = (TipoParentesco)cboTipoParentesco.SelectedValue;
                        }
                        corresponsabilidadmadre.CodigoFormulario = this.txtCodigoFormulario.Text;
                        corresponsabilidadmadre.FechaUltimaMenstruacion = this.dtpFechaFUM.SelectedDate.Value; ;
                        corresponsabilidadmadre.FechaUltimoParto = this.dtpFechaUltimoParto.SelectedDate.Value; ;
                        corresponsabilidadmadre.NumeroEmbarazo = Convert.ToInt32(this.txtNumeroEmbarazo.Text);
                        corresponsabilidadmadre.ARO = (bool)this.chkARO.IsChecked;
                        corresponsabilidadmadre.FechaSalidaPrograma = DateTime.Now;
                        corresponsabilidadmadre.TipoSalidaMadre = 0;
                        corresponsabilidadmadre.Observaciones = "";
                        corresponsabilidadmadre.AutorizadoPor = "";
                        corresponsabilidadmadre.CargoAutorizador = "";
                        corresponsabilidadmadre.DireccionMadre = "";
                        corresponsabilidadmadre.DireccionTutor = "";

                        modelocorresponsabilidadmadre.Crear(corresponsabilidadmadre);
                        IdSeleccionado = corresponsabilidadmadre.Id;

                        //generamos los 4 registros de controles

                        ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
                        DateTime fechitaControles;

                        fechitaControles = dtpFechaFUM.SelectedDate.Value;
                        fechitaControles = fechitaControles.AddMonths(-1);

                        for (int i = 0; i < CantidadDeControles; i++)
                        {
                            fechitaControles = fechitaControles.AddMonths(2);

                            ControlMadre controlmadre = new ControlMadre();
                            controlmadre.IdCorresponsabilidadMadre = IdSeleccionado;
                            controlmadre.IdMedico = 1;
                            controlmadre.IdMadre = IdMadre;
                            controlmadre.IdTutor = IdTutor;
                            controlmadre.FechaProgramada = fechitaControles;
                            controlmadre.FechaControl = DateTime.Now;
                            controlmadre.TallaCm = 0;
                            controlmadre.PesoKg = 0;
                            controlmadre.NumeroControl = i + 1;
                            controlmadre.Observaciones = "";
                            controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                            controlmadre.TipoControlMadre = TipoControlMadre.Control;
                            if (IdTutor > 0)
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Tutor;
                            else
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Madre;
                            modelocontrolmadre.Crear(controlmadre);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            fechitaControles = fechitaControles.AddMonths(2);

                            ControlMadre controlmadre = new ControlMadre();
                            controlmadre.IdCorresponsabilidadMadre = IdSeleccionado;
                            controlmadre.IdMedico = 1;
                            controlmadre.IdMadre = IdMadre;
                            controlmadre.IdTutor = IdTutor;
                            controlmadre.FechaProgramada = fechitaControles;
                            controlmadre.FechaControl = DateTime.Now;
                            controlmadre.TallaCm = 0;
                            controlmadre.PesoKg = 0;
                            controlmadre.NumeroControl = CantidadDeControles + i + 1;
                            controlmadre.Observaciones = "";
                            if (i == 0)
                            {
                                controlmadre.EstadoPago = TipoEstadoPago.NoAplicable;
                                controlmadre.TipoControlMadre = TipoControlMadre.Parto;
                            }
                            else
                            {
                                controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                                controlmadre.TipoControlMadre = TipoControlMadre.PostParto;
                            }
                            if (IdTutor > 0)
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Tutor;
                            else
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Madre;
                            modelocontrolmadre.Crear(controlmadre);
                        }

                        this.txtCodigoFormulario.IsEnabled = false;
                        this.dtpFechaInscripcion.IsEnabled = false;
                        this.rdbNueva.IsEnabled = false;
                        this.rdbTransferencia.IsEnabled = false;
                        this.dtpFechaFUM.IsEnabled = false;
                        this.dtpFechaUltimoParto.IsEnabled = false;
                        this.txtNumeroEmbarazo.IsEnabled = false;
                        this.chkARO.IsEnabled = false;
                        this.cmdGuardar.IsEnabled = false;
                        RecuperarControlMadre();
                    }
                }
                else
                {
                    ok = false;

                    if (chkSalida.IsChecked == true)
                    {
                        if ((this.rdbTransferenciaSalida.IsChecked == false) && (this.rdbObitoFetal.IsChecked == false) && (this.rdbIncumplimiento.IsChecked == false) && (this.rdbFallecimiento.IsChecked == false) && (this.rdbCumplimiento.IsChecked == false) && (this.rdbAborto.IsChecked == false))
                        {
                            MessageBox.Show("Se requiere especificar causa.", "Error");
                            ok = true;
                        }
                        else if (!(txtAutorizado.Text.Length > 0))
                        {
                            MessageBox.Show("Se requiere especificar autorizador.", "Error");
                            ok = true;
                        }
                        else if (!(txtCargo.Text.Length > 0))
                        {
                            MessageBox.Show("Se requiere especificar cargo.", "Error");
                            ok = true;
                        }
                    }

                    if (ok == false)
                    {
                        corresponsabilidadmadre = modelocorresponsabilidadmadre.Recuperar(IdSeleccionado);
                        corresponsabilidadmadre.IdMadre = IdMadre;
                        if (this.chkTutor.IsChecked == true)
                        {
                            corresponsabilidadmadre.IdTutor = IdTutor;
                            if (Convert.ToInt64(cboTipoParentesco.SelectedValue) > 0)
                                corresponsabilidadmadre.TipoParentesco = (TipoParentesco)cboTipoParentesco.SelectedValue;
                        }
                        if (this.chkSalida.IsChecked == true)
                        {
                            corresponsabilidadmadre.FechaSalidaPrograma = this.dtpFechaSalida.SelectedDate.Value;
                            if (this.rdbAborto.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.Aborto;
                            else if (this.rdbCumplimiento.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.Cumplimiento;
                            else if (this.rdbFallecimiento.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.Fallecimiento;
                            else if (this.rdbIncumplimiento.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.Incumplimiento;
                            else if (this.rdbObitoFetal.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.ObitoFetal;
                            else if (this.rdbTransferenciaSalida.IsChecked == true)
                                corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.Transferencia;
                            corresponsabilidadmadre.AutorizadoPor = this.txtAutorizado.Text;
                            corresponsabilidadmadre.CargoAutorizador = this.txtCargo.Text;
                        }
                        corresponsabilidadmadre.Observaciones = "";
                        modelocorresponsabilidadmadre.Editar(IdSeleccionado, corresponsabilidadmadre);
                    }
                }
            }
        }

        void RecuperarControlMadre()
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
            this.grdControl.ItemsSource = modelocontrolmadre.ListarControlesDeCorresponsabilidadDeMadre(IdSeleccionado);
        }

        private void cmdEditarControl_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;

                if (Id > 0)
                {
                    ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
                    ControlMadre controlmadre = new ControlMadre();
                    controlmadre = modelocontrolmadre.Recuperar(Id);
                    if (controlmadre.TipoControlMadre == TipoControlMadre.Parto)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControlParto objControlWindow = new frmControlParto();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Edicion;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre();
                        objControlWindow = null;
                    }
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.PostParto)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControlPostParto objControlWindow = new frmControlPostParto();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Edicion;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre();
                        objControlWindow = null;
                    }
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.Control)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControl objControlWindow = new frmControl();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Edicion;
                        objControlWindow.TipoControl = TipoControl.Madre;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre();
                        objControlWindow = null;
                    }
                }
            }
        }

        private void cmdDetalleControl_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;

                if (Id > 0)
                {
                    ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
                    ControlMadre controlmadre = new ControlMadre();
                    controlmadre = modelocontrolmadre.Recuperar(Id);
                    if (controlmadre.TipoControlMadre == TipoControlMadre.Parto)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControlParto objControlWindow = new frmControlParto();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Detalle;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        objControlWindow = null;
                    }
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.PostParto)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControlPostParto objControlWindow = new frmControlPostParto();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Detalle;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        objControlWindow = null;
                    }
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.Control)
                    {
                        this.Cursor = Cursors.Wait;
                        frmControl objControlWindow = new frmControl();
                        objControlWindow.IdSeleccionado = Id;
                        objControlWindow.IdMadre = IdMadre;
                        objControlWindow.IdTutor = IdTutor;
                        objControlWindow.TipoAccion = TipoAccion.Detalle;
                        objControlWindow.TipoControl = TipoControl.Madre;
                        objControlWindow.Owner = this;
                        objControlWindow.ShowDialog();
                        this.Cursor = Cursors.Arrow;
                        objControlWindow = null;
                    }
                }
            }
        }

        private void cmdSeleccionarMadre_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMadres = new frmLista();

            formularioListaMadres.NuevoRegistro += formularioListaMadres_NuevoRegistro;
            formularioListaMadres.DetallesRegistro += formularioListaMadres_DetallesRegistro;
            formularioListaMadres.ModificarRegistro += formularioListaMadres_ModificarRegistro;
            formularioListaMadres.BorrarRegistro += formularioListaMadres_BorrarRegistro;
            formularioListaMadres.SeleccionarRegistro += formularioListaMadres_SeleccionarRegistro;

            ModeloMadre modelomadre = new ModeloMadre();

            formularioListaMadres.proveedorDatos = modelomadre;
            formularioListaMadres.titulo = "Madres";
            formularioListaMadres.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_NuevoRegistro(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.TipoAccion = TipoAccion.Nuevo;
            objMadreWindow.IdSeleccionado = 0;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            objMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdSeleccionado = fe.id;
            objMadreWindow.TipoAccion = TipoAccion.Detalle;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            objMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdSeleccionado = fe.id;
            objMadreWindow.TipoAccion = TipoAccion.Edicion;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            objMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_BorrarRegistro(object sender, IdentidadEventArgs fe)
        {
            //throw new NotImplementedException();
            MessageBox.Show("En proceso para validación.", "Mensaje");
        }

        void formularioListaMadres_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        {
            IdMadre = fe.id;
            RecuperarMadre();
            RecuperarCorresponsabilidadMadre();
            this.cmdSeleccionarMadre.IsEnabled = false;
            this.cmdModificarMadre.IsEnabled = true;
            this.cmdDetallesMadre.IsEnabled = true;
            this.chkTutor.IsEnabled = true;
        }

        void RecuperarCorresponsabilidadMadre()
        {
            ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
            CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

            corresponsabilidadmadre = modelocorresponsabilidadmadre.RecuperarElUltimoValido(IdMadre);
            if (corresponsabilidadmadre != null)
            {
                IdSeleccionado = corresponsabilidadmadre.Id;
                if (corresponsabilidadmadre.IdTutor > 0)
                {
                    IdTutor = corresponsabilidadmadre.IdTutor;
                    cboTipoParentesco.SelectedValue = corresponsabilidadmadre.TipoParentesco;
                    RecuperarTutor();
                    this.chkTutor.IsChecked = true;
                    this.cmdDetallesTutor.IsEnabled = true;
                    this.cmdModificarTutor.IsEnabled = true;
                }
                this.rdbNueva.IsEnabled = false;
                this.rdbTransferencia.IsEnabled = false;
                if (corresponsabilidadmadre.TipoInscripcionMadre == TipoInscripcion.Nueva)
                    this.rdbNueva.IsChecked = true;
                else if (corresponsabilidadmadre.TipoInscripcionMadre == TipoInscripcion.Transferencia)
                    this.rdbTransferencia.IsChecked = true;
                this.txtCodigoFormulario.Text = corresponsabilidadmadre.CodigoFormulario;
                this.txtCodigoFormulario.IsEnabled = false;
                this.dtpFechaInscripcion.SelectedDate = corresponsabilidadmadre.FechaInscripcion;
                this.dtpFechaInscripcion.IsEnabled = false;
                this.dtpFechaFUM.SelectedDate = corresponsabilidadmadre.FechaUltimaMenstruacion;
                this.dtpFechaFUM.IsEnabled = false;
                this.dtpFechaUltimoParto.SelectedDate = corresponsabilidadmadre.FechaUltimoParto;
                this.dtpFechaUltimoParto.IsEnabled = false;
                this.txtNumeroEmbarazo.Text = corresponsabilidadmadre.NumeroEmbarazo.ToString();
                this.txtNumeroEmbarazo.IsEnabled = false;
                this.chkARO.IsChecked = corresponsabilidadmadre.ARO;
                this.chkARO.IsEnabled = false;
                RecuperarControlMadre();
                if (corresponsabilidadmadre.TipoSalidaMadre > 0)
                {
                    this.chkSalida.IsChecked = true;
                    this.dtpFechaSalida.SelectedDate = corresponsabilidadmadre.FechaSalidaPrograma;
                    this.dtpFechaSalida.IsEnabled = true;
                    this.rdbAborto.IsEnabled = true;
                    this.rdbCumplimiento.IsEnabled = true;
                    this.rdbFallecimiento.IsEnabled = true;
                    this.rdbIncumplimiento.IsEnabled = true;
                    this.rdbObitoFetal.IsEnabled = true;
                    this.rdbTransferenciaSalida.IsEnabled = true;
                    switch (corresponsabilidadmadre.TipoSalidaMadre)
                    {
                        case TipoSalidaMadre.Aborto:
                            this.rdbAborto.IsChecked = true;
                            break;
                        case TipoSalidaMadre.Cumplimiento:
                            this.rdbCumplimiento.IsChecked = true;
                            break;
                        case TipoSalidaMadre.Fallecimiento:
                            this.rdbFallecimiento.IsChecked = true;
                            break;
                        case TipoSalidaMadre.Incumplimiento:
                            this.rdbIncumplimiento.IsChecked = true;
                            break;
                        case TipoSalidaMadre.ObitoFetal:
                            this.rdbObitoFetal.IsChecked = true;
                            break;
                        case TipoSalidaMadre.Transferencia:
                            this.rdbTransferenciaSalida.IsChecked = true;
                            break;
                    }
                    this.txtAutorizado.Text = corresponsabilidadmadre.AutorizadoPor;
                    this.txtAutorizado.IsEnabled = true;
                    this.txtCargo.Text = corresponsabilidadmadre.CargoAutorizador;
                    this.txtCargo.IsEnabled = true;
                }
                else
                    this.dtpFechaSalida.SelectedDate = DateTime.Now;
            }
        }

        private void cmdSeleccionarTutor_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaTutor = new frmLista();

            formularioListaTutor.NuevoRegistro += formularioListaTutor_NuevoRegistro;
            formularioListaTutor.DetallesRegistro += formularioListaTutor_DetallesRegistro;
            formularioListaTutor.ModificarRegistro += formularioListaTutor_ModificarRegistro;
            formularioListaTutor.BorrarRegistro += formularioListaTutor_BorrarRegistro;
            formularioListaTutor.SeleccionarRegistro += formularioListaTutor_SeleccionarRegistro;

            ModeloTutor modelomadre = new ModeloTutor();

            formularioListaTutor.proveedorDatos = modelomadre;
            formularioListaTutor.titulo = "Tutores";
            formularioListaTutor.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutor_NuevoRegistro(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.TipoAccion = TipoAccion.Nuevo;
            objTutorWindow.IdSeleccionado = 0;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutor_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdSeleccionado = fe.id;
            objTutorWindow.TipoAccion = TipoAccion.Detalle;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutor_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdSeleccionado = fe.id;
            objTutorWindow.TipoAccion = TipoAccion.Edicion;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutor_BorrarRegistro(object sender, IdentidadEventArgs fe)
        {
            //throw new NotImplementedException();
            MessageBox.Show("En proceso para validación.", "Mensaje");
        }

        void formularioListaTutor_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        {
            IdTutor = fe.id;
            RecuperarTutor();
        }

        private void cmdDetallesMadre_Click(object sender, RoutedEventArgs e)
        {
            if (IdMadre > 0)
            {
                this.Cursor = Cursors.Wait;
                frmMadre objMadreWindow = new frmMadre();
                objMadreWindow.IdSeleccionado = IdMadre;
                objMadreWindow.TipoAccion = TipoAccion.Detalle;
                objMadreWindow.Owner = this;
                objMadreWindow.ShowDialog();
                objMadreWindow = null;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void cmdModificarMadre_Click(object sender, RoutedEventArgs e)
        {
            if (IdMadre > 0)
            {
                this.Cursor = Cursors.Wait;
                frmMadre objMadreWindow = new frmMadre();
                objMadreWindow.IdSeleccionado = IdMadre;
                objMadreWindow.TipoAccion = TipoAccion.Edicion;
                objMadreWindow.Owner = this;
                objMadreWindow.ShowDialog();
                objMadreWindow = null;
                this.Cursor = Cursors.Arrow;
                RecuperarMadre();
            }
        }

        private void cmdDetallesTutor_Click(object sender, RoutedEventArgs e)
        {
            if (IdTutor > 0)
            {
                this.Cursor = Cursors.Wait;
                frmTutor objTutorWindow = new frmTutor();
                objTutorWindow.IdSeleccionado = IdTutor;
                objTutorWindow.TipoAccion = TipoAccion.Detalle;
                objTutorWindow.Owner = this;
                objTutorWindow.ShowDialog();
                objTutorWindow = null;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void cmdModificarTutor_Click(object sender, RoutedEventArgs e)
        {
            if (IdTutor > 0)
            {
                this.Cursor = Cursors.Wait;
                frmTutor objTutorWindow = new frmTutor();
                objTutorWindow.IdSeleccionado = IdTutor;
                objTutorWindow.TipoAccion = TipoAccion.Edicion;
                objTutorWindow.Owner = this;
                objTutorWindow.ShowDialog();
                objTutorWindow = null;
                this.Cursor = Cursors.Arrow;
                RecuperarTutor();
            }
        }

        private void chkTutor_Checked(object sender, RoutedEventArgs e)
        {
            if (IdMadre > 0)
            {
                this.cboTipoParentesco.IsEnabled = true;
                this.cmdSeleccionarTutor.IsEnabled = true;
                this.cmdDetallesTutor.IsEnabled = true;
                this.cmdModificarTutor.IsEnabled = true;
            }
        }

        private void chkTutor_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IdMadre > 0)
            {
                this.cboTipoParentesco.IsEnabled = false;
                this.cmdSeleccionarTutor.IsEnabled = false;
                this.cmdDetallesTutor.IsEnabled = false;
                this.cmdModificarTutor.IsEnabled = false;
                IdTutor = 0;
                this.lblNombreTutor.Content = "";
                this.lblFechaNacimientoTutor.Content = "";
                this.cboTipoParentesco.SelectedIndex = -1;
            }
        }

        private void chkSalida_Checked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaSalida.IsEnabled = true;
            this.rdbAborto.IsEnabled = true;
            this.rdbCumplimiento.IsEnabled = true;
            this.rdbFallecimiento.IsEnabled = true;
            this.rdbIncumplimiento.IsEnabled = true;
            this.rdbObitoFetal.IsEnabled = true;
            this.rdbTransferenciaSalida.IsEnabled = true;
            this.txtAutorizado.IsEnabled = true;
            this.txtCargo.IsEnabled = true;
        }

        private void chkSalida_Unchecked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaSalida.IsEnabled = false;
            this.rdbAborto.IsEnabled = false;
            this.rdbCumplimiento.IsEnabled = false;
            this.rdbFallecimiento.IsEnabled = false;
            this.rdbIncumplimiento.IsEnabled = false;
            this.rdbObitoFetal.IsEnabled = false;
            this.rdbTransferenciaSalida.IsEnabled = false;
            this.txtAutorizado.IsEnabled = false;
            this.txtCargo.IsEnabled = false;
            this.dtpFechaSalida.SelectedDate = DateTime.Today;
            this.rdbAborto.IsChecked = false;
            this.rdbCumplimiento.IsChecked = false;
            this.rdbFallecimiento.IsChecked = false;
            this.rdbIncumplimiento.IsChecked = false;
            this.rdbObitoFetal.IsChecked = false;
            this.rdbTransferenciaSalida.IsChecked = false;
            this.txtAutorizado.Text = "";
            this.txtCargo.Text = "";
        }

        ////se esta adicionando esta parte RENAN
        //private void cmdPrueba_Click(object sender, RoutedEventArgs e)
        //{
        //    DateTime fechitaControles;
        //    string strMesesProgramados = "";
        //    string strMesesPerdidos = "";

        //    fechitaControles = dtpFechaFUM.SelectedDate.Value;
        //    fechitaControles = fechitaControles.AddMonths(-1);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        fechitaControles = fechitaControles.AddMonths(2);
        //        //strMesesProgramados += "01/" + fechitaControles.Month.ToString() + "/" + fechitaControles.Year.ToString() + "      "; FECHA COMPLETA
        //        strMesesProgramados += string.Format("{0:MM/yyyy}", fechitaControles) + "            ";

        //        //controles perdidos---------------------------------------------------------------
        //        if ((fechitaControles.Month >= dtpFechaInscripcion.SelectedDate.Value.Month) && (fechitaControles.Year >= dtpFechaInscripcion.SelectedDate.Value.Year))
        //        {
        //            //strMesesPerdidos += fechitaControles.Date; FECHA COMPLETA
        //            strMesesPerdidos += string.Format("{0:MM/yyyy}", fechitaControles) + "        ";
        //        }
        //    }
        //    lblFechas.Content = "Fechas: " + strMesesProgramados;
        //    lblMesesPerdidos.Content = "A Pagar: " + strMesesPerdidos;
        //}

    }
}
