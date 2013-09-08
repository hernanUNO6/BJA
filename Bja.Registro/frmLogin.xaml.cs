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
  /// Lógica de interacción para frmLogin.xaml
  /// </summary>
  public partial class frmLogin : Window
  {
    public frmLogin()
    {
      this.Cursor = Cursors.Wait;
      InitializeComponent();
      this.Cursor = Cursors.Arrow;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        this.txtUsuario.Focus();
    }

      private void cmdAceptar_Click(object sender, RoutedEventArgs e)
    {
        bool ok = false;
        if (!(this.txtUsuario.Text.Length > 0))
        {
            MessageBox.Show("Se requiere especificar cuenta de usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            ok = true;
        }
        else if (!(this.pasContrasena.Password.Length > 0))
        {
            MessageBox.Show("Se requiere especificar contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            ok = true;
        }
        else if (!(this.pasContrasena.Password.Length >= 3))
        {
            MessageBox.Show("Se requiere especificar contraseña válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            ok = true;
        }
        if (ok == false)
        {
            var rbac = new Rbac();
            User user = rbac.authenticate(this.txtUsuario.Text, this.pasContrasena.Password);
            if (user != null)
            {
                SessionManager.initSession(user);

                this.Hide();

                frmPrincipal objPrincipal = new frmPrincipal();
                bool? Resultado = objPrincipal.ShowDialog();
                if (Resultado == false)
                {
                    objPrincipal.Close();
                    Application.Current.Shutdown();
                }
            }
            else
                MessageBox.Show("Usuario o contraseña no válidos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void cmdCancelar_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

  }
}
