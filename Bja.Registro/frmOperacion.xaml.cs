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
    /// Lógica de interacción para frmOperacion.xaml
    /// </summary>
    public partial class frmOperacion : Window
    {
        public bool Resultado { get; set; }

        public frmOperacion()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModeloAsignacionMedico modeloasignacionmedico = new ModeloAsignacionMedico();

            this.cboActual.ItemsSource = modeloasignacionmedico.ListarEstablecimientoDeSaludHabilitado(SessionManager.getCurrentSession().User.IdUserRelation); //Sel 1 como médico
            this.cboActual.DisplayMemberPath = "Descripcion";
            this.cboActual.SelectedValuePath = "Id";
            if (this.cboActual.ItemsSource == null)
                this.cboActual.SelectedIndex = -1;
            else
                this.cboActual.SelectedIndex = 0;

            this.cboNuevo.ItemsSource = modeloasignacionmedico.ListarEstablecimientoDeSaludConfigurado(SessionManager.getCurrentSession().User.IdUserRelation); //Sel 1 como médico
            this.cboNuevo.DisplayMemberPath = "Descripcion";
            this.cboNuevo.SelectedValuePath = "Id";
            this.cboNuevo.SelectedIndex = -1;
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!(Convert.ToInt64(this.cboNuevo.SelectedValue) >= 0))
                MessageBox.Show("Se requiere especificar establecimiento de salud.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ModeloAsignacionMedico modeloasignacionmedico = new ModeloAsignacionMedico();
                AsignacionMedico asignacionmedico = new AsignacionMedico();

                modeloasignacionmedico.EstablecerEstablecimientoDeSaludComoVigenteParaUnMedico(SessionManager.getCurrentSession().User.IdUserRelation, Convert.ToInt64(this.cboNuevo.SelectedValue)); 
                Resultado = true;

                this.Close();
            }
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;

            this.Close();
        }

    }
}
