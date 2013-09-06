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
    /// Lógica de interacción para frmFamilias.xaml
    /// </summary>
    public partial class frmFamilias : Window
    {
        public frmFamilias()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.grdFamilias.ItemsSource = null;
            this.txtPaterno.Focus();
        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtMaterno_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void txtPaterno_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void grdFamilias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cmdBuscar_Click(object sender, RoutedEventArgs e)
        {
        }

        private void cmdSeleccionar_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
