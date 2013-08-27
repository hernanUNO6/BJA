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
    /// Lógica de interacción para frmPrincipal.xaml
    /// </summary>
    public partial class frmPrincipal : Window
    {
        public frmPrincipal()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SessionManager.endSession();
        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.endSession();
            this.Close();
        }

        private void cmdCorresponsabilidadesDeMadres_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmCorresponsabilidadMadre objCorresponsabilidadMadreWindow = new frmCorresponsabilidadMadre();
            objCorresponsabilidadMadreWindow.IdSeleccionado = 0;
            objCorresponsabilidadMadreWindow.TipoAccion = TipoAccion.Nuevo;
            objCorresponsabilidadMadreWindow.Owner = this;
            objCorresponsabilidadMadreWindow.ShowDialog();
            objCorresponsabilidadMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void cmdCorresponsabilidadesDeMenores_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmCorresponsabilidadMenor objCorresponsabilidadMenorWindow = new frmCorresponsabilidadMenor();
            objCorresponsabilidadMenorWindow.IdSeleccionado = 0;
            objCorresponsabilidadMenorWindow.TipoAccion = TipoAccion.Nuevo;
            objCorresponsabilidadMenorWindow.Owner = this;
            objCorresponsabilidadMenorWindow.ShowDialog();
            objCorresponsabilidadMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void cmdAdministracion_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmAdministracion objAdministracionWindow = new frmAdministracion();
            objAdministracionWindow.Owner = this;
            objAdministracionWindow.ShowDialog();
            objAdministracionWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void cmdAvanzado_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmInformacionAvanzada objInformacionAvanzadaWindow = new frmInformacionAvanzada();
            objInformacionAvanzadaWindow.Owner = this;
            objInformacionAvanzadaWindow.ShowDialog();
            objInformacionAvanzadaWindow = null;
            this.Cursor = Cursors.Arrow;
        }

    }
}
