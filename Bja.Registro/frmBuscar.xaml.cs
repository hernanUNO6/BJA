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
    /// Lógica de interacción para frmBuscar.xaml
    /// </summary>
    public partial class frmBuscar : Window
    {
        public frmBuscar()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.grdResultado.ItemsSource = null;
            this.txtDocIde.Focus();
        }

        void Buscar()
        {
            //ModeloBusqueda modelobusqueda = new ModeloBusqueda();
            //this.grdResultado.ItemsSource = modelobusqueda.ListarBusquedaDeRegistrosPorCriterio(txtDocIde.Text, txtPaterno.Text, txtMaterno.Text, txtNombres.Text);
        }

        private void grdResultado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmdSeleccionar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdBuscar_Click(object sender, RoutedEventArgs e)
        {
            if ((this.txtDocIde.Text.Trim().Length > 0) || (this.txtPaterno.Text.Trim().Length > 0) || (this.txtMaterno.Text.Trim().Length > 0) || (this.txtNombres.Text.Trim().Length > 0))
                Buscar();
        }

        private void txtDocIde_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        private void txtPaterno_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        private void txtMaterno_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        private void txtNombres_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

    }
}
