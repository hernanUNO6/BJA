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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModeloAsignacionMedico modeloasignacionmedico = new ModeloAsignacionMedico();
            AsignacionMedico asignacionmedico = new AsignacionMedico();

            //Por ahora el médico tiene código 1
            asignacionmedico = modeloasignacionmedico.EstablecimientoDeSaludHabilitado(1);

            if (asignacionmedico == null)
            {
                menFamilia.IsEnabled = false;
                menBusFam.IsEnabled = false;
                menNueFam.IsEnabled = false;
                menDatos.IsEnabled = false;
                menBusMad.IsEnabled = false;
                menBusMen.IsEnabled = false;
                menBusTut.IsEnabled = false;
                menBusAva.IsEnabled = false;
                menReporte.IsEnabled = false;
                menRep01.IsEnabled = false;
                menRep02.IsEnabled = false;
            }
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
                frmGrupoFamiliar objGrupoFamiliarWindow = new frmGrupoFamiliar();
                objGrupoFamiliarWindow.IdSeleccionado = Id;
                objGrupoFamiliarWindow.TipoAccion = TipoAccion.Nuevo;
                objGrupoFamiliarWindow.Owner = this;
                objGrupoFamiliarWindow.ShowDialog();
                objGrupoFamiliarWindow = null;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void menBusAva_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmInformacionAvanzada objInformacionAvanzadaWindow = new frmInformacionAvanzada();
            objInformacionAvanzadaWindow.Owner = this;
            objInformacionAvanzadaWindow.ShowDialog();
            objInformacionAvanzadaWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menSalir_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.endSession();
            this.Close();
        }

        private void menBusMad_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMadres = new frmLista();

            formularioListaMadres.ModificarRegistro += formularioListaMadres_ModificarRegistro;
            formularioListaMadres.DetallesRegistro += formularioListaMadres_DetallesRegistro;

            ModeloMadre modelomadre = new ModeloMadre();

            formularioListaMadres.proveedorDatos = modelomadre;
            formularioListaMadres.titulo = "Madres";
            formularioListaMadres.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdFamilia = 0;
            objMadreWindow.IdSeleccionado = fe.id;
            objMadreWindow.TipoAccion = TipoAccion.Edicion;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            objMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMadres_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdFamilia = 0;
            objMadreWindow.IdSeleccionado = fe.id;
            objMadreWindow.TipoAccion = TipoAccion.Detalle;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            objMadreWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menBusMen_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaMenores = new frmLista();

            formularioListaMenores.ModificarRegistro += formularioListaMenores_ModificarRegistro;
            formularioListaMenores.DetallesRegistro += formularioListaMenores_DetallesRegistro;

            ModeloMenor modelomenor = new ModeloMenor();

            formularioListaMenores.proveedorDatos = modelomenor;
            formularioListaMenores.titulo = "Menores";
            formularioListaMenores.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdFamilia = 0;
            objMenorWindow.IdSeleccionado = fe.id;
            objMenorWindow.TipoAccion = TipoAccion.Edicion;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            objMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaMenores_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdFamilia = 0;
            objMenorWindow.IdSeleccionado = fe.id;
            objMenorWindow.TipoAccion = TipoAccion.Detalle;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            objMenorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menBusTut_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmLista formularioListaTutores = new frmLista();

            formularioListaTutores.ModificarRegistro += formularioListaTutores_ModificarRegistro;
            formularioListaTutores.DetallesRegistro += formularioListaTutores_DetallesRegistro;

            ModeloTutor modelotutor = new ModeloTutor();

            formularioListaTutores.proveedorDatos = modelotutor;
            formularioListaTutores.titulo = "Tutores";
            formularioListaTutores.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutores_ModificarRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdFamilia = 0;
            objTutorWindow.IdSeleccionado = fe.id;
            objTutorWindow.TipoAccion = TipoAccion.Edicion;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        void formularioListaTutores_DetallesRegistro(object sender, IdentidadEventArgs fe)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdFamilia = 0;
            objTutorWindow.IdSeleccionado = fe.id;
            objTutorWindow.TipoAccion = TipoAccion.Detalle;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            objTutorWindow = null;
            this.Cursor = Cursors.Arrow;
        }

        private void menOperacion_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmOperacion objOperacionWindow = new frmOperacion();
            objOperacionWindow.Owner = this;
            objOperacionWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;

            if (objOperacionWindow.Resultado == true)
            {
                menFamilia.IsEnabled = true;
                menBusFam.IsEnabled = true;
                menNueFam.IsEnabled = true;
                menDatos.IsEnabled = true;
                menBusMad.IsEnabled = true;
                menBusMen.IsEnabled = true;
                menBusTut.IsEnabled = true;
                menBusAva.IsEnabled = true;
                menReporte.IsEnabled = true;
                menRep01.IsEnabled = true;
                menRep02.IsEnabled = true;
            }

            objOperacionWindow = null;
        }

        private void menRelFam_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            frmFamilias objFamiliasWindow = new frmFamilias();
            objFamiliasWindow.Owner = this;
            objFamiliasWindow.ShowDialog();
            objFamiliasWindow = null;
            this.Cursor = Cursors.Arrow;
        }

    }
}
