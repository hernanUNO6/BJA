﻿using Bja.Entidades;
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
    /// Lógica de interacción para frmInformacionAvanzada.xaml
    /// </summary>
    public partial class frmInformacionAvanzada : Window
    {
        private ModeloMadre modelomadre = new ModeloMadre();
        private ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
        private ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
        private ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
        private ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
        private ModeloTutor modelotutor = new ModeloTutor();
        private ModeloMenor modelomenor = new ModeloMenor();
        private Madre madre = new Madre();
        private CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();
        private Tutor tutormadre = new Tutor();
        private Menor menor = new Menor();
        private CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();
        private Tutor tutormenor = new Tutor();
        private int OpcionDeBusquedaAsignada = 0;

        public frmInformacionAvanzada()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ValoresEn = new string[] { "Madres", "Menores", "Tutores" };
            this.cboEn.ItemsSource = ValoresEn;
            this.cboEn.SelectedIndex = 0;
            this.ellMadres.Fill = null;
            this.ellMenores.Fill = null;
            this.ellTutoresDeMadres.Fill = null;
            this.ellTutoresDeMenores.Fill = null;
            this.txtBuscar.Focus();
        }

        private void VerMadre(long IdMadre, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmMadre objMadreWindow = new frmMadre();
            objMadreWindow.IdSeleccionado = IdMadre;
            objMadreWindow.TipoAccion = TipoAccion;
            objMadreWindow.Owner = this;
            objMadreWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objMadreWindow.Resultado == true))
            {
                this.Cursor = Cursors.Wait;

                OpcionDeBusquedaAsignada = 0;
                RealizarBusqueda();

                this.Cursor = Cursors.Arrow;
            }
            objMadreWindow = null;
        }

        private void cmdNuevaMadre_Click(object sender, RoutedEventArgs e)
        {
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

        private void VerCorresponsabilidadMadre(long IdCorresponsabilidadMadre, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmCorresponsabilidadMadre objCorresponsabilidadMadreWindow = new frmCorresponsabilidadMadre();
            objCorresponsabilidadMadreWindow.IdSeleccionado = IdCorresponsabilidadMadre;
            objCorresponsabilidadMadreWindow.TipoAccion = TipoAccion;
            objCorresponsabilidadMadreWindow.Owner = this;
            objCorresponsabilidadMadreWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion))
            {
                this.Cursor = Cursors.Wait;

                OpcionDeBusquedaAsignada = 0;
                RealizarBusqueda();

                this.Cursor = Cursors.Arrow;
            }
            objCorresponsabilidadMadreWindow = null;
        }

        private void cmdNuevaCorresponsabilidadMadre_Click(object sender, RoutedEventArgs e)
        {
            VerCorresponsabilidadMadre(0, TipoAccion.Nuevo);
        }

        private void cmdEditarCorresponsabilidadMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadMadre(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleCorresponsabilidadMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadMadre(Id, TipoAccion.Detalle);
            }
        }

        private void cmdEditarControlMadre_Click(object sender, RoutedEventArgs e)
        {
        }

        private void cmdDetalleControlMadre_Click(object sender, RoutedEventArgs e)
        {
        }

        private void VerMenor(long IdMenor, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmMenor objMenorWindow = new frmMenor();
            objMenorWindow.IdSeleccionado = IdMenor;
            objMenorWindow.TipoAccion = TipoAccion;
            objMenorWindow.Owner = this;
            objMenorWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objMenorWindow.Resultado == true))
            {
                this.Cursor = Cursors.Wait;

                OpcionDeBusquedaAsignada = 0;
                RealizarBusqueda();

                this.Cursor = Cursors.Arrow;
            }
            objMenorWindow = null;
        }

        private void cmdNuevoMenor_Click(object sender, RoutedEventArgs e)
        {
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

        private void VerCorresponsabilidadMenor(long IdCorresponsabilidadMenor, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmCorresponsabilidadMenor objCorresponsabilidadMenorWindow = new frmCorresponsabilidadMenor();
            objCorresponsabilidadMenorWindow.IdSeleccionado = IdCorresponsabilidadMenor;
            objCorresponsabilidadMenorWindow.TipoAccion = TipoAccion;
            objCorresponsabilidadMenorWindow.Owner = this;
            objCorresponsabilidadMenorWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if ((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion))
            {
                this.Cursor = Cursors.Wait;

                OpcionDeBusquedaAsignada = 0;
                RealizarBusqueda();

                this.Cursor = Cursors.Arrow;
            }
            objCorresponsabilidadMenorWindow = null;
        }

        private void cmdNuevaCorresponsabilidadMenor_Click(object sender, RoutedEventArgs e)
        {
            VerCorresponsabilidadMenor(0, TipoAccion.Nuevo);
        }

        private void cmdEditarCorresponsabilidadMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadMenor(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleCorresponsabilidadMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerCorresponsabilidadMenor(Id, TipoAccion.Detalle);
            }
        }

        private void cmdEditarControlMenor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void cmdDetalleControlMenor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void VerTutor(long IdTutor, TipoAccion TipoAccion)
        {
            this.Cursor = Cursors.Wait;
            frmTutor objTutorWindow = new frmTutor();
            objTutorWindow.IdSeleccionado = IdTutor;
            objTutorWindow.TipoAccion = TipoAccion;
            objTutorWindow.Owner = this;
            objTutorWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (((TipoAccion == TipoAccion.Nuevo) || (TipoAccion == TipoAccion.Edicion)) && (objTutorWindow.Resultado == true))
            {
                this.Cursor = Cursors.Wait;

                OpcionDeBusquedaAsignada = 0;
                RealizarBusqueda();

                this.Cursor = Cursors.Arrow;
            }
            objTutorWindow = null;
        }

        private void cmdEditarTutorMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleTutorMadre_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Detalle);
            }
        }

        private void cmdEditarTutorMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Edicion);
            }
        }

        private void cmdDetalleTutorMenor_Click(object sender, RoutedEventArgs e)
        {
            Button Img = (Button)sender;
            if (Img.Tag != null)
            {
                Int64 Id = (Int64)Img.Tag;
                if (Id > 0)
                    VerTutor(Id, TipoAccion.Detalle);
            }
        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RealizarBusqueda()
        {
            txtBuscar.Text = txtBuscar.Text.Trim();
            if (txtBuscar.Text.Length > 0)
            {
                this.grdMadre.ItemsSource = null;
                this.grdCorresponsabilidadMadre.ItemsSource = null;
                this.grdControlMadre.ItemsSource = null;
                this.grdTutorMadre.ItemsSource = null;
                this.grdMenor.ItemsSource = null;
                this.grdCorresponsabilidadMenor.ItemsSource = null;
                this.grdControlMenor.ItemsSource = null;
                this.grdTutorMenor.ItemsSource = null;

                this.ellMadres.Fill = null;
                this.ellMenores.Fill = null;
                this.ellTutoresDeMadres.Fill = null;
                this.ellTutoresDeMenores.Fill = null;

                switch (this.cboEn.SelectedValue.ToString())
                {
                    case "Madres":
                        OpcionDeBusquedaAsignada = 1;
                        this.ellMadres.Fill = Brushes.Black;
                        this.grdMadre.ItemsSource = modelomadre.ListarMadresPorCriterio(txtBuscar.Text);
                        if (this.grdMadre.Items.Count > 0)
                            this.grdMadre.SelectedIndex = 0;
                        break;
                    case "Menores":
                        OpcionDeBusquedaAsignada = 2;
                        this.ellMenores.Fill = Brushes.Black;
                        this.grdMenor.ItemsSource = modelomenor.ListarMenoresPorCriterio(txtBuscar.Text);
                        if (this.grdMenor.Items.Count > 0)
                            this.grdMenor.SelectedIndex = 0;
                        break;
                    case "Tutores":
                        OpcionDeBusquedaAsignada = 3;
                        this.ellTutoresDeMadres.Fill = Brushes.Black;
                        this.ellTutoresDeMenores.Fill = Brushes.Black;

                        this.grdTutorMadre.ItemsSource = modelotutor.ListarTutoresDeMadresPorCriterio(txtBuscar.Text);
                        if (this.grdTutorMadre.Items.Count > 0)
                            this.grdTutorMadre.SelectedIndex = 0;

                        this.grdTutorMenor.ItemsSource = modelotutor.ListarTutoresDeMenoresPorCriterio(txtBuscar.Text);
                        if (this.grdTutorMenor.Items.Count > 0)
                            this.grdTutorMenor.SelectedIndex = 0;
                        break;
                }
            }
        }

        private void cmdBuscar_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            OpcionDeBusquedaAsignada = 0;
            RealizarBusqueda();

            this.Cursor = Cursors.Arrow;
        }

        private void grdMadre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            madre = this.grdMadre.SelectedItem as Madre;

            if (OpcionDeBusquedaAsignada == 1)
            {
                this.grdCorresponsabilidadMadre.ItemsSource = null;
                this.grdControlMadre.ItemsSource = null;
                this.grdTutorMadre.ItemsSource = null;
                this.grdMenor.ItemsSource = null;
                this.grdCorresponsabilidadMenor.ItemsSource = null;
                this.grdControlMenor.ItemsSource = null;
                this.grdTutorMenor.ItemsSource = null;

                if (madre != null)
                {
                    this.grdMenor.ItemsSource = modelomenor.ListarHijosDeMadreATravesDeCorresponsabilidadDeMenor(madre.Id);
                    if (this.grdMenor.Items.Count > 0)
                        this.grdMenor.SelectedIndex = 0;
                }
            }

            if (madre != null)
            {
                this.grdCorresponsabilidadMadre.ItemsSource = modelocorresponsabilidadmadre.ListarCorresponsabilidadesDeMadre(madre.Id);
                if (this.grdCorresponsabilidadMadre.Items.Count > 0)
                    this.grdCorresponsabilidadMadre.SelectedIndex = 0;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void grdCorresponsabilidadMadre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            corresponsabilidadmadre = this.grdCorresponsabilidadMadre.SelectedItem as CorresponsabilidadMadre;

            if ((OpcionDeBusquedaAsignada == 1) || (OpcionDeBusquedaAsignada == 2))
            {
                if (corresponsabilidadmadre != null)
                {
                    if (corresponsabilidadmadre.IdTutor != null)
                    {
                        this.grdTutorMadre.ItemsSource = modelotutor.RecuperarTutor((long)corresponsabilidadmadre.IdTutor);
                        if (this.grdTutorMadre.Items.Count > 0)
                            this.grdTutorMadre.SelectedIndex = 0;
                    }
                }
            }
            if (corresponsabilidadmadre != null)
            {
                this.grdControlMadre.ItemsSource = modelocontrolmadre.ListarControlesDeCorresponsabilidadDeMadre(corresponsabilidadmadre.Id);
                if (this.grdControlMadre.Items.Count > 0)
                    this.grdControlMadre.SelectedIndex = 0;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void grdMenor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            menor = this.grdMenor.SelectedItem as Menor;

            if (OpcionDeBusquedaAsignada == 2)
            {
                this.grdMadre.ItemsSource = null;
                this.grdCorresponsabilidadMadre.ItemsSource = null;
                this.grdControlMadre.ItemsSource = null;
                this.grdTutorMadre.ItemsSource = null;
                this.grdCorresponsabilidadMenor.ItemsSource = null;
                this.grdControlMenor.ItemsSource = null;
                this.grdTutorMenor.ItemsSource = null;

                if (menor != null)
                {
                    this.grdMadre.ItemsSource = modelomadre.ListarMadresDeMenorATravesDeCorresponsabilidadDeMenor(menor.Id);
                    if (this.grdMadre.Items.Count > 0)
                        this.grdMadre.SelectedIndex = 0;
                }
            }
            if (menor != null)
            {
                this.grdCorresponsabilidadMenor.ItemsSource = modelocorresponsabilidadmenor.ListarCorresponsabilidadesDeMenor(menor.Id);
                if (this.grdCorresponsabilidadMenor.Items.Count > 0)
                    this.grdCorresponsabilidadMenor.SelectedIndex = 0;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void grdCorresponsabilidadMenor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            corresponsabilidadmenor = this.grdCorresponsabilidadMenor.SelectedItem as CorresponsabilidadMenor;

            if ((OpcionDeBusquedaAsignada == 1) || (OpcionDeBusquedaAsignada == 2))
            {
                if (corresponsabilidadmenor != null)
                {
                    if (corresponsabilidadmenor.IdTutor != null)
                    {
                        this.grdTutorMenor.ItemsSource = modelotutor.RecuperarTutor((long)corresponsabilidadmenor.IdTutor);
                        if (this.grdTutorMenor.Items.Count > 0)
                            this.grdTutorMenor.SelectedIndex = 0;
                    }
                }
            }
            if (corresponsabilidadmenor != null)
            {
                this.grdControlMenor.ItemsSource = modelocontrolmenor.ListarControlesDeCorresponsabilidadDeMenor(corresponsabilidadmenor.Id);
                if (this.grdControlMenor.Items.Count > 0)
                    this.grdControlMenor.SelectedIndex = 0;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void grdTutorMadre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            tutormadre = this.grdTutorMadre.SelectedItem as Tutor;
            if (OpcionDeBusquedaAsignada == 3)
            {
                this.grdMadre.ItemsSource = null;
                this.grdCorresponsabilidadMadre.ItemsSource = null;
                this.grdControlMadre.ItemsSource = null;

                if (tutormadre != null)
                {
                    this.grdMadre.ItemsSource = modelomadre.ListarMadresBajoTuicionDeTutor(tutormadre.Id);
                    if (this.grdMadre.Items.Count > 0)
                        this.grdMadre.SelectedIndex = 0;
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void grdTutorMenor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            tutormenor = this.grdTutorMenor.SelectedItem as Tutor;
            if (OpcionDeBusquedaAsignada == 3)
            {
                this.grdMenor.ItemsSource = null;
                this.grdCorresponsabilidadMenor.ItemsSource = null;
                this.grdControlMenor.ItemsSource = null;

                if (tutormenor != null)
                {
                    this.grdMenor.ItemsSource = modelomenor.ListarMenoresBajoTuicionDeTutor(tutormenor.Id);
                    if (this.grdMenor.Items.Count > 0)
                        this.grdMenor.SelectedIndex = 0;
                }
            }
            this.Cursor = Cursors.Arrow;
        }

    }

}
