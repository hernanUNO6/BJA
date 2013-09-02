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
        public long IdFamilia { get; set; }
        public long IdSeleccionado { get; set; }
        //public long IdMadre { get; set; }
        //long IdTutor { get; set; }
        long IdCorresponsabilidadMadre { get; set; }
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

            ////this.lblDepartamento.Content = "";
            ////this.lblEstablecimiento.Content = "";

            ModeloMadre modelomadre = new ModeloMadre();
            Madre madre = new Madre();

            madre = modelomadre.Recuperar(IdSeleccionado);

            if (madre != null)
            {
                this.lblNombresMadre.Content = madre.NombreCompleto;
                this.lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
            }
            else 
            {
                this.lblNombresMadre.Content = "";
                this.lblFechaNacimientoMadre.Content = "";
            }

            ModeloTipoParentesco modelotipoparentesco = new ModeloTipoParentesco();

            this.cboTipoParentesco.ItemsSource = modelotipoparentesco.Listar();
            this.cboTipoParentesco.DisplayMemberPath = "Descripcion";
            this.cboTipoParentesco.SelectedValuePath = "Id";
            this.cboTipoParentesco.SelectedIndex = -1;

            ModeloTutor modelotutor = new ModeloTutor();
            this.cboTutor.ItemsSource = modelotutor.ListarTutoresDeUnaFamiliaParaCombo(IdFamilia);
            this.cboTutor.DisplayMemberPath = "Descripcion";
            this.cboTutor.SelectedValuePath = "Id";
            this.cboTutor.SelectedIndex = -1;

            ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();

            IdCorresponsabilidadMadre = modelocorresponsabilidadmadre.RecuperarLaUltimaCorresponsabilidadValidaDeMadre(IdSeleccionado);

            ValoresPorDefecto();

            if (IdCorresponsabilidadMadre > 0)
                RecuperarCorresponsabilidadMadre();
            else
            {
                this.txtCodigoFormulario.IsEnabled = true;
                this.dtpFechaFUM.IsEnabled = true;
                this.dtpFechaInscripcion.IsEnabled = true;
                this.dtpFechaUltimoParto.IsEnabled = true;
                this.txtNumeroEmbarazo.IsEnabled = true;
                this.chkARO.IsEnabled = true;
                this.rdbNueva.IsEnabled = true;
                this.rdbTransferencia.IsEnabled = true;
                this.cboTutor.IsEnabled = true;
                this.cboTipoParentesco.IsEnabled = true;
                this.cmdGuardar.IsEnabled = true;
            }

            if (TipoAccion == TipoAccion.Detalle)
            {
                this.txtCodigoFormulario.IsEnabled = false;
                this.dtpFechaFUM.IsEnabled = false;
                this.dtpFechaInscripcion.IsEnabled = false;
                this.dtpFechaSalida.IsEnabled = false;
                this.dtpFechaUltimoParto.IsEnabled = false;
                this.txtNumeroEmbarazo.IsEnabled = false;
                this.chkARO.IsEnabled = false;
                this.chkSalida.IsEnabled = false;
                this.rdbAborto.IsEnabled = false;
                this.rdbCumplimiento.IsEnabled = false;
                this.rdbFallecimiento.IsEnabled = false;
                this.rdbIncumplimiento.IsEnabled = false;
                this.rdbNueva.IsEnabled = false;
                this.rdbObitoFetal.IsEnabled = false;
                this.rdbTransferencia.IsEnabled = false;
                this.rdbTransferenciaSalida.IsEnabled = false;
                this.txtAutorizado.IsEnabled = false;
                this.txtCargo.IsEnabled = false;
                this.cboTutor.IsEnabled = false;
                this.cboTipoParentesco.IsEnabled = false;
                this.cmdGuardar.IsEnabled = false;
            }
        }

        void RecuperarCorresponsabilidadMadre()
        {
            ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
            CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

            corresponsabilidadmadre = modelocorresponsabilidadmadre.Recuperar(IdCorresponsabilidadMadre);

            if (corresponsabilidadmadre != null)
            {
                if (corresponsabilidadmadre.IdTutor.HasValue)
                {
                    if (corresponsabilidadmadre.IdTutor.Value > 0)
                        cboTutor.SelectedValue = corresponsabilidadmadre.IdTutor.Value;
                    if (corresponsabilidadmadre.IdTipoParentesco.HasValue)
                        cboTipoParentesco.SelectedValue = corresponsabilidadmadre.IdTipoParentesco.Value;
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
                this.chkSalida.IsEnabled = true;
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
            }
        }

        private void ValoresPorDefecto()
        {
            this.txtCodigoFormulario.Text = "";
            this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
            this.dtpFechaFUM.SelectedDate = DateTime.Today;
            this.dtpFechaUltimoParto.SelectedDate = DateTime.Today;
            this.dtpFechaSalida.SelectedDate = DateTime.Today;
            this.rdbNueva.IsChecked = true;
            this.txtNumeroEmbarazo.Text = "0";
            this.cboTutor.IsEnabled = false;
            this.cboTipoParentesco.IsEnabled = false;
            this.chkSalida.IsChecked = false;
            this.chkSalida.IsEnabled = false;
            this.dtpFechaSalida.IsEnabled = false;
            this.rdbAborto.IsChecked = false;
            this.rdbAborto.IsEnabled = false;
            this.rdbCumplimiento.IsChecked = false;
            this.rdbCumplimiento.IsEnabled = false;
            this.rdbFallecimiento.IsChecked = false;
            this.rdbFallecimiento.IsEnabled = false;
            this.rdbIncumplimiento.IsChecked = false;
            this.rdbIncumplimiento.IsEnabled = false;
            this.rdbObitoFetal.IsChecked = false;
            this.rdbObitoFetal.IsEnabled = false;
            this.rdbTransferenciaSalida.IsChecked = false;
            this.rdbTransferenciaSalida.IsEnabled = false;
            this.txtAutorizado.Text = "";
            this.txtAutorizado.IsEnabled = false;
            this.txtCargo.Text = "";
            this.txtCargo.IsEnabled = false;
        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
                ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
                CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

                bool ok = false;

                if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                    {
                        if (Convert.ToInt32(cboTipoParentesco.SelectedIndex) < 0)
                        {
                            MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
                            ok = true;
                        }
                    }

                if (ok == false)
                {
                    if (!(txtCodigoFormulario.Text.Length > 0))
                    {
                        MessageBox.Show("Se requiere especificar número de formulario.", "Error");
                        ok = true;
                    }
                }

                if (ok == false)
                {
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
                }

                if (ok == false)
                {
                    if (IdCorresponsabilidadMadre == 0)
                    {
                        corresponsabilidadmadre.IdEstablecimientoSalud = 1;
                        if (rdbNueva.IsChecked == true)
                            corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Nueva;
                        else if (rdbTransferencia.IsChecked == true)
                            corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Transferencia;

                        corresponsabilidadmadre.FechaInscripcion = this.dtpFechaInscripcion.SelectedDate.Value;
                        corresponsabilidadmadre.IdMadre = IdSeleccionado;
                        if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                        {
                            corresponsabilidadmadre.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                            corresponsabilidadmadre.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                        }
                        corresponsabilidadmadre.CodigoFormulario = this.txtCodigoFormulario.Text;
                        corresponsabilidadmadre.FechaUltimaMenstruacion = this.dtpFechaFUM.SelectedDate.Value; ;
                        corresponsabilidadmadre.FechaUltimoParto = this.dtpFechaUltimoParto.SelectedDate.Value; ;
                        corresponsabilidadmadre.NumeroEmbarazo = Convert.ToInt32(this.txtNumeroEmbarazo.Text);
                        corresponsabilidadmadre.ARO = (bool)this.chkARO.IsChecked;
                        corresponsabilidadmadre.FechaSalidaPrograma = DateTime.Now;
                        corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.EnProceso;
                        corresponsabilidadmadre.Observaciones = "";
                        corresponsabilidadmadre.AutorizadoPor = "";
                        corresponsabilidadmadre.CargoAutorizador = "";

                        modelocorresponsabilidadmadre.Crear(corresponsabilidadmadre);
                        IdCorresponsabilidadMadre = corresponsabilidadmadre.Id;

                        ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
                        DateTime fechitaControles;

                        fechitaControles = dtpFechaFUM.SelectedDate.Value;
                        fechitaControles = fechitaControles.AddMonths(-1);

                        for (int i = 0; i < CantidadDeControles; i++)
                        {
                            fechitaControles = fechitaControles.AddMonths(2);

                            ControlMadre controlmadre = new ControlMadre();
                            controlmadre.IdCorresponsabilidadMadre = IdCorresponsabilidadMadre;
                            controlmadre.IdEstablecimientoSalud = 1;
                            controlmadre.IdMedico = 1;
                            controlmadre.IdMadre = IdSeleccionado;
                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                            {
                                controlmadre.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                                controlmadre.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                            }
                            controlmadre.FechaProgramada = fechitaControles;
                            controlmadre.FechaControl = DateTime.Now;
                            controlmadre.TallaCm = 0;
                            controlmadre.PesoKg = 0;
                            controlmadre.NumeroControl = i + 1;
                            controlmadre.Observaciones = "";
                            controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                            controlmadre.TipoControlMadre = TipoControlMadre.Control;
                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Tutor;
                            else
                                controlmadre.TipoBeneficiario = TipoBeneficiario.Madre;
                            modelocontrolmadre.Crear(controlmadre);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            fechitaControles = fechitaControles.AddMonths(2);

                            ControlMadre controlmadre = new ControlMadre();
                            controlmadre.IdCorresponsabilidadMadre = IdCorresponsabilidadMadre;
                            controlmadre.IdEstablecimientoSalud = 1;
                            controlmadre.IdMedico = 1;
                            controlmadre.IdMadre = IdSeleccionado;
                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                            {
                                controlmadre.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                                controlmadre.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                            }
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
                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
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
                        RecuperarControlMadre();
                    }
                    else
                    {
                        corresponsabilidadmadre = modelocorresponsabilidadmadre.Recuperar(IdCorresponsabilidadMadre);

                        if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                        {
                            corresponsabilidadmadre.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                            corresponsabilidadmadre.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
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
                        else
                        {
                            corresponsabilidadmadre.FechaSalidaPrograma = DateTime.Now;
                            corresponsabilidadmadre.TipoSalidaMadre = TipoSalidaMadre.EnProceso;
                            corresponsabilidadmadre.AutorizadoPor = "";
                            corresponsabilidadmadre.CargoAutorizador = "";
                        }

                        modelocorresponsabilidadmadre.Editar(IdCorresponsabilidadMadre, corresponsabilidadmadre);
                    }
                }
        }

        void RecuperarControlMadre()
        {
            ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
            this.grdControl.ItemsSource = modelocontrolmadre.ListarControlesDeCorresponsabilidadDeMadre(IdCorresponsabilidadMadre);
        }

        private void VerControlPartoMadre(long IdControl, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmControlParto objControlWindow = new frmControlParto();
            objControlWindow.IdSeleccionado = IdControl;
            objControlWindow.IdMadre = IdSeleccionado;
            if (Convert.ToInt64(cboTutor.SelectedValue) > 0)
            {
                objControlWindow.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                objControlWindow.IdTipoParentesco = Convert.ToInt64(cboTipoParentesco.SelectedValue);
            }
            objControlWindow.TipoAccion = TipoAccion;
            objControlWindow.Owner = this;
            objControlWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Edicion) && (objControlWindow.Resultado == true))
                RecuperarControlMadre();
            objControlWindow = null;
        }

        private void VerControlPostPartoMadre(long IdControl, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmControlPostParto objControlWindow = new frmControlPostParto();
            objControlWindow.IdSeleccionado = IdControl;
            objControlWindow.IdMadre = IdSeleccionado;
            if (Convert.ToInt64(cboTutor.SelectedValue) > 0)
            {
                objControlWindow.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                objControlWindow.IdTipoParentesco = Convert.ToInt64(cboTipoParentesco.SelectedValue);
            }
            objControlWindow.TipoAccion = TipoAccion;
            objControlWindow.Owner = this;
            objControlWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Edicion) && (objControlWindow.Resultado == true))
                RecuperarControlMadre();
            objControlWindow = null;
        }

        private void VerControlMadre(long IdControl, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmControl objControlWindow = new frmControl();
            objControlWindow.IdSeleccionado = IdControl;
            objControlWindow.IdMadre = IdSeleccionado;
            if (Convert.ToInt64(cboTutor.SelectedValue) > 0)
            {
                objControlWindow.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                objControlWindow.IdTipoParentesco = Convert.ToInt64(cboTipoParentesco.SelectedValue);
            }
            objControlWindow.TipoAccion = TipoAccion;
            objControlWindow.TipoControl = TipoControl.Madre;
            objControlWindow.Owner = this;
            objControlWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Edicion) && (objControlWindow.Resultado == true))
                RecuperarControlMadre();
            objControlWindow = null;
        }

        private void cmdEditarControl_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;

            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
            {
                if (!(Convert.ToInt32(cboTipoParentesco.SelectedIndex) >= 0))
                {
                    MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
                    ok = true;
                }
            }
            if (ok == false)
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
                            VerControlPartoMadre(Id, TipoAccion.Edicion);
                        else if (controlmadre.TipoControlMadre == TipoControlMadre.PostParto)
                            VerControlPostPartoMadre(Id, TipoAccion.Edicion);
                        else if (controlmadre.TipoControlMadre == TipoControlMadre.Control)
                            VerControlMadre(Id, TipoAccion.Edicion);
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
                        VerControlPartoMadre(Id, TipoAccion.Detalle);
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.PostParto)
                        VerControlPostPartoMadre(Id, TipoAccion.Detalle);
                    else if (controlmadre.TipoControlMadre == TipoControlMadre.Control)
                        VerControlMadre(Id, TipoAccion.Detalle);
                }
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

    }
}
