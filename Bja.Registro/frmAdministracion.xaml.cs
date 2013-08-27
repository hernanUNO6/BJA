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
    /// Lógica de interacción para frmAdministracion.xaml
    /// </summary>
    public partial class frmAdministracion : Window
    {
        public frmAdministracion()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdMadres_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMadres = new frmLista();

            formularioListaMadres.NuevoRegistro += formularioListaMadres_NuevoRegistro;
            formularioListaMadres.DetallesRegistro += formularioListaMadres_DetallesRegistro;
            formularioListaMadres.ModificarRegistro += formularioListaMadres_ModificarRegistro;
            formularioListaMadres.BorrarRegistro += formularioListaMadres_BorrarRegistro;

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

        void formularioListaMadres_DetallesRegistro(object sender, IdentidadEventArgs fe)
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

        private void cmdTutores_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaTutores = new frmLista();

            formularioListaTutores.NuevoRegistro += formularioListaTutores_NuevoRegistro;
            formularioListaTutores.DetallesRegistro += formularioListaTutores_DetallesRegistro;
            formularioListaTutores.ModificarRegistro += formularioListaTutores_ModificarRegistro;
            formularioListaTutores.BorrarRegistro += formularioListaTutores_BorrarRegistro;

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

        void formularioListaTutores_DetallesRegistro(object sender, IdentidadEventArgs fe)
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

        private void cmdMenores_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMenores = new frmLista();

            formularioListaMenores.NuevoRegistro += formularioListaMenores_NuevoRegistro;
            formularioListaMenores.DetallesRegistro += formularioListaMenores_DetallesRegistro;
            formularioListaMenores.ModificarRegistro += formularioListaMenores_ModificarRegistro;
            formularioListaMenores.BorrarRegistro += formularioListaMenores_BorrarRegistro;

            ModeloMenor modelomenor = new ModeloMenor();

            formularioListaMenores.proveedorDatos = modelomenor;
            formularioListaMenores.titulo = "Menores";
            formularioListaMenores.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_NuevoRegistro(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdSeleccionado = 0;
            objMenorWindow.TipoAccion = TipoAccion.Nuevo;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            objMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdSeleccionado = fe.id;
            objMenorWindow.TipoAccion = TipoAccion.Detalle;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            objMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdSeleccionado = fe.id;
            objMenorWindow.TipoAccion = TipoAccion.Edicion;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            objMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_BorrarRegistro(object sender, IdentidadEventArgs fe)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Por Implementar.", "Mensaje");
        }

        private void cmdPassword_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmPassword objPasswordWindow = new frmPassword();
            objPasswordWindow.Owner = this;
            objPasswordWindow.ShowDialog();
            objPasswordWindow = null;
            this.Cursor = Cursors.Arrow;
        }

    }
}
