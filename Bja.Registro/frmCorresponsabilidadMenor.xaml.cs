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
  /// Lógica de interacción para frmCorresponsabilidadMenor.xaml
  /// </summary>
  public partial class frmCorresponsabilidadMenor : Window
  {
      long IdMenor { get; set; }
      long IdMadre { get; set; }
      long IdTutor { get; set; }
      public long IdSeleccionado { get; set; }
      public TipoAccion TipoAccion { get; set; }
      int CantidadDeControles { get; set; }  //debe definirse en la tabla parámetros.

      public frmCorresponsabilidadMenor()
      {
          this.Cursor = Cursors.Wait;
          InitializeComponent();
          this.Cursor = Cursors.Arrow;
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
          CantidadDeControles = 12;

          SoporteCombo.cargarEnumerador(this.cboTipoParentesco, typeof(TipoParentesco));

          //this.lblDepartamento.Content = "|";
          //this.lblEstablecimiento.Content = "|";
          this.lblNombresMenor.Content = "";
          this.lblFechaNacimientoMenor.Content = "";
          this.lblNombreMadre.Content = "";
          this.lblFechaNacimientoMadre.Content = "";
          this.lblNombreTutor.Content = "";
          this.lblFechaNacimientoTutor.Content = "";

          if (IdSeleccionado == 0)
          {
              this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
              this.dtpFechaSalida.SelectedDate = DateTime.Today;
              this.rdbNueva.IsChecked = true;
          }
      }

      private void cmdSalir_Click(object sender, RoutedEventArgs e)
      {
          this.Close();
      }

      private void cmdSeleccionarMenor_Click(object sender, RoutedEventArgs e)
      {
          this.Cursor = Cursors.Wait;
          frmLista formularioListaMenores = new frmLista();

          formularioListaMenores.NuevoRegistro += formularioListaMenores_NuevoRegistro;
          formularioListaMenores.DetallesRegistro += formularioListaMenores_DetallesRegistro;
          formularioListaMenores.ModificarRegistro += formularioListaMenores_ModificarRegistro;
          formularioListaMenores.BorrarRegistro += formularioListaMenores_BorrarRegistro;
          formularioListaMenores.SeleccionarRegistro += formularioListaMenores_SeleccionarRegistro;

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
          //MessageBox.Show("Por Implementar.", "Mensaje");
          if (IdMenor == fe.id && IdMenor > 0)
              MessageBox.Show("No se puede eliminar este registro de mneor.", "Mensaje");
          else
          {
              ModeloMenor modelomenor = new ModeloMenor();
              modelomenor.Eliminar(fe.id);
          }
      }

      void formularioListaMenores_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
      {
          IdMenor = fe.id;
          RecuperarMenor();
          //RecuperarUltimaCorresponsabilidad();
      }

      //void RecuperarUltimaCorresponsabilidad()
      //{
      //    ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
      //    //...
          
      //}

      void RecuperarMenor()
      {
          ModeloMenor modelomenor = new ModeloMenor();
          Menor menor = new Menor();
          menor = modelomenor.Recuperar(IdMenor);
          lblNombresMenor.Content = menor.Nombres + " " + menor.PrimerApellido + " " + menor.SegundoApellido;
          lblFechaNacimientoMenor.Content = string.Format("{0:dd/MM/yyyy}", menor.FechaNacimiento);
      }

      private void cmdSeleccionarMadre_Click(object sender, RoutedEventArgs e)
      {
          this.Cursor = Cursors.Wait;
          frmLista formularioListaMadres = new frmLista();

          formularioListaMadres.NuevoRegistro += formularioListaMadres_NuevoRegistro;
          formularioListaMadres.DetallesRegistro += formularioListaMadres_DetallesRegistro;
          formularioListaMadres.ModificarRegistro += formularioListaMadres_ModificarRegistro;
          formularioListaMadres.BorrarRegistro += formularioListaMadres_BorrarRegistro;
          formularioListaMadres.SeleccionarRegistro += formularioListaMadres_SeleccionarRegistro;

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

      void formularioListaMadres_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
      {
          IdMadre = fe.id;
          RecuperarMadre();
      }

      void RecuperarMadre()
      {
          ModeloMadre modelomadre = new ModeloMadre();
          Madre madre = new Madre();
          madre = modelomadre.Recuperar(IdMadre);
          lblNombreMadre.Content = madre.Nombres + " " + madre.PrimerApellido + " " + madre.SegundoApellido;
          lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
      }

      private void cmdDetallesMenor_Click(object sender, RoutedEventArgs e)
      {
          if (IdMenor > 0)
          {
              this.Cursor = Cursors.Wait;
              frmMenor objMenorWindow = new frmMenor();
              objMenorWindow.IdSeleccionado = IdMenor;
              objMenorWindow.TipoAccion = TipoAccion.Detalle;
              objMenorWindow.Owner = this;
              objMenorWindow.ShowDialog();
              objMenorWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarMenor();
          }
      }

      private void cmdModificarMenor_Click(object sender, RoutedEventArgs e)
      {
          if (IdMenor > 0)
          {
              this.Cursor = Cursors.Wait;
              frmMenor objMenorWindow = new frmMenor();
              objMenorWindow.IdSeleccionado = IdMenor;
              objMenorWindow.TipoAccion = TipoAccion.Edicion;
              objMenorWindow.Owner = this;
              objMenorWindow.ShowDialog();
              objMenorWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarMenor();
          }
      }

      private void cmdSeleccionarTutor_Click(object sender, RoutedEventArgs e)
      {
          this.Cursor = Cursors.Wait;
          frmLista formularioListaTutores = new frmLista();

          formularioListaTutores.NuevoRegistro += formularioListaTutores_NuevoRegistro;
          formularioListaTutores.DetallesRegistro += formularioListaTutores_DetallesRegistro;
          formularioListaTutores.ModificarRegistro += formularioListaTutores_ModificarRegistro;
          formularioListaTutores.BorrarRegistro += formularioListaTutores_BorrarRegistro;
          formularioListaTutores.SeleccionarRegistro += formularioListaTutores_SeleccionarRegistro;

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
          objTutorWindow.TipoAccion = TipoAccion.Nuevo;
          objTutorWindow.IdSeleccionado = 0;
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

      void formularioListaTutores_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
      {
          IdTutor = fe.id;
          RecuperarTutor();
      }

      void RecuperarTutor()
      {
          ModeloTutor modelotutor = new ModeloTutor();
          Tutor tutor = new Tutor();
          tutor = modelotutor.Recuperar(IdTutor);
          lblNombreTutor.Content = tutor.Nombres + " " + tutor.PrimerApellido + " " + tutor.SegundoApellido;
          lblFechaNacimientoTutor.Content = string.Format("{0:dd/MM/yyyy}", tutor.FechaNacimiento);
      }

      private void cmdDetallesTutor_Click(object sender, RoutedEventArgs e)
      {
          if (IdTutor > 0)
          {
              this.Cursor = Cursors.Wait;
              frmTutor objTutorWindow = new frmTutor();
              objTutorWindow.IdSeleccionado = IdTutor;
              objTutorWindow.TipoAccion = TipoAccion.Detalle;
              objTutorWindow.Owner = this;
              objTutorWindow.ShowDialog();
              objTutorWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarTutor();
          }
      }

      private void cmdModificarTutor_Click(object sender, RoutedEventArgs e)
      {
          if (IdTutor > 0)
          {
              this.Cursor = Cursors.Wait;
              frmTutor objTutorWindow = new frmTutor();
              objTutorWindow.IdSeleccionado = IdTutor;
              objTutorWindow.TipoAccion = TipoAccion.Edicion;
              objTutorWindow.Owner = this;
              objTutorWindow.ShowDialog();
              objTutorWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarTutor();
          }
      }

      private void cmdDetallesMadre_Click(object sender, RoutedEventArgs e)
      {
          if (IdMadre > 0)
          {
              this.Cursor = Cursors.Wait;
              frmMadre objMadreWindow = new frmMadre();
              objMadreWindow.IdSeleccionado = IdMadre;
              objMadreWindow.TipoAccion = TipoAccion.Detalle;
              objMadreWindow.Owner = this;
              objMadreWindow.ShowDialog();
              objMadreWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarMadre();
          }
      }

      private void cmdModificarMadre_Click(object sender, RoutedEventArgs e)
      {
          if (IdMadre > 0)
          {
              this.Cursor = Cursors.Wait;
              frmMadre objMadreWindow = new frmMadre();
              objMadreWindow.IdSeleccionado = IdMadre;
              objMadreWindow.TipoAccion = TipoAccion.Edicion;
              objMadreWindow.Owner = this;
              objMadreWindow.ShowDialog();
              objMadreWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarMadre();
          }
      }

      private void cmdGuardar_Click(object sender, RoutedEventArgs e)
      {
          if (IdSeleccionado == 0)
          {
              if (IdMenor > 0)
              {
                  ModeloCorresponsabilidadMenor modelocorresponsabilidadmenor = new ModeloCorresponsabilidadMenor();
                  CorresponsabilidadMenor corresponsabilidadmenor = new CorresponsabilidadMenor();

                  corresponsabilidadmenor.IdEstablecimientoSalud = 1;
                  if (rdbNueva.IsChecked == true)
                      corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Nueva;
                  else if (rdbTranferencia.IsChecked == true)
                      corresponsabilidadmenor.TipoInscripcionMenor = TipoInscripcion.Transferencia;

                  corresponsabilidadmenor.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
                  corresponsabilidadmenor.IdMenor = IdMenor;
                  corresponsabilidadmenor.DireccionMenor = "";
                  corresponsabilidadmenor.IdMadre = IdMadre;
                  corresponsabilidadmenor.DireccionMadre = "";
                  corresponsabilidadmenor.IdTutor = IdMadre;
                  corresponsabilidadmenor.DireccionTutor = "";
                  corresponsabilidadmenor.CodigoFormulario = txtCodigoFormulario.Text;
                  corresponsabilidadmenor.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
                  corresponsabilidadmenor.TipoSalidaMenor = 0;
                  corresponsabilidadmenor.Observaciones = "";
                  corresponsabilidadmenor.AutorizadoPor = txtAutorizado.Text;
                  corresponsabilidadmenor.CargoAutorizador = txtCargo.Text;

                  modelocorresponsabilidadmenor.Crear(corresponsabilidadmenor);
                  IdSeleccionado = corresponsabilidadmenor.Id;

                  //generamos los 6 registro de controles

                  ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
                  DateTime fechitaControles;

                  fechitaControles = Convert.ToDateTime(lblFechaNacimientoMenor.Content);
                  fechitaControles = fechitaControles.AddMonths(-1);

                  for (int i = 0; i < CantidadDeControles; i++)
                  {
                      fechitaControles = fechitaControles.AddMonths(2);

                      ControlMenor controlmenor = new ControlMenor();
                      controlmenor.IdCorresponsabilidadMenor = IdSeleccionado;
                      controlmenor.IdMedico = 1;
                      controlmenor.IdMadre = IdMadre;
                      controlmenor.IdTutor = IdTutor;
                      controlmenor.FechaProgramada = fechitaControles;
                      controlmenor.FechaControl = DateTime.Now;
                      controlmenor.TallaCm = 0;
                      controlmenor.PesoKg = 0;
                      controlmenor.NumeroControl = i + 1;
                      controlmenor.Observaciones = "";
                      controlmenor.EstadoPago = TipoEstadoPago.NoPagado;
                      if (IdTutor > 0)
                          controlmenor.TipoBeneficiario = TipoBeneficiario.Tutor;
                      else
                          controlmenor.TipoBeneficiario = TipoBeneficiario.Madre;
                      modelocontrolmenor.Crear(controlmenor);
                  }
                  txtCodigoFormulario.IsEnabled = false;
                  dtpFechaInscripcion.IsEnabled = false;
                  rdbNueva.IsEnabled = false;
                  rdbTranferencia.IsEnabled = false;
                  cmdGuardar.IsEnabled = false;
                  this.grdControl.ItemsSource = modelocontrolmenor.ListarControlesDeCorresponsabilidadDeMenor(IdSeleccionado);
              }
          }
      }

      void RecuperarControlMenor(long id, int opcion)
      {
          ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
          this.grdControl.ItemsSource = modelocontrolmenor.ListarControlesDeCorresponsabilidadDeMenor(IdSeleccionado);
      }

      private void cmdEditarControl_Click(object sender, RoutedEventArgs e)
      {
          Button Img = (Button)sender;
          if (Img.Tag != null)
          {
              Int64 Id = (Int64)Img.Tag;

              if (Id > 0)
              {
                  this.Cursor = Cursors.Wait;
                  frmControl objControlWindow = new frmControl();
                  objControlWindow.IdSeleccionado = Id;
                  objControlWindow.IdMenor = IdMenor;
                  objControlWindow.IdMadre = IdMadre;
                  objControlWindow.IdTutor = IdTutor;
                  objControlWindow.TipoAccion = TipoAccion.Edicion;
                  objControlWindow.TipoControl = TipoControl.Menor;
                  objControlWindow.Owner = this;
                  objControlWindow.ShowDialog();
                  if (objControlWindow.Resultado == true)
                      RecuperarControlMenor(1, 1);
                  objControlWindow = null;
                  this.Cursor = Cursors.Arrow;
              }
          }
      }

      private void cmdDetalleControl_Click(object sender, RoutedEventArgs e)
      {
          Button Img = (Button)sender;
          if (Img.Tag != null)
          {
              Int64 Id = (Int64)Img.Tag;

              if (Id > 0)
              {
                  this.Cursor = Cursors.Wait;
                  frmControl objControlWindow = new frmControl();
                  objControlWindow.IdSeleccionado = Id;
                  objControlWindow.IdMenor = IdMenor;
                  objControlWindow.IdMadre = IdMadre;
                  objControlWindow.IdTutor = IdTutor;
                  objControlWindow.TipoAccion = TipoAccion.Detalle;
                  objControlWindow.TipoControl = TipoControl.Menor;
                  objControlWindow.Owner = this;
                  objControlWindow.ShowDialog();
                  if (objControlWindow.Resultado == true)
                      RecuperarControlMenor(1, 1);
                  objControlWindow = null;
                  this.Cursor = Cursors.Arrow;
              }
          }
      }

      private void chkSalida_Checked(object sender, RoutedEventArgs e)
      {

      }

      private void chkSalida_Unchecked(object sender, RoutedEventArgs e)
      {

      }

      private void chkMadre_Checked(object sender, RoutedEventArgs e)
      {

      }

      private void chkMadre_Unchecked(object sender, RoutedEventArgs e)
      {

      }

      private void chkTutor_Checked(object sender, RoutedEventArgs e)
      {

      }

      private void chkTutor_Unchecked(object sender, RoutedEventArgs e)
      {

      }

  }
}
