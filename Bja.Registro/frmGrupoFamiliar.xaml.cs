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
    /// Lógica de interacción para frmGrupoFamiliar.xaml
    /// </summary>
    public partial class frmGrupoFamiliar : Window
    {
        public long IdSeleccionado { get; set; }
        private bool ControlPreliminar { get; set; }
        public TipoAccion TipoAccion { get; set; }
        private Familia _familia = new Familia();
        private Madre madre = new Madre();
        private Menor menor = new Menor();
        private RegistroTitularPago registrotitularpago = new RegistroTitularPago();

        public frmGrupoFamiliar()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
            {
                RecuperarFamilia();
                RecuperarMadres();
                RecuperarMenores();
                RecuperarMadresYTutoresALaVez();
            }
        }

        private void cmdModificar_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
            {
                bool ok;
                this.Cursor = Cursors.Wait;
                frmFamilia objFamiliaWindow = new frmFamilia();
                objFamiliaWindow.IdSeleccionado = IdSeleccionado;
                objFamiliaWindow.TipoAccion = TipoAccion.Edicion;
                objFamiliaWindow.Owner = this;
                objFamiliaWindow.ShowDialog();
                ok = objFamiliaWindow.Resultado;
                objFamiliaWindow = null;
                this.Cursor = Cursors.Arrow;
                if (ok == true)
                    RecuperarFamilia();
            }
        }

        void RecuperarFamilia()
        {
            ModeloFamilia modelofamilia = new ModeloFamilia();

            _familia = modelofamilia.Recuperar(IdSeleccionado);
            dtpFechaInscripcion.SelectedDate = _familia.FechaInscripcion;
            txtPaterno.Text = _familia.PrimerApellido;
            txtMaterno.Text = _familia.SegundoApellido;
        }

        void RecuperarMadres()
        {
            ModeloMadre modelomadre = new ModeloMadre();
            this.grdMadre.ItemsSource = modelomadre.ListarMadresDeUnaFamilia(IdSeleccionado);
        }

        private void VerMadre(long IdMadre, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdFamilia = IdSeleccionado;
            objMadreWindow.IdSeleccionado = IdMadre;
            objMadreWindow.TipoAccion = TipoAccion;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objMadreWindow.Resultado == true))
            { 
                RecuperarMadres();
                RecuperarMadresYTutoresALaVez();
            }
            objMadreWindow = null;
        }

        private void cmdNuevaMadre_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
                VerMadre(0, TipoAccion.Nuevo);
        }

        private void cmdEditarMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerMadre(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerMadre(Id, TipoAccion.Detalle);
            }
        }

        private void VerCorresponsabilidadDeMadre(long IdMadre, TipoAccion TipoAccion)
        {
            ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
            GrupoFamiliar grupofamiliar = new GrupoFamiliar();

            grupofamiliar = modelogrupofamiliar.RecuperarTitularHabilitado(IdSeleccionado);

            if (grupofamiliar != null)
            {
                this.Cursor = Cursors.Wait;
                frmCorresponsabilidadMadre objCorresponsabilidadMadreWindow = new frmCorresponsabilidadMadre();
                objCorresponsabilidadMadreWindow.IdFamilia = IdSeleccionado;
                objCorresponsabilidadMadreWindow.IdSeleccionado = IdMadre;
                objCorresponsabilidadMadreWindow.TipoAccion = TipoAccion;
                objCorresponsabilidadMadreWindow.Owner = this;
                objCorresponsabilidadMadreWindow.ShowDialog();
                this.Cursor = Cursors.Arrow;
                objCorresponsabilidadMadreWindow = null;
            }
            else
                MessageBox.Show("Se requiere especificar previamente titular de pago.", "Error");
        }

        private void cmdCorresponsabilidadMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadDeMadre(Id, TipoAccion.Edicion);
            }
        }

        void RecuperarMenores()
        {
            ModeloMenor modelomenor = new ModeloMenor();
            this.grdMenor.ItemsSource = modelomenor.ListarMenoresDeUnaFamilia(IdSeleccionado);
        }

        private void VerMenor(long IdMenor, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdFamilia = IdSeleccionado;
            objMenorWindow.IdSeleccionado = IdMenor;
            objMenorWindow.TipoAccion = TipoAccion;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objMenorWindow.Resultado == true))
                RecuperarMenores();
            objMenorWindow = null;
        }

        private void cmdNuevoMenor_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
                VerMenor(0, TipoAccion.Nuevo);
        }

        private void cmdEditarMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerMenor(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerMenor(Id, TipoAccion.Detalle);
            }
        }

        private void VerCorresponsabilidadDeMenor(long IdMenor, TipoAccion TipoAccion)
        {
            ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
            GrupoFamiliar grupofamiliar = new GrupoFamiliar();

            grupofamiliar = modelogrupofamiliar.RecuperarTitularHabilitado(IdSeleccionado);

            if (grupofamiliar != null)
            {
                this.Cursor = Cursors.Wait;
                frmCorresponsabilidadMenor objCorresponsabilidadMenorWindow = new frmCorresponsabilidadMenor();
                objCorresponsabilidadMenorWindow.IdFamilia = IdSeleccionado;
                objCorresponsabilidadMenorWindow.IdSeleccionado = IdMenor;
                objCorresponsabilidadMenorWindow.TipoAccion = TipoAccion;
                objCorresponsabilidadMenorWindow.Owner = this;
                objCorresponsabilidadMenorWindow.ShowDialog();
                this.Cursor = Cursors.Arrow;
                objCorresponsabilidadMenorWindow = null;
            }
            else
                MessageBox.Show("Se requiere especificar previamente titular de pago.", "Error");
        }

        private void cmdCorresponsabilidadMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadDeMenor(Id, TipoAccion.Edicion);
            }
        }

        void RecuperarMadresYTutoresALaVez()
        {
            ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
            this.grdTutor.ItemsSource = modelogrupofamiliar.ListarMadresYTutoresDeUnaFamilia(IdSeleccionado);
        }

        private void VerTutor(long IdTutor, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdFamilia = IdSeleccionado;
            objTutorWindow.IdSeleccionado = IdTutor;
            objTutorWindow.TipoAccion = TipoAccion;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objTutorWindow.Resultado == true))
                RecuperarMadresYTutoresALaVez();
            objTutorWindow = null;
        }

        private void cmdNuevoTutor_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
                VerTutor(0, TipoAccion.Nuevo);
        }

        private void cmdEditarTutor_Click(object sender, RoutedEventArgs e)
        {
            if (registrotitularpago != null)
            {
                Button Img = (Button)sender;
                if (Img.Tag != null)
                {
                    Int64 Id = (Int64)Img.Tag;
                    if (Id > 0)
                    {
                        if (registrotitularpago.Tipo =="Madre")
                            VerMadre(Id, TipoAccion.Edicion);
                        else
                            VerTutor(Id, TipoAccion.Edicion);
                    }
                }
            }
        }

        private void cmdDetalleTutor_Click(object sender, RoutedEventArgs e)
        {
            if (registrotitularpago != null)
            {
                Button Img = (Button)sender;
                if (Img.Tag != null)
                {
                    Int64 Id = (Int64)Img.Tag;
                    if (Id > 0)
                    {
                        if (registrotitularpago.Tipo == "Madre")
                            VerMadre(Id, TipoAccion.Detalle);
                        else
                            VerTutor(Id, TipoAccion.Detalle);
                    }
                }
            }
        }

        private void cmdEstablecerTutor_Click(object sender, RoutedEventArgs e)
        {
            if (registrotitularpago != null)
            {
                Button Img = (Button)sender;
                if (Img.Tag != null)
                {
                    Int64 Id = (Int64)Img.Tag;
                    if (Id > 0)
                    {
                        string NombreCompleto = "";

                        if (registrotitularpago.Tipo == "Madre")
                        {
                            ModeloMadre modelomadre = new ModeloMadre();
                            Madre __madre = new Madre();
                            __madre = modelomadre.Recuperar(Id);
                            NombreCompleto = __madre.NombreCompleto;
                        }
                        else
                        {
                            ModeloTutor modelotutor = new ModeloTutor();
                            Tutor __tutor = new Tutor();
                            __tutor = modelotutor.Recuperar(Id);
                            NombreCompleto = __tutor.NombreCompleto;
                        }

                        if (MessageBox.Show("¿Desea establecer a " + NombreCompleto + " como titular de pago para esta familia?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            ModeloGrupoFamiliar modelogrupofamiliar = new ModeloGrupoFamiliar();
                            modelogrupofamiliar.EstablecerTitularDePagoVigenteDeFamilia(IdSeleccionado, registrotitularpago.IdGrupoFamiliar);
                            RecuperarMadresYTutoresALaVez();
                        }
                    }
                }
            }
        }

        private void grdMadre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            madre = this.grdMadre.SelectedItem as Madre;
            this.Cursor = Cursors.Arrow;
        }

        private void grdMenor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            menor = this.grdMenor.SelectedItem as Menor;
            this.Cursor = Cursors.Arrow;
        }

        private void grdTutor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            registrotitularpago = this.grdTutor.SelectedItem as RegistroTitularPago;
            this.Cursor = Cursors.Arrow;
        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
