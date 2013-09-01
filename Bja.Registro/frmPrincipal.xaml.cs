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

        private void menCamCon_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmPassword objPasswordWindow = new frmPassword();
            objPasswordWindow.Owner = this;
            objPasswordWindow.ShowDialog();
            objPasswordWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menBusFam_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmBuscar objBuscarWindow = new frmBuscar();
            objBuscarWindow.Owner = this;
            objBuscarWindow.ShowDialog();
            objBuscarWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menNueFam_Click(object sender, RoutedEventArgs e)
        {
            bool ok;
            long Id;
            this.Cursor = Cursors.Wait;
            frmFamilia objFamiliaWindow = new frmFamilia();
            objFamiliaWindow.IdSeleccionado = 0;
            objFamiliaWindow.TipoAccion = TipoAccion.Nuevo;
            objFamiliaWindow.Owner = this;
            objFamiliaWindow.ShowDialog();
            ok = objFamiliaWindow.Resultado;
            Id = objFamiliaWindow.Id;
            objFamiliaWindow = null;
            this.Cursor = Cursors.Arrow;
            if (ok == true)
            {
                this.Cursor = Cursors.Wait;
                frmCarpetaFamiliar objCarpetaFamiliarWindow = new frmCarpetaFamiliar();
                objCarpetaFamiliarWindow.IdSeleccionado = Id;
                objCarpetaFamiliarWindow.Owner = this;
                objCarpetaFamiliarWindow.ShowDialog();
                objCarpetaFamiliarWindow = null;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void menSalir_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.endSession();
            this.Close();
        }

    }
}
