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
    /// Lógica de interacción para frmCorresponsabilidadMenor.xaml
    /// </summary>
    public partial class frmCorresponsabilidadMenor : Window
    {
        public long IdFamilia { get; set; }
        public long IdSeleccionado { get; set; }
        //public long IdMenor { get; set; }
        //long IdMadre { get; set; }
        //long IdTutor { get; set; }
        long IdCorresponsabilidadMenor { get; set; }
        public TipoAccion TipoAccion { get; set; }
        int CantidadDeControles { get; set; }  //debe definirse en la tabla parámetros.

        public frmCorresponsabilidadMenor()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CantidadDeControles = 12;

            ////this.lblDepartamento.Content = "";
            ////this.lblEstablecimiento.Content = "";

            ModeloMenor modelomenor = new ModeloMenor();
            Menor menor = new Menor();

            menor = modelomenor.Recuperar(IdSeleccionado);

            if (menor != null)
            {
                this.lblNombresMenor.Content = menor.PrimerApellido + " " + menor.SegundoApellido + " " + menor.Nombres;
                this.lblFechaNacimientoMenor.Content = string.Format("{0:dd/MM/yyyy}", menor.FechaNacimiento);
            }
            else
            {
                this.lblNombresMenor.Content = "";
                this.lblFechaNacimientoMenor.Content = "";
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

            ModeloMadre modelomadre = new ModeloMadre();
            this.cboMadre.ItemsSource = modelomadre.ListarMadresDeUnaFamiliaParaCombo(IdFamilia);
            this.cboMadre.DisplayMemberPath = "Descripcion";
            this.cboMadre.SelectedValuePath = "Id";
            this.cboMadre.SelectedIndex = -1;

            ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();

            IdCorresponsabilidadMenor = modelocorresponsabilidadmenor.RecuperarLaUltimaCorresponsabilidadValidaDeMenor(IdSeleccionado);

            ValoresPorDefecto();

            if (IdCorresponsabilidadMenor > 0)
                RecuperarCorresponsabilidadMenor();
            else
            {
                this.txtCodigoFormulario.IsEnabled = true;
                this.dtpFechaInscripcion.IsEnabled = true;
                this.rdbNueva.IsEnabled = true;
                this.rdbTransferencia.IsEnabled = true;
                this.cboMadre.IsEnabled = true;
                this.cboTutor.IsEnabled = true;
                this.cboTipoParentesco.IsEnabled = true;
                this.cmdGuardar.IsEnabled = true;
            }

            if (TipoAccion == TipoAccion.Detalle)
            {
                this.txtCodigoFormulario.IsEnabled = false;
                this.dtpFechaInscripcion.IsEnabled = false;
                this.dtpFechaSalida.IsEnabled = false;
                this.chkSalida.IsEnabled = false;
                this.rdbCumplimiento.IsEnabled = false;
                this.rdbFallecimiento.IsEnabled = false;
                this.rdbIncumplimiento.IsEnabled = false;
                this.rdbNueva.IsEnabled = false;
                this.rdbTransferencia.IsEnabled = false;
                this.rdbTransferenciaSalida.IsEnabled = false;
                this.txtAutorizado.IsEnabled = false;
                this.txtCargo.IsEnabled = false;
                this.cboMadre.IsEnabled = false;
                this.cboTutor.IsEnabled = false;
                this.cboTipoParentesco.IsEnabled = false;
                this.cmdGuardar.IsEnabled = false;
            }
        }

        private void ValoresPorDefecto()
        {
            this.txtCodigoFormulario.Text = "";
            this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
            this.dtpFechaSalida.SelectedDate = DateTime.Today;
            this.rdbNueva.IsChecked = true;
            this.cboMadre.IsEnabled = false;
            this.cboTutor.IsEnabled = false;
            this.cboTipoParentesco.IsEnabled = false;
            this.chkSalida.IsChecked = false;
            this.chkSalida.IsEnabled = false;
            this.dtpFechaSalida.IsEnabled = false;
            this.rdbCumplimiento.IsChecked = false;
            this.rdbCumplimiento.IsEnabled = false;
            this.rdbFallecimiento.IsChecked = false;
            this.rdbFallecimiento.IsEnabled = false;
            this.rdbIncumplimiento.IsChecked = false;
            this.rdbIncumplimiento.IsEnabled = false;
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

        void RecuperarCorresponsabilidadMenor()
        {
            ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
            CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();

            corresponsabilidadmenor = modelocorresponsabilidadmenor.Recuperar(IdCorresponsabilidadMenor);

            if (corresponsabilidadmenor != null)
            {
                if (corresponsabilidadmenor.IdMadre.HasValue)
                {
                    if (corresponsabilidadmenor.IdMadre.Value > 0)
                        cboMadre.SelectedValue = corresponsabilidadmenor.IdMadre.Value;
                }

                if (corresponsabilidadmenor.IdTutor.HasValue)
                {
                    if (corresponsabilidadmenor.IdTutor.Value > 0)
                        cboTutor.SelectedValue = corresponsabilidadmenor.IdTutor.Value;
                    if (corresponsabilidadmenor.IdTipoParentesco.HasValue)
                        cboTipoParentesco.SelectedValue = corresponsabilidadmenor.IdTipoParentesco.Value;
                }
                this.rdbNueva.IsEnabled = false;
                this.rdbTransferencia.IsEnabled = false;
                if (corresponsabilidadmenor.TipoInscripcionMenor == TipoInscripcion.Nueva)
                    this.rdbNueva.IsChecked = true;
                else if (corresponsabilidadmenor.TipoInscripcionMenor == TipoInscripcion.Transferencia)
                    this.rdbTransferencia.IsChecked = true;
                this.txtCodigoFormulario.Text = corresponsabilidadmenor.CodigoFormulario;
                this.txtCodigoFormulario.IsEnabled = false;
                this.dtpFechaInscripcion.SelectedDate = corresponsabilidadmenor.FechaInscripcion;
                this.dtpFechaInscripcion.IsEnabled = false;
                RecuperarControlMenor();
                this.chkSalida.IsEnabled = true;
                if (corresponsabilidadmenor.TipoSalidaMenor > 0)
                {
                    this.chkSalida.IsChecked = true;
                    this.dtpFechaSalida.SelectedDate = corresponsabilidadmenor.FechaSalidaPrograma;
                    this.dtpFechaSalida.IsEnabled = true;
                    this.rdbCumplimiento.IsEnabled = true;
                    this.rdbFallecimiento.IsEnabled = true;
                    this.rdbIncumplimiento.IsEnabled = true;
                    this.rdbTransferenciaSalida.IsEnabled = true;
                    switch (corresponsabilidadmenor.TipoSalidaMenor)
                    {
                        case TipoSalidaMenor.Cumplimiento:
                            this.rdbCumplimiento.IsChecked = true;
                            break;
                        case TipoSalidaMenor.Fallecimiento:
                            this.rdbFallecimiento.IsChecked = true;
                            break;
                        case TipoSalidaMenor.Incumplimiento:
                            this.rdbIncumplimiento.IsChecked = true;
                            break;
                        case TipoSalidaMenor.Transferencia:
                            this.rdbTransferenciaSalida.IsChecked = true;
                            break;
                    }
                    this.txtAutorizado.Text = corresponsabilidadmenor.AutorizadoPor;
                    this.txtAutorizado.IsEnabled = true;
                    this.txtCargo.Text = corresponsabilidadmenor.CargoAutorizador;
                    this.txtCargo.IsEnabled = true;
                }
            }
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
            CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();

            bool ok = false;

            if ((Convert.ToInt64(cboMadre.SelectedIndex) < 0) && (Convert.ToInt64(cboTutor.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar madre y/o tutor para el menor.", "Error");
                ok = true;
            }

            if (ok == false)
                {
                    if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                    {
                        if (Convert.ToInt32(cboTipoParentesco.SelectedIndex) < 0)
                        {
                            MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
                            ok = true;
                        }
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
                    if (IdCorresponsabilidadMenor == 0)
                    {
                        corresponsabilidadmenor.IdEstablecimientoSalud = 1;
                        if (rdbNueva.IsChecked == true)
                            corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Nueva;
                        else if (rdbTransferencia.IsChecked == true)
                            corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Transferencia;

                        corresponsabilidadmenor.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
                        corresponsabilidadmenor.IdMenor = IdSeleccionado;

                        if (Convert.ToInt64(cboMadre.SelectedIndex) >= 0)
                            corresponsabilidadmenor.IdMadre = Convert.ToInt64(cboMadre.SelectedValue);

                        if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                        {
                            corresponsabilidadmenor.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                            corresponsabilidadmenor.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                        }

                        corresponsabilidadmenor.CodigoFormulario = txtCodigoFormulario.Text;
                        corresponsabilidadmenor.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
                        corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.EnProceso;
                        corresponsabilidadmenor.Observaciones = "";
                        corresponsabilidadmenor.AutorizadoPor = txtAutorizado.Text;
                        corresponsabilidadmenor.CargoAutorizador = txtCargo.Text;

                        modelocorresponsabilidadmenor.Crear(corresponsabilidadmenor);
                        IdCorresponsabilidadMenor = corresponsabilidadmenor.Id;

                        ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
                        DateTime fechitaControles;

                        fechitaControles = Convert.ToDateTime(lblFechaNacimientoMenor.Content);
                        fechitaControles = fechitaControles.AddMonths(-1);

                        for (int i = 0; i < CantidadDeControles; i++)
                        {
                            fechitaControles = fechitaControles.AddMonths(2);

                            ControlMenor controlmenor = new ControlMenor();
                            controlmenor.IdCorresponsabilidadMenor = IdCorresponsabilidadMenor;
                            controlmenor.IdEstablecimientoSalud = 1;
                            controlmenor.IdMenor = IdSeleccionado;
                            controlmenor.IdMedico = 1;

                            if (Convert.ToInt64(cboMadre.SelectedIndex) >= 0)
                                controlmenor.IdMadre = Convert.ToInt64(cboMadre.SelectedValue);

                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                            {
                                controlmenor.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                                controlmenor.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                            }

                            controlmenor.FechaProgramada = fechitaControles;
                            controlmenor.FechaControl = DateTime.Now;
                            controlmenor.TallaCm = 0;
                            controlmenor.PesoKg = 0;
                            controlmenor.NumeroControl = i + 1;
                            controlmenor.Observaciones = "";
                            controlmenor.EstadoPago = TipoEstadoPago.NoPagado;
                            if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                                controlmenor.TipoBeneficiario = TipoBeneficiario.Tutor;
                            else
                                controlmenor.TipoBeneficiario = TipoBeneficiario.Madre;
                            modelocontrolmenor.Crear(controlmenor);
                        }
                        this.txtCodigoFormulario.IsEnabled = false;
                        this.dtpFechaInscripcion.IsEnabled = false;
                        this.rdbNueva.IsEnabled = false;
                        this.rdbTransferencia.IsEnabled = false;
                        RecuperarControlMenor();
                    }
                    else
                    {
                        corresponsabilidadmenor = modelocorresponsabilidadmenor.Recuperar(IdCorresponsabilidadMenor);

                        if (Convert.ToInt64(cboMadre.SelectedIndex) >= 0)
                            corresponsabilidadmenor.IdMadre = Convert.ToInt64(cboMadre.SelectedValue);

                        if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                        {
                            corresponsabilidadmenor.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                            corresponsabilidadmenor.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
                        }

                        if (this.chkSalida.IsChecked == true)
                        {
                            corresponsabilidadmenor.FechaSalidaPrograma = this.dtpFechaSalida.SelectedDate.Value;
                            if (this.rdbCumplimiento.IsChecked == true)
                                corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Cumplimiento;
                            else if (this.rdbFallecimiento.IsChecked == true)
                                corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Fallecimiento;
                            else if (this.rdbIncumplimiento.IsChecked == true)
                                corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Incumplimiento;
                            else if (this.rdbTransferenciaSalida.IsChecked == true)
                                corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Transferencia;
                            corresponsabilidadmenor.AutorizadoPor = this.txtAutorizado.Text;
                            corresponsabilidadmenor.CargoAutorizador = this.txtCargo.Text;
                        }
                        else
                        {
                            corresponsabilidadmenor.FechaSalidaPrograma = DateTime.Now;
                            corresponsabilidadmenor.TipoSalidaMenor = 0;
                            corresponsabilidadmenor.AutorizadoPor = "";
                            corresponsabilidadmenor.CargoAutorizador = "";
                        }

                        modelocorresponsabilidadmenor.Editar(IdCorresponsabilidadMenor, corresponsabilidadmenor);
                    }
                }
        }

        void RecuperarControlMenor()
        {
            ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
            this.grdControl.ItemsSource = modelocontrolmenor.ListarControlesDeCorresponsabilidadDeMenor(IdCorresponsabilidadMenor);
        }

        private void VerControlMenor(long IdControl, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmControl objControlWindow = new frmControl();
            objControlWindow.IdSeleccionado = IdControl;
            objControlWindow.IdMadre = IdSeleccionado;
            if (Convert.ToInt64(cboMadre.SelectedValue) > 0)
                objControlWindow.IdMadre = Convert.ToInt64(cboMadre.SelectedValue);
            if (Convert.ToInt64(cboTutor.SelectedValue) > 0)
            {
                objControlWindow.IdTutor = Convert.ToInt64(cboTutor.SelectedValue);
                objControlWindow.IdTipoParentesco = Convert.ToInt64(cboTipoParentesco.SelectedValue);
            }
            objControlWindow.TipoAccion = TipoAccion;
            objControlWindow.TipoControl = TipoControl.Menor;
            objControlWindow.Owner = this;
            objControlWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Edicion) && (objControlWindow.Resultado == true))
                RecuperarControlMenor();
            objControlWindow = null;
        }

        private void cmdEditarControl_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;

            if ((Convert.ToInt64(cboMadre.SelectedIndex) < 0) && (Convert.ToInt64(cboTutor.SelectedIndex) < 0))
            {
                MessageBox.Show("Se requiere especificar madre y/o tutor para el menor.", "Error");
                ok = true;
            }
            if (ok == false)
            {
                if (Convert.ToInt64(cboTutor.SelectedIndex) >= 0)
                {
                    if (Convert.ToInt32(cboTipoParentesco.SelectedIndex) < 0)
                    {
                        MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
                        ok = true;
                    }
                }
            }

            if (ok == false)
            {
                Button Img = (Button)sender;
                if (Img.Tag != null)
                {
                    Int64 Id = (Int64)Img.Tag;

                    if (Id > 0)
                        VerControlMenor(Id, TipoAccion.Edicion);
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
                    VerControlMenor(Id, TipoAccion.Detalle);
            }
        }

        private void chkSalida_Checked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaSalida.IsEnabled = true;
            this.rdbCumplimiento.IsEnabled = true;
            this.rdbFallecimiento.IsEnabled = true;
            this.rdbIncumplimiento.IsEnabled = true;
            this.rdbTransferenciaSalida.IsEnabled = true;
            this.txtAutorizado.IsEnabled = true;
            this.txtCargo.IsEnabled = true;
        }

        private void chkSalida_Unchecked(object sender, RoutedEventArgs e)
        {
            this.dtpFechaSalida.IsEnabled = false;
            this.rdbCumplimiento.IsEnabled = false;
            this.rdbFallecimiento.IsEnabled = false;
            this.rdbIncumplimiento.IsEnabled = false;
            this.rdbTransferenciaSalida.IsEnabled = false;
            this.txtAutorizado.IsEnabled = false;
            this.txtCargo.IsEnabled = false;
            this.dtpFechaSalida.SelectedDate = DateTime.Today;
            this.rdbCumplimiento.IsChecked = false;
            this.rdbFallecimiento.IsChecked = false;
            this.rdbIncumplimiento.IsChecked = false;
            this.rdbTransferenciaSalida.IsChecked = false;
            this.txtAutorizado.Text = "";
            this.txtCargo.Text = "";
        }

    }
}
