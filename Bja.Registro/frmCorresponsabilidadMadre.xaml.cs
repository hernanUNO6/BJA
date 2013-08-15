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
        long[] IdControlMadre = new long[4];    //A ser controlado posteriromwente por la variable CantidadDeControles
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

            if (IdSeleccionado == 0)
            {
                this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
                this.dtpFechaFUM.SelectedDate = DateTime.Today;
                this.dtpFechaUltimoParto.SelectedDate = DateTime.Today;
                this.dtpFechaSalida.SelectedDate = DateTime.Today;
                rdbNueva.IsChecked = true;
                txtNumeroEmbarazo.Text = "0";
            }
        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdBuscar_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaTutor = new frmLista();

            formularioListaTutor.NuevoRegistro += formularioListaTutor_NuevoRegistro;
            formularioListaTutor.MostrarDetallesRegistro += formularioListaTutor_MostrarDetallesRegistro;
            formularioListaTutor.ModificarRegistro += formularioListaTutor_ModificarRegistro;
            formularioListaTutor.BorrarRegistro += formularioListaTutor_BorrarRegistro;
            formularioListaTutor.SeleccionarRegistro += formularioListaTutor_SeleccionarRegistro;

            ModeloTutor modelomadre = new ModeloTutor();

            formularioListaTutor.proveedorDatos = modelomadre;
            formularioListaTutor.titulo = "Madres";
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

        void formularioListaTutor_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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
            MessageBox.Show("Por Implementar.", "Mensaje");
        }

        void formularioListaTutor_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        {
            long IdTutor = fe.id;
            ModeloTutor modelotutor = new ModeloTutor();
            Tutor tutor = new Tutor();
            tutor = modelotutor.Recuperar(IdTutor);
            lblNomnbreMadre.Content = tutor.Nombres;
            lblPrimerApellido.Content = tutor.PrimerApellido;
            lblSegundoApellido.Content = tutor.SegundoApellido;
            lblTercerApellido.Content = tutor.TercerApellido;
            lblDocumentoIdentidad.Content = tutor.DocumentoIdentidad;
            lblFechaNacimientoMadre.Content = tutor.FechaNacimiento;
            IdTutor = fe.id;
        }

        private void cmdBuscarMadre_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMadres = new frmLista();

            formularioListaMadres.NuevoRegistro += formularioListaMadres_NuevoRegistro;
            formularioListaMadres.MostrarDetallesRegistro += formularioListaMadres_MostrarDetallesRegistro;
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

        void formularioListaMadres_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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
            MessageBox.Show("Por Implementar.", "Mensaje");
        }

        void formularioListaMadres_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        {
            IdMadre = fe.id;
            RecuperarMadre();
        }

        private void cmdBuscarTutor_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaTutores = new frmLista();

            formularioListaTutores.NuevoRegistro += formularioListaTutores_NuevoRegistro;
            formularioListaTutores.MostrarDetallesRegistro += formularioListaTutores_MostrarDetallesRegistro;
            formularioListaTutores.ModificarRegistro += formularioListaTutores_ModificarRegistro;
            formularioListaTutores.BorrarRegistro += formularioListaTutores_BorrarRegistro;
            formularioListaTutores.SeleccionarRegistro += formularioListaTutores_SeleccionarRegistro;

            ModeloTutor modelotutor = new ModeloTutor();

            formularioListaTutores.proveedorDatos = modelotutor;
            formularioListaTutores.titulo = "Tutores";
            formularioListaTutores.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutores_NuevoRegistro(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdSeleccionado = 0;
            objTutorWindow.TipoAccion = TipoAccion.Nuevo;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutores_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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

        void formularioListaTutores_ModificarRegistro(object sender, IdentidadEventArgs fe)
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

        void formularioListaTutores_BorrarRegistro(object sender, IdentidadEventArgs fe)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Por Implementar.", "Mensaje");
        }

        void formularioListaTutores_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
        {
            IdTutor = fe.id;
            RecuperarTutor();
        }

        void RecuperarMadre()
        {
            ModeloMadre modelomadre = new ModeloMadre();
            Madre madre = new Madre();
            madre = modelomadre.Recuperar(IdMadre);
            lblPrimerApellido.Content = madre.PrimerApellido;
            lblSegundoApellido.Content = madre.SegundoApellido;
            lblTercerApellido.Content = madre.TercerApellido;
            lblNomnbreMadre.Content = madre.Nombres;
            lblDocumentoIdentidad.Content = madre.DocumentoIdentidad;
            lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
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
                RecuperarMadre();
            }
        }

        private void cmdModifcarMadre_Click(object sender, RoutedEventArgs e)
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
                RecuperarTutor();
            }
        }

        void RecuperarTutor()
        {
            ModeloTutor modelotutor = new ModeloTutor();
            Tutor tutor = new Tutor();
            tutor = modelotutor.Recuperar(IdTutor);
            lblPrimerApellidoTutor.Content = tutor.PrimerApellido;
            lblSegundoApellidoTutor.Content = tutor.SegundoApellido;
            lblTercerApellidoTutor.Content = tutor.TercerApellido;
            lblNombreTutor.Content = tutor.Nombres;
            lblDocumentoIdentidadTutor.Content = tutor.DocumentoIdentidad;
            lblFechaNacimientoTutor.Content = string.Format("{0:dd/MM/yyyy}", tutor.FechaNacimiento);
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado == 0)
            {
                if (IdMadre > 0)
                {
                    ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
                    CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

                    corresponsabilidadmadre.IdEstablecimientoSalud = 1;
                    if (rdbNueva.IsChecked == true)
                        corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Nueva;
                    else if (rdbTransferencia.IsChecked == true)
                        corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Transferencia;

                    corresponsabilidadmadre.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
                    corresponsabilidadmadre.IdMadre = IdMadre;
                    corresponsabilidadmadre.IdTutor = IdTutor;
                    corresponsabilidadmadre.CodigoFormulario = txtCodigoFormulario.Text;
                    corresponsabilidadmadre.FechaUltimaMenstruacion = dtpFechaFUM.SelectedDate.Value; ;
                    corresponsabilidadmadre.FechaUltimoParto = dtpFechaUltimoParto.SelectedDate.Value; ;
                    corresponsabilidadmadre.NumeroEmbarazo = Convert.ToInt32(txtNumeroEmbarazo.Text);
                    corresponsabilidadmadre.ARO = (bool)chkARO.IsChecked;
                    corresponsabilidadmadre.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
                    corresponsabilidadmadre.TipoSalidaMadre = 0;
                    corresponsabilidadmadre.Observaciones = "";
                    corresponsabilidadmadre.AutorizadoPor = txtAutorizado.Text;
                    corresponsabilidadmadre.CargoAutorizador = txtCargo.Text;
                    corresponsabilidadmadre.DireccionMadre = "";
                    corresponsabilidadmadre.DireccionTutor = "";

                    modelocorresponsabilidadmadre.Crear(corresponsabilidadmadre);
                    IdSeleccionado = corresponsabilidadmadre.Id;

                    //generamos los 4 registro de controles

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
                        IdControlMadre[i] = controlmadre.Id;
                    }
                    
                    for (int i = 0; i < 2; i++)
                    {
                        ControlMadre controlmadre = new ControlMadre();
                        controlmadre.IdCorresponsabilidadMadre = IdSeleccionado;
                        controlmadre.IdMedico = 1;
                        controlmadre.IdTutor = IdTutor;
                        controlmadre.FechaProgramada = DateTime.Now;
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
                        IdControlMadre[i] = controlmadre.Id;
                    }
                    
                    txtCodigoFormulario.IsEnabled = false;
                    dtpFechaInscripcion.IsEnabled = false;
                    rdbNueva.IsEnabled = false;
                    rdbTransferencia.IsEnabled = false;
                    dtpFechaFUM.IsEnabled = false;
                    dtpFechaUltimoParto.IsEnabled = false;
                    txtNumeroEmbarazo.IsEnabled = false;
                    chkARO.IsEnabled = false;
                    cmdGuardar.IsEnabled = false;
                    this.grdControl.ItemsSource = modelocontrolmadre.ListarControlesDeCorresponsabilidadDeMadre(IdSeleccionado);
                }
            }
        }

        void RecuperarControlMadre(long id, int opcion)
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
                            RecuperarControlMadre(1, 1);
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
                            RecuperarControlMadre(1, 1);
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
                            RecuperarControlMadre(1, 1);
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
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre(1, 1);
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
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre(1, 1);
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
                        if (objControlWindow.Resultado == true)
                            RecuperarControlMadre(1, 1);
                        objControlWindow = null;
                    }
                }
            }
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
