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
      long[] IdControlMenor = new long[13];
      public long IdSeleccionado { get; set; }
      public TipoAccion TipoAccion { get; set; }

      public frmCorresponsabilidadMenor()
      {
          this.Cursor = Cursors.Wait;
          InitializeComponent();
          this.Cursor = Cursors.Arrow;
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
          if (IdSeleccionado == 0)
          {
              this.dtpFechaInscripcion.SelectedDate = DateTime.Today;
              this.dtpFechaSalida.SelectedDate = DateTime.Today;
              this.cmdModificar11.IsEnabled = false;
              this.cmdModificar12.IsEnabled = false;
              this.cmdModificar13.IsEnabled = false;
              this.cmdModificar14.IsEnabled = false;
              this.cmdModificar15.IsEnabled = false;
              this.cmdModificar16.IsEnabled = false;
              this.cmdModificar21.IsEnabled = false;
              this.cmdModificar22.IsEnabled = false;
              this.cmdModificar23.IsEnabled = false;
              this.cmdModificar24.IsEnabled = false;
              this.cmdModificar25.IsEnabled = false;
              this.cmdModificar26.IsEnabled = false;
              lblPrevisto11.Content = "";
              lblTalla11.Content = "";
              lblPeso11.Content = "";
              lblFecha11.Content = "";
              lblPrevisto12.Content = "";
              lblTalla12.Content = "";
              lblPeso12.Content = "";
              lblFecha12.Content = "";
              lblPrevisto13.Content = "";
              lblTalla13.Content = "";
              lblPeso13.Content = "";
              lblFecha13.Content = "";
              lblPrevisto14.Content = "";
              lblTalla14.Content = "";
              lblPeso14.Content = "";
              lblFecha14.Content = "";
              lblPrevisto15.Content = "";
              lblTalla15.Content = "";
              lblPeso15.Content = "";
              lblFecha15.Content = "";
              lblPrevisto16.Content = "";
              lblTalla16.Content = "";
              lblPeso16.Content = "";
              lblFecha16.Content = "";
              lblPrevisto21.Content = "";
              lblTalla21.Content = "";
              lblPeso21.Content = "";
              lblFecha21.Content = "";
              lblPrevisto22.Content = "";
              lblTalla22.Content = "";
              lblPeso22.Content = "";
              lblFecha22.Content = "";
              lblPrevisto23.Content = "";
              lblTalla23.Content = "";
              lblPeso23.Content = "";
              lblFecha23.Content = "";
              lblPrevisto24.Content = "";
              lblTalla24.Content = "";
              lblPeso24.Content = "";
              lblFecha24.Content = "";
              lblPrevisto25.Content = "";
              lblTalla25.Content = "";
              lblPeso25.Content = "";
              lblFecha25.Content = "";
              lblPrevisto26.Content = "";
              lblTalla26.Content = "";
              lblPeso26.Content = "";
              lblFecha26.Content = "";
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
          formularioListaMenores.MostrarDetallesRegistro += formularioListaMenores_MostrarDetallesRegistro;
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

      void formularioListaMenores_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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
      }

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
          formularioListaMadres.MostrarDetallesRegistro += formularioListaMadres_MostrarDetallesRegistro;
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

      void formularioListaMadres_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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
          formularioListaTutores.MostrarDetallesRegistro += formularioListaTutores_MostrarDetallesRegistro;
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

      void formularioListaTutores_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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
          if (IdMenor > 0 && IdSeleccionado == 0)
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
              corresponsabilidadmenor.IdMadre = IdMadre;
              corresponsabilidadmenor.IdTutor = IdMadre;
              corresponsabilidadmenor.CodigoFormulario = txtCodigoFormulario.Text;
              corresponsabilidadmenor.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
              corresponsabilidadmenor.TipoSalidaMenor = 0;
              corresponsabilidadmenor.Observaciones = "";
              corresponsabilidadmenor.AutorizadoPor = txtAutorizado.Text;
              corresponsabilidadmenor.CargoAutorizador = txtCargo.Text;
              corresponsabilidadmenor.DireccionMadre = "";
              corresponsabilidadmenor.DireccionMenor = "";
              corresponsabilidadmenor.DireccionTutor = "";

              modelocorresponsabilidadmenor.Crear(corresponsabilidadmenor);
              IdSeleccionado = corresponsabilidadmenor.Id;

              //generamos los 6 registro de controles

              ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
              DateTime fechitaControles;

              fechitaControles = Convert.ToDateTime(lblFechaNacimientoMenor.Content);
              fechitaControles = fechitaControles.AddMonths(-1);

              for (int i = 0; i < 12; i++)
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
                  controlmenor.TipoBeneficiario = TipoBeneficiario.Madre;
                  modelocontrolmenor.Crear(controlmenor);
                  IdControlMenor[i] = controlmenor.Id;
                  if (i == 0)
                      lblPrevisto11.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 1)
                      lblPrevisto12.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 2)
                      lblPrevisto13.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 3)
                      lblPrevisto14.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 4)
                      lblPrevisto15.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 5)
                      lblPrevisto16.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 6)
                      lblPrevisto21.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 7)
                      lblPrevisto22.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 8)
                      lblPrevisto23.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 9)
                      lblPrevisto24.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 10)
                      lblPrevisto25.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 11)
                      lblPrevisto26.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
              }
              this.cmdModificar11.IsEnabled = true;
              this.cmdModificar12.IsEnabled = true;
              this.cmdModificar13.IsEnabled = true;
              this.cmdModificar14.IsEnabled = true;
              this.cmdModificar15.IsEnabled = true;
              this.cmdModificar16.IsEnabled = true;
              this.cmdModificar21.IsEnabled = true;
              this.cmdModificar22.IsEnabled = true;
              this.cmdModificar23.IsEnabled = true;
              this.cmdModificar24.IsEnabled = true;
              this.cmdModificar25.IsEnabled = true;
              this.cmdModificar26.IsEnabled = true;
              txtCodigoFormulario.IsEnabled = false;
              dtpFechaInscripcion.IsEnabled = false;
              rdbNueva.IsEnabled = false;
              rdbTranferencia.IsEnabled = false;
              cmdGuardar.IsEnabled = false;
          }
      }

      private void cmdModificar11_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[0] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[0];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[0], 0);
          }
      }

      private void cmdModificar12_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[1] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[1];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[1], 1);
          }
      }

      private void cmdModificar13_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[2] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[2];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[2], 2);
          }
      }

      private void cmdModificar14_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[3] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[3];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[3], 3);
          }
      }

      private void cmdModificar15_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[4] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[4];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[4], 4);
          }
      }

      private void cmdModificar16_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[5] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[5];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[5], 5);
          }
      }

      private void cmdModificar21_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[6] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[6];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[6], 6);
          }
      }

      private void cmdModificar22_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[7] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[7];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[7], 7);
          }
      }

      private void cmdModificar23_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[8] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[8];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[8], 8);
          }
      }

      private void cmdModificar24_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[9] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[9];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[9], 9);
          }
      }

      private void cmdModificar25_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[10] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[10];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[10], 10);
          }
      }

      private void cmdModificar26_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMenor[11] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMenor[11];
              objControlWindow.IdMenor = IdMenor;
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = true;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMenor(IdControlMenor[11], 11);
          }
      }

      void RecuperarControlMenor(long id, int opcion)
      {
          ModeloControlMenor modelocontrolmenor = new ModeloControlMenor();
          ControlMenor controlmenor = new ControlMenor();
          controlmenor = modelocontrolmenor.Recuperar(id);
          if (opcion == 0)
          {
              lblPrevisto11.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla11.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso11.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha11.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 1)
          {
              lblPrevisto12.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla12.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso12.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha12.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 2)
          {
              lblPrevisto13.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla13.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso13.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha13.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 3)
          {
              lblPrevisto14.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla14.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso14.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha14.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 4)
          {
              lblPrevisto15.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla15.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso15.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha15.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 5)
          {
              lblPrevisto16.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla16.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso16.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha16.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 6)
          {
              lblPrevisto21.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla21.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso21.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha21.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 7)
          {
              lblPrevisto22.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla22.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso22.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha22.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 8)
          {
              lblPrevisto23.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla23.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso23.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha23.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 9)
          {
              lblPrevisto24.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla24.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso24.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha24.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 10)
          {
              lblPrevisto25.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla25.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso25.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha25.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
          else if (opcion == 11)
          {
              lblPrevisto26.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaProgramada);
              lblTalla26.Content = Convert.ToString(controlmenor.TallaCm);
              lblPeso26.Content = Convert.ToString(controlmenor.PesoKg);
              lblFecha26.Content = string.Format("{0:dd/MM/yyyy}", controlmenor.FechaControl);
          }
      }

      //private void cmdPrueba_Click(object sender, RoutedEventArgs e)
      //{
      //    if (IdMenor>0)
      //    {
      //        DateTime fechitaControles;
      //        string strMesesProgramados = "";
      //        string strMesesPerdidos = "";

      //        fechitaControles = Convert.ToDateTime(lblFechaNacimientoMenor.Content);
      //        fechitaControles = fechitaControles.AddMonths(-1);
      //        for (int i = 0; i < 12; i++)
      //        {
      //            fechitaControles = fechitaControles.AddMonths(2);
      //            //strMesesProgramados += "01/" + fechitaControles.Month.ToString() + "/" + fechitaControles.Year.ToString() + "      "; FECHA COMPLETA
      //            strMesesProgramados += string.Format("{0:MM/yyyy}", fechitaControles) + " ";

      //            if ((fechitaControles.Month >= dtpFechaInscripcion.SelectedDate.Value.Month) && (fechitaControles.Year >= dtpFechaInscripcion.SelectedDate.Value.Year))
      //            {
      //                //strMesesPerdidos += fechitaControles.Date; FECHA COMPLETA
      //                strMesesPerdidos += string.Format("{0:MM/yyyy}", fechitaControles) + " ";
      //            }
      //        }
      //        lblFechas.Content = "Fechas: " + strMesesProgramados;
      //        lblMesesPerdidos.Content = "A Pagar: " + strMesesPerdidos;
      //    }
      //}

  }
}
