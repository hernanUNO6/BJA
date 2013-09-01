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
        private Familia _familia = new Familia();

        public frmGrupoFamiliar()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
                RecuperarFamilia();
        }

        private void cmdNuevaMadre_Click(object sender, RoutedEventArgs e)
        {
            if (IdSeleccionado > 0)
            {
                bool ok;
                this.Cursor = Cursors.Wait;
                frmMadre objMadreWindow = new frmMadre();
                objMadreWindow.IdFamilia = IdSeleccionado;
                objMadreWindow.IdSeleccionado = 0;
                objMadreWindow.TipoAccion = TipoAccion.Nuevo;
                objMadreWindow.Owner = this;
                objMadreWindow.ShowDialog();
                ok = objMadreWindow.Resultado;
                objMadreWindow = null;
                this.Cursor = Cursors.Arrow;
                if (ok == true)
                { 
                    //RecargarMasdres
                }
            }
        }

        private void cmdEditarMadre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDetalleMadre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCorresponsabilidadMadre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNuevoMenor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEditarMenor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDetalleMenor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCorresponsabilidadMenor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNuevoTutor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEditarTutor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDetalleTutor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
