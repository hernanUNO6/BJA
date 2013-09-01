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
        public long IdSeleccionado { get; set; }
        public long IdMenor { get; set; }
        long IdMadre { get; set; }
        long IdTutor { get; set; }
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

            //SoporteCombo.cargarEnumerador(this.cboTipoParentesco, typeof(TipoParentesco));

            ////this.lblDepartamento.Content = "|";
            ////this.lblEstablecimiento.Content = "|";
            this.lblNombresMenor.Content = "";
            this.lblFechaNacimientoMenor.Content = "";
            this.lblNombreMadre.Content = "";
            this.lblFechaNacimientoMadre.Content = "";
            this.cboTipoParentesco.SelectedIndex = -1;

            //ValoresPorDefecto();

            //if (IdMenor > 0)
            //{
            //    RecuperarMenor();
            //    this.cmdModificarMenor.IsEnabled = true;
            //    this.cmdDetallesMenor.IsEnabled = true;
            //}

            //if (IdSeleccionado > 0)
            //    RecuperarCorresponsabilidadMenor(IdSeleccionado);
            //if (TipoAccion == TipoAccion.Detalle)
            //{
            //    this.txtCodigoFormulario.IsEnabled = false;
            //    this.dtpFechaInscripcion.IsEnabled = false;
            //    this.cmdSeleccionarMenor.IsEnabled = false;
            //    this.cmdModificarMenor.IsEnabled = false;
            //    this.cmdDetallesMenor.IsEnabled = false;
            //    this.chkMadre.IsEnabled = false;
            //    this.cmdSeleccionarMadre.IsEnabled = false;
            //    this.cmdModificarMadre.IsEnabled = false;
            //    this.cmdDetallesMadre.IsEnabled = false;
            //    this.chkTutor.IsEnabled = false;
            //    this.cmdSeleccionarTutor.IsEnabled = false;
            //    this.cmdModificarTutor.IsEnabled = false;
            //    this.cmdDetallesTutor.IsEnabled = false;
            //    this.chkSalida.IsEnabled = false;
            //    this.rdbCumplimiento.IsEnabled = false;
            //    this.rdbFallecimiento.IsEnabled = false;
            //    this.rdbIncumplimiento.IsEnabled = false;
            //    this.rdbNueva.IsEnabled = false;
            //    this.rdbTransferencia.IsEnabled = false;
            //    this.rdbTransferenciaSalida.IsEnabled = false;
            //    this.txtAutorizado.IsEnabled = false;
            //    this.txtCargo.IsEnabled = false;
            //    this.cmdGuardar.IsEnabled = false;
            //}
            //else if ((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion))
            //    this.cmdSeleccionarMenor.IsEnabled = false;
        }

        //private void ValoresPorDefecto()
        //{
        //    this.txtCodigoFormulario.Text = "";
        //    this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
        //    this.dtpFechaSalida.SelectedDate = DateTime.Today;
        //    this.rdbNueva.IsChecked = true;
        //    this.cmdDetallesMenor.IsEnabled = false;
        //    this.cmdModificarMenor.IsEnabled = false;
        //    this.chkMadre.IsChecked = false;
        //    this.cmdSeleccionarMadre.IsEnabled = false;
        //    this.cmdDetallesMadre.IsEnabled = false;
        //    this.cmdModificarMadre.IsEnabled = false;
        //    this.chkTutor.IsChecked = false;
        //    this.cboTipoParentesco.IsEnabled = false;
        //    this.cmdSeleccionarTutor.IsEnabled = false;
        //    this.cmdDetallesTutor.IsEnabled = false;
        //    this.cmdModificarTutor.IsEnabled = false;
        //    this.chkSalida.IsChecked = false;
        //    this.chkSalida.IsEnabled = false;
        //    this.dtpFechaSalida.IsEnabled = false;
        //    this.rdbCumplimiento.IsChecked = false;
        //    this.rdbCumplimiento.IsEnabled = false;
        //    this.rdbFallecimiento.IsChecked = false;
        //    this.rdbFallecimiento.IsEnabled = false;
        //    this.rdbIncumplimiento.IsChecked = false;
        //    this.rdbIncumplimiento.IsEnabled = false;
        //    this.rdbTransferenciaSalida.IsChecked = false;
        //    this.rdbTransferenciaSalida.IsEnabled = false;
        //    this.txtAutorizado.Text = "";
        //    this.txtAutorizado.IsEnabled = false;
        //    this.txtCargo.Text = "";
        //    this.txtCargo.IsEnabled = false;
        //}

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void cmdSeleccionarMenor_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmLista formularioListaMenores = new frmLista();

        //    formularioListaMenores.NuevoRegistro += formularioListaMenores_NuevoRegistro;
        //    formularioListaMenores.DetallesRegistro += formularioListaMenores_DetallesRegistro;
        //    formularioListaMenores.ModificarRegistro += formularioListaMenores_ModificarRegistro;
        //    formularioListaMenores.SeleccionarRegistro += formularioListaMenores_SeleccionarRegistro;

        //    ModeloMenor modelomenor = new ModeloMenor();

        //    formularioListaMenores.proveedorDatos = modelomenor;
        //    formularioListaMenores.titulo = "Menores";
        //    formularioListaMenores.ShowDialog();
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMenores_NuevoRegistro(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMenor objMenorWindow = new frmMenor();
        //    objMenorWindow.IdSeleccionado = 0;
        //    objMenorWindow.TipoAccion = TipoAccion.Nuevo;
        //    objMenorWindow.Owner = this;
        //    objMenorWindow.ShowDialog();
        //    objMenorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMenores_DetallesRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMenor objMenorWindow = new frmMenor();
        //    objMenorWindow.IdSeleccionado = fe.id;
        //    objMenorWindow.TipoAccion = TipoAccion.Detalle;
        //    objMenorWindow.Owner = this;
        //    objMenorWindow.ShowDialog();
        //    objMenorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMenores_ModificarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMenor objMenorWindow = new frmMenor();
        //    objMenorWindow.IdSeleccionado = fe.id;
        //    objMenorWindow.TipoAccion = TipoAccion.Edicion;
        //    objMenorWindow.Owner = this;
        //    objMenorWindow.ShowDialog();
        //    objMenorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMenores_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    IdMenor = fe.id;
        //    this.lblNombresMenor.Content = "";
        //    this.lblFechaNacimientoMenor.Content = "";
        //    this.lblNombreMadre.Content = "";
        //    this.lblFechaNacimientoMadre.Content = "";
        //    this.lblNombreTutor.Content = "";
        //    this.lblFechaNacimientoTutor.Content = "";
        //    RecuperarMenor();
        //    ValoresPorDefecto();
        //    RecuperarCorresponsabilidadMenor(0);
        //    this.cmdModificarMenor.IsEnabled = true;
        //    this.cmdDetallesMenor.IsEnabled = true;
        //}

        //void RecuperarCorresponsabilidadMenor(long IdCual)
        //{
        //    //ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
        //    //CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();

        //    //if (IdCual > 0)
        //    //    corresponsabilidadmenor = modelocorresponsabilidadmenor.Recuperar(IdCual);
        //    //else
        //    //    corresponsabilidadmenor = modelocorresponsabilidadmenor.RecuperarElUltimoValido(IdMenor);
        //    //if (corresponsabilidadmenor != null)
        //    //{
        //    //    IdSeleccionado = corresponsabilidadmenor.Id;

        //    //    if (corresponsabilidadmenor.IdMadre > 0)
        //    //    {
        //    //        IdMadre = corresponsabilidadmenor.IdMadre;
        //    //        RecuperarMadre();
        //    //        this.chkMadre.IsChecked = true;
        //    //        this.cmdDetallesMadre.IsEnabled = true;
        //    //        this.cmdModificarMadre.IsEnabled = true;
        //    //    }

        //    //    if (corresponsabilidadmenor.IdTutor > 0)
        //    //    {
        //    //        IdTutor = corresponsabilidadmenor.IdTutor;
        //    //        cboTipoParentesco.SelectedValue = corresponsabilidadmenor.IdTipoParentesco;
        //    //        RecuperarTutor();
        //    //        this.chkTutor.IsChecked = true;
        //    //        this.cmdDetallesTutor.IsEnabled = true;
        //    //        this.cmdModificarTutor.IsEnabled = true;
        //    //    }
        //    //    this.rdbNueva.IsEnabled = false;
        //    //    this.rdbTransferencia.IsEnabled = false;
        //    //    if (corresponsabilidadmenor.TipoInscripcionMenor == TipoInscripcion.Nueva)
        //    //        this.rdbNueva.IsChecked = true;
        //    //    else if (corresponsabilidadmenor.TipoInscripcionMenor == TipoInscripcion.Transferencia)
        //    //        this.rdbTransferencia.IsChecked = true;
        //    //    this.txtCodigoFormulario.Text = corresponsabilidadmenor.CodigoFormulario;
        //    //    this.txtCodigoFormulario.IsEnabled = false;
        //    //    this.dtpFechaInscripcion.SelectedDate = corresponsabilidadmenor.FechaInscripcion;
        //    //    this.dtpFechaInscripcion.IsEnabled = false;
        //    //    RecuperarControlMenor();
        //    //    this.chkSalida.IsEnabled = true;
        //    //    if (corresponsabilidadmenor.TipoSalidaMenor > 0)
        //    //    {
        //    //        this.chkSalida.IsChecked = true;
        //    //        this.dtpFechaSalida.SelectedDate = corresponsabilidadmenor.FechaSalidaPrograma;
        //    //        this.dtpFechaSalida.IsEnabled = true;
        //    //        this.rdbCumplimiento.IsEnabled = true;
        //    //        this.rdbFallecimiento.IsEnabled = true;
        //    //        this.rdbIncumplimiento.IsEnabled = true;
        //    //        this.rdbTransferenciaSalida.IsEnabled = true;
        //    //        switch (corresponsabilidadmenor.TipoSalidaMenor)
        //    //        {
        //    //            case TipoSalidaMenor.Cumplimiento:
        //    //                this.rdbCumplimiento.IsChecked = true;
        //    //                break;
        //    //            case TipoSalidaMenor.Fallecimiento:
        //    //                this.rdbFallecimiento.IsChecked = true;
        //    //                break;
        //    //            case TipoSalidaMenor.Incumplimiento:
        //    //                this.rdbIncumplimiento.IsChecked = true;
        //    //                break;
        //    //            case TipoSalidaMenor.Transferencia:
        //    //                this.rdbTransferenciaSalida.IsChecked = true;
        //    //                break;
        //    //        }
        //    //        this.txtAutorizado.Text = corresponsabilidadmenor.AutorizadoPor;
        //    //        this.txtAutorizado.IsEnabled = true;
        //    //        this.txtCargo.Text = corresponsabilidadmenor.CargoAutorizador;
        //    //        this.txtCargo.IsEnabled = true;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    IdSeleccionado = 0;
        //    //    this.txtCodigoFormulario.IsEnabled = true;
        //    //    this.dtpFechaInscripcion.IsEnabled = true;
        //    //    this.rdbNueva.IsEnabled = true;
        //    //    this.rdbTransferencia.IsEnabled = true;
        //    //    this.grdControl.ItemsSource = null;
        //    //}
        //}

        //void RecuperarMenor()
        //{
        //    ModeloMenor modelomenor = new ModeloMenor();
        //    Menor menor = new Menor();
        //    menor = modelomenor.Recuperar(IdMenor);
        //    lblNombresMenor.Content = menor.Nombres + " " + menor.PrimerApellido + " " + menor.SegundoApellido;
        //    lblFechaNacimientoMenor.Content = string.Format("{0:dd/MM/yyyy}", menor.FechaNacimiento);
        //}

        //private void cmdSeleccionarMadre_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmLista formularioListaMadres = new frmLista();

        //    formularioListaMadres.NuevoRegistro += formularioListaMadres_NuevoRegistro;
        //    formularioListaMadres.DetallesRegistro += formularioListaMadres_DetallesRegistro;
        //    formularioListaMadres.ModificarRegistro += formularioListaMadres_ModificarRegistro;
        //    formularioListaMadres.SeleccionarRegistro += formularioListaMadres_SeleccionarRegistro;

        //    ModeloMadre modelomadre = new ModeloMadre();

        //    formularioListaMadres.proveedorDatos = modelomadre;
        //    formularioListaMadres.titulo = "Madres";
        //    formularioListaMadres.ShowDialog();
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMadres_NuevoRegistro(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMadre objMadreWindow = new frmMadre();
        //    objMadreWindow.TipoAccion = TipoAccion.Nuevo;
        //    objMadreWindow.IdSeleccionado = 0;
        //    objMadreWindow.Owner = this;
        //    objMadreWindow.ShowDialog();
        //    objMadreWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMadres_DetallesRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMadre objMadreWindow = new frmMadre();
        //    objMadreWindow.IdSeleccionado = fe.id;
        //    objMadreWindow.TipoAccion = TipoAccion.Detalle;
        //    objMadreWindow.Owner = this;
        //    objMadreWindow.ShowDialog();
        //    objMadreWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMadres_ModificarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmMadre objMadreWindow = new frmMadre();
        //    objMadreWindow.IdSeleccionado = fe.id;
        //    objMadreWindow.TipoAccion = TipoAccion.Edicion;
        //    objMadreWindow.Owner = this;
        //    objMadreWindow.ShowDialog();
        //    objMadreWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaMadres_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    IdMadre = fe.id;
        //    RecuperarMadre();
        //}

        //void RecuperarMadre()
        //{
        //    ModeloMadre modelomadre = new ModeloMadre();
        //    Madre madre = new Madre();
        //    madre = modelomadre.Recuperar(IdMadre);
        //    lblNombreMadre.Content = madre.Nombres + " " + madre.PrimerApellido + " " + madre.SegundoApellido;
        //    lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
        //}

        //private void cmdDetallesMenor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmMenor objMenorWindow = new frmMenor();
        //        objMenorWindow.IdSeleccionado = IdMenor;
        //        objMenorWindow.TipoAccion = TipoAccion.Detalle;
        //        objMenorWindow.Owner = this;
        //        objMenorWindow.ShowDialog();
        //        objMenorWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //    }
        //}

        //private void cmdModificarMenor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmMenor objMenorWindow = new frmMenor();
        //        objMenorWindow.IdSeleccionado = IdMenor;
        //        objMenorWindow.TipoAccion = TipoAccion.Edicion;
        //        objMenorWindow.Owner = this;
        //        objMenorWindow.ShowDialog();
        //        objMenorWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //        RecuperarMenor();
        //    }
        //}

        //private void cmdSeleccionarTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmLista formularioListaTutores = new frmLista();

        //    formularioListaTutores.NuevoRegistro += formularioListaTutores_NuevoRegistro;
        //    formularioListaTutores.DetallesRegistro += formularioListaTutores_DetallesRegistro;
        //    formularioListaTutores.ModificarRegistro += formularioListaTutores_ModificarRegistro;
        //    formularioListaTutores.SeleccionarRegistro += formularioListaTutores_SeleccionarRegistro;

        //    ModeloTutor modelotutor = new ModeloTutor();

        //    formularioListaTutores.proveedorDatos = modelotutor;
        //    formularioListaTutores.titulo = "Tutores";
        //    formularioListaTutores.ShowDialog();
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaTutores_NuevoRegistro(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmTutor objTutorWindow = new frmTutor();
        //    objTutorWindow.TipoAccion = TipoAccion.Nuevo;
        //    objTutorWindow.IdSeleccionado = 0;
        //    objTutorWindow.Owner = this;
        //    objTutorWindow.ShowDialog();
        //    objTutorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaTutores_DetallesRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmTutor objTutorWindow = new frmTutor();
        //    objTutorWindow.IdSeleccionado = fe.id;
        //    objTutorWindow.TipoAccion = TipoAccion.Detalle;
        //    objTutorWindow.Owner = this;
        //    objTutorWindow.ShowDialog();
        //    objTutorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaTutores_ModificarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    this.Cursor = Cursors.Wait;
        //    frmTutor objTutorWindow = new frmTutor();
        //    objTutorWindow.IdSeleccionado = fe.id;
        //    objTutorWindow.TipoAccion = TipoAccion.Edicion;
        //    objTutorWindow.Owner = this;
        //    objTutorWindow.ShowDialog();
        //    objTutorWindow = null;
        //    this.Cursor = Cursors.Arrow;
        //}

        //void formularioListaTutores_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        //{
        //    IdTutor = fe.id;
        //    RecuperarTutor();
        //}

        //void RecuperarTutor()
        //{
        //    ModeloTutor modelotutor = new ModeloTutor();
        //    Tutor tutor = new Tutor();
        //    tutor = modelotutor.Recuperar(IdTutor);
        //    lblNombreTutor.Content = tutor.Nombres + " " + tutor.PrimerApellido + " " + tutor.SegundoApellido;
        //    lblFechaNacimientoTutor.Content = string.Format("{0:dd/MM/yyyy}", tutor.FechaNacimiento);
        //}

        //private void cmdDetallesTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdTutor > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmTutor objTutorWindow = new frmTutor();
        //        objTutorWindow.IdSeleccionado = IdTutor;
        //        objTutorWindow.TipoAccion = TipoAccion.Detalle;
        //        objTutorWindow.Owner = this;
        //        objTutorWindow.ShowDialog();
        //        objTutorWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //    }
        //}

        //private void cmdModificarTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdTutor > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmTutor objTutorWindow = new frmTutor();
        //        objTutorWindow.IdSeleccionado = IdTutor;
        //        objTutorWindow.TipoAccion = TipoAccion.Edicion;
        //        objTutorWindow.Owner = this;
        //        objTutorWindow.ShowDialog();
        //        objTutorWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //        RecuperarTutor();
        //    }
        //}

        //private void cmdDetallesMadre_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdMadre > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmMadre objMadreWindow = new frmMadre();
        //        objMadreWindow.IdSeleccionado = IdMadre;
        //        objMadreWindow.TipoAccion = TipoAccion.Detalle;
        //        objMadreWindow.Owner = this;
        //        objMadreWindow.ShowDialog();
        //        objMadreWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //    }
        //}

        //private void cmdModificarMadre_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IdMadre > 0)
        //    {
        //        this.Cursor = Cursors.Wait;
        //        frmMadre objMadreWindow = new frmMadre();
        //        objMadreWindow.IdSeleccionado = IdMadre;
        //        objMadreWindow.TipoAccion = TipoAccion.Edicion;
        //        objMadreWindow.Owner = this;
        //        objMadreWindow.ShowDialog();
        //        objMadreWindow = null;
        //        this.Cursor = Cursors.Arrow;
        //        RecuperarMadre();
        //    }
        //}

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            //    ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
            //    CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();

            //    bool ok = false;

            //    if (!(IdMenor > 0))
            //    {
            //        MessageBox.Show("Se requiere especificar menor.", "Error");
            //        ok = true;
            //    }
            //    else if ((!(IdMadre > 0)) && (!(IdTutor > 0)))
            //    {
            //        MessageBox.Show("Se requiere especificar madre o tutor del menor.", "Error");
            //        ok = true;
            //    }

            //    if (ok == false)
            //    {
            //        if (this.chkMadre.IsChecked == true)
            //        {
            //            if (!(IdMadre > 0))
            //            {
            //                MessageBox.Show("Se requiere especificar madre.", "Error");
            //                ok = true;
            //            }
            //        }
            //    }

            //    if (ok == false)
            //    {
            //        if (this.chkTutor.IsChecked == true)
            //        {
            //            if (!(IdTutor > 0))
            //            {
            //                MessageBox.Show("Se requiere especificar tutor.", "Error");
            //                ok = true;
            //            }
            //            else if (!(Convert.ToInt32(cboTipoParentesco.SelectedValue) > 0))
            //            {
            //                MessageBox.Show("Se requiere especificar tipo de parentesco.", "Error");
            //                ok = true;
            //            }
            //        }
            //    }

            //    if (ok == false)
            //    {
            //        if (!(txtCodigoFormulario.Text.Length > 0))
            //        {
            //            MessageBox.Show("Se requiere especificar número de formulario.", "Error");
            //            ok = true;
            //        }
            //    }

            //    if (ok == false)
            //    {
            //        if (IdSeleccionado == 0)
            //        {
            //            corresponsabilidadmenor.IdEstablecimientoSalud = 1;
            //            if (rdbNueva.IsChecked == true)
            //                corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Nueva;
            //            else if (rdbTransferencia.IsChecked == true)
            //                corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Transferencia;

            //            corresponsabilidadmenor.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
            //            corresponsabilidadmenor.IdMenor = IdMenor;
            //            corresponsabilidadmenor.IdMadre = IdMadre;
            //            corresponsabilidadmenor.IdTutor = IdMadre;
            //            corresponsabilidadmenor.CodigoFormulario = txtCodigoFormulario.Text;
            //            corresponsabilidadmenor.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
            //            corresponsabilidadmenor.TipoSalidaMenor = 0;
            //            corresponsabilidadmenor.Observaciones = "";
            //            corresponsabilidadmenor.AutorizadoPor = txtAutorizado.Text;
            //            corresponsabilidadmenor.CargoAutorizador = txtCargo.Text;

            //            modelocorresponsabilidadmenor.Crear(corresponsabilidadmenor);
            //            IdSeleccionado = corresponsabilidadmenor.Id;

            //            ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
            //            DateTime fechitaControles;

            //            fechitaControles = Convert.ToDateTime(lblFechaNacimientoMenor.Content);
            //            fechitaControles = fechitaControles.AddMonths(-1);

            //            for (int i = 0; i < CantidadDeControles; i++)
            //            {
            //                fechitaControles = fechitaControles.AddMonths(2);

            //                ControlMenor controlmenor = new ControlMenor();
            //                controlmenor.IdCorresponsabilidadMenor = IdSeleccionado;
            //                controlmenor.IdMedico = 1;
            //                controlmenor.IdMadre = IdMadre;
            //                controlmenor.IdTutor = IdTutor;
            //                controlmenor.FechaProgramada = fechitaControles;
            //                controlmenor.FechaControl = DateTime.Now;
            //                controlmenor.TallaCm = 0;
            //                controlmenor.PesoKg = 0;
            //                controlmenor.NumeroControl = i + 1;
            //                controlmenor.Observaciones = "";
            //                controlmenor.EstadoPago = TipoEstadoPago.NoPagado;
            //                if (IdTutor > 0)
            //                    controlmenor.TipoBeneficiario = TipoBeneficiario.Tutor;
            //                else
            //                    controlmenor.TipoBeneficiario = TipoBeneficiario.Madre;
            //                modelocontrolmenor.Crear(controlmenor);
            //            }
            //            this.txtCodigoFormulario.IsEnabled = false;
            //            this.dtpFechaInscripcion.IsEnabled = false;
            //            this.rdbNueva.IsEnabled = false;
            //            this.rdbTransferencia.IsEnabled = false;
            //            RecuperarControlMenor();
            //        }
            //        else
            //        {
            //            corresponsabilidadmenor = modelocorresponsabilidadmenor.Recuperar(IdSeleccionado);

            //            if (this.chkMadre.IsChecked == true)
            //                corresponsabilidadmenor.IdMadre = IdMadre;
            //            else
            //                corresponsabilidadmenor.IdMadre = 0; //rrsc

            //            if (this.chkTutor.IsChecked == true)
            //            {
            //                corresponsabilidadmenor.IdTutor = IdTutor;
            //                corresponsabilidadmenor.IdTipoParentesco = Convert.ToInt32(cboTipoParentesco.SelectedValue);
            //            }
            //            else
            //            {
            //                corresponsabilidadmenor.IdTutor = 0; //rrsc
            //                corresponsabilidadmenor.IdTipoParentesco = 0; //rrsc
            //            }

            //            if (this.chkSalida.IsChecked == true)
            //            {
            //                corresponsabilidadmenor.FechaSalidaPrograma = this.dtpFechaSalida.SelectedDate.Value;
            //                if (this.rdbCumplimiento.IsChecked == true)
            //                    corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Cumplimiento;
            //                else if (this.rdbFallecimiento.IsChecked == true)
            //                    corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Fallecimiento;
            //                else if (this.rdbIncumplimiento.IsChecked == true)
            //                    corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Incumplimiento;
            //                else if (this.rdbTransferenciaSalida.IsChecked == true)
            //                    corresponsabilidadmenor.TipoSalidaMenor = TipoSalidaMenor.Transferencia;
            //                corresponsabilidadmenor.AutorizadoPor = this.txtAutorizado.Text;
            //                corresponsabilidadmenor.CargoAutorizador = this.txtCargo.Text;
            //            }
            //            else
            //            {
            //                corresponsabilidadmenor.FechaSalidaPrograma = DateTime.Now;
            //                corresponsabilidadmenor.TipoSalidaMenor = 0;
            //                corresponsabilidadmenor.AutorizadoPor = "";
            //                corresponsabilidadmenor.CargoAutorizador = "";
            //            }

            //            modelocorresponsabilidadmenor.Editar(IdSeleccionado, corresponsabilidadmenor);
            //        }
            //    }
        }

        //void RecuperarControlMenor()
        //{
        //    ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
        //    this.grdControl.ItemsSource = modelocontrolmenor.ListarControlesDeCorresponsabilidadDeMenor(IdSeleccionado);
        //}

        private void cmdEditarControl_Click(object sender, RoutedEventArgs e)
        {
            //    Button Img = (Button)sender;
            //    if (Img.Tag != null)
            //    {
            //        Int64 Id = (Int64)Img.Tag;

            //        if (Id > 0)
            //        {
            //            this.Cursor = Cursors.Wait;
            //            frmControl objControlWindow = new frmControl();
            //            objControlWindow.IdSeleccionado = Id;
            //            objControlWindow.IdMenor = IdMenor;
            //            objControlWindow.IdMadre = IdMadre;
            //            objControlWindow.IdTutor = IdTutor;
            //            objControlWindow.TipoAccion = TipoAccion.Edicion;
            //            objControlWindow.TipoControl = TipoControl.Menor;
            //            objControlWindow.Owner = this;
            //            objControlWindow.ShowDialog();
            //            if (objControlWindow.Resultado == true)
            //                RecuperarControlMenor();
            //            objControlWindow = null;
            //            this.Cursor = Cursors.Arrow;
            //        }
            //    }
        }

        private void cmdDetalleControl_Click(object sender, RoutedEventArgs e)
        {
            //    Button Img = (Button)sender;
            //    if (Img.Tag != null)
            //    {
            //        Int64 Id = (Int64)Img.Tag;

            //        if (Id > 0)
            //        {
            //            this.Cursor = Cursors.Wait;
            //            frmControl objControlWindow = new frmControl();
            //            objControlWindow.IdSeleccionado = Id;
            //            objControlWindow.IdMenor = IdMenor;
            //            objControlWindow.IdMadre = IdMadre;
            //            objControlWindow.IdTutor = IdTutor;
            //            objControlWindow.TipoAccion = TipoAccion.Detalle;
            //            objControlWindow.TipoControl = TipoControl.Menor;
            //            objControlWindow.Owner = this;
            //            objControlWindow.ShowDialog();
            //            objControlWindow = null;
            //            this.Cursor = Cursors.Arrow;
            //        }
            //    }
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

        //private void chkMadre_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.cmdSeleccionarMadre.IsEnabled = true;
        //        this.cmdDetallesMadre.IsEnabled = true;
        //        this.cmdModificarMadre.IsEnabled = true;
        //    }

        //}

        //private void chkMadre_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.cmdSeleccionarMadre.IsEnabled = false;
        //        this.cmdDetallesMadre.IsEnabled = false;
        //        this.cmdModificarMadre.IsEnabled = false;
        //        IdMadre = 0;
        //        this.lblNombreMadre.Content = "";
        //        this.lblFechaNacimientoMadre.Content = "";
        //    }
        //}

        //private void chkTutor_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.cboTipoParentesco.IsEnabled = true;
        //        this.cmdSeleccionarTutor.IsEnabled = true;
        //        this.cmdDetallesTutor.IsEnabled = true;
        //        this.cmdModificarTutor.IsEnabled = true;
        //    }
        //}

        //private void chkTutor_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    if (IdMenor > 0)
        //    {
        //        this.cboTipoParentesco.IsEnabled = false;
        //        this.cmdSeleccionarTutor.IsEnabled = false;
        //        this.cmdDetallesTutor.IsEnabled = false;
        //        this.cmdModificarTutor.IsEnabled = false;
        //        IdTutor = 0;
        //        this.lblNombreTutor.Content = "";
        //        this.lblFechaNacimientoTutor.Content = "";
        //        this.cboTipoParentesco.SelectedIndex = -1;
        //    }
        //}

    }
}
