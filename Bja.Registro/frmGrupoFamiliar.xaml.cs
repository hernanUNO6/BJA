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
        private Tutor tutor = new Tutor();

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
                RecuperarTutores();
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
                RecuperarMadres();
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

        void RecuperarTutores()
        {
            ModeloTutor modelotutor = new ModeloTutor();
            this.grdTutor.ItemsSource = modelotutor.ListarTutoresDeUnaFamilia(IdSeleccionado);
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
                RecuperarTutores();
            objTutorWindow = null;
        }

        private void cmdNuevoTutor_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
                VerTutor(0, TipoAccion.Nuevo);
        }

        private void cmdEditarTutor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleTutor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Detalle);
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
            tutor = this.grdTutor.SelectedItem as Tutor;
            this.Cursor = Cursors.Arrow;
        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
