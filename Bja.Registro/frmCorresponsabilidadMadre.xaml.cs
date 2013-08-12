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
  /// Lógica de interacción para frmCorresponsabilidadMadre.xaml
  /// </summary>
  public partial class frmCorresponsabilidadMadre : Window
  {
      long IdMadre { get; set; }
      long IdTutor { get; set; }
      long[] IdControlMadre = new long[6];
      public long IdSeleccionado { get; set; }
      public TipoAccion TipoAccion { get; set; }

      public frmCorresponsabilidadMadre()
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
              this.dtpFechaFUM.SelectedDate = DateTime.Today;
              this.dtpFechaUltimoParto.SelectedDate = DateTime.Today;
              this.dtpFechaSalida.SelectedDate = DateTime.Today;
              this.cmdModificar11.IsEnabled = false;
              this.cmdModificar12.IsEnabled = false;
              this.cmdModificar13.IsEnabled = false;
              this.cmdModificar14.IsEnabled = false;
              this.cmdModificar15.IsEnabled = false;
              this.cmdModificar16.IsEnabled = false;
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
          }
      }

      private void cmdSalir_Click(object sender, RoutedEventArgs e)
      {
          this.Close();
      }

      private void cmdBuscar_Click(object sender, RoutedEventArgs e)
      {
          this.Cursor = Cursors.Wait;
          frmLista formularioListaTutor = new frmLista();

          formularioListaTutor.NuevoRegistro += formularioListaTutor_NuevoRegistro;
          formularioListaTutor.MostrarDetallesRegistro += formularioListaTutor_MostrarDetallesRegistro;
          formularioListaTutor.ModificarRegistro += formularioListaTutor_ModificarRegistro;
          formularioListaTutor.BorrarRegistro += formularioListaTutor_BorrarRegistro;
          formularioListaTutor.SeleccionarRegistro += formularioListaTutor_SeleccionarRegistro;

          ModeloTutor modelomadre = new ModeloTutor();

          formularioListaTutor.proveedorDatos = modelomadre;
          formularioListaTutor.titulo = "Madres";
          formularioListaTutor.ShowDialog();
          this.Cursor = Cursors.Arrow;
      }

      void formularioListaTutor_NuevoRegistro(object sender, EventArgs e)
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

      void formularioListaTutor_MostrarDetallesRegistro(object sender, IdentidadEventArgs fe)
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

      void formularioListaTutor_ModificarRegistro(object sender, IdentidadEventArgs fe)
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

      void formularioListaTutor_BorrarRegistro(object sender, IdentidadEventArgs fe)
      {
          //throw new NotImplementedException();
          MessageBox.Show("Por Implementar.", "Mensaje");
      }

      void formularioListaTutor_SeleccionarRegistro(object sender, IdentidadEventArgs fe)
      {
          //throw new NotImplementedException();
          //MessageBox.Show("Por Implementar.", "Mensaje");
          long IdTutor = fe.id;
          ModeloTutor modelotutor = new ModeloTutor();
          Tutor tutor = new Tutor();
          tutor = modelotutor.Recuperar(IdTutor);
          lblNomnbreMadre.Content = tutor.Nombres;
          lblPrimerApellido.Content = tutor.PrimerApellido;
          lblSegundoApellido.Content = tutor.SegundoApellido;
          lblTercerApellido.Content = tutor.TercerApellido;
          lblDocumentoIdentidad.Content = tutor.DocumentoIdentidad;
          lblFechaNacimientoMadre.Content = tutor.FechaNacimiento;
          IdTutor = fe.id;
      }

      private void cmdBuscarMadre_Click(object sender, RoutedEventArgs e)
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

      private void cmdBuscarTutor_Click(object sender, RoutedEventArgs e)
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
          objTutorWindow.IdSeleccionado = 0;
          objTutorWindow.TipoAccion = TipoAccion.Nuevo;
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

      void RecuperarMadre()
      {
          ModeloMadre modelomadre = new ModeloMadre();
          Madre madre = new Madre();
          madre = modelomadre.Recuperar(IdMadre);
          lblPrimerApellido.Content = madre.PrimerApellido;
          lblSegundoApellido.Content = madre.SegundoApellido;
          lblTercerApellido.Content = madre.TercerApellido;
          lblNomnbreMadre.Content = madre.Nombres;
          lblDocumentoIdentidad.Content = madre.DocumentoIdentidad;
          lblFechaNacimientoMadre.Content = string.Format("{0:dd/MM/yyyy}", madre.FechaNacimiento);
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

      private void cmdModifcarMadre_Click(object sender, RoutedEventArgs e)
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

      void RecuperarTutor()
      {
          ModeloTutor modelotutor = new ModeloTutor();
          Tutor tutor = new Tutor();
          tutor = modelotutor.Recuperar(IdTutor);
          lblPrimerApellidoTutor.Content = tutor.PrimerApellido;
          lblSegundoApellidoTutor.Content = tutor.SegundoApellido;
          lblTercerApellidoTutor.Content = tutor.TercerApellido;
          lblNombreTutor.Content = tutor.Nombres;
          lblDocumentoIdentidadTutor.Content = tutor.DocumentoIdentidad;
          lblFechaNacimientoTutor.Content = string.Format("{0:dd/MM/yyyy}", tutor.FechaNacimiento);
      }

      private void cmdGuardar_Click(object sender, RoutedEventArgs e)
      {
          if (IdMadre > 0 && IdSeleccionado == 0)
          {
              ModeloCorresponsabilidadMadre modelocorresponsabilidadmadre = new ModeloCorresponsabilidadMadre();
              CorresponsabilidadMadre corresponsabilidadmadre = new CorresponsabilidadMadre();

              corresponsabilidadmadre.IdEstablecimientoSalud = 1;
              if (rdbNueva.IsChecked == true)
                  corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Nueva;
              else if (rdbTransferencia.IsChecked == true)
                  corresponsabilidadmadre.TipoInscripcionMadre = TipoInscripcion.Transferencia;

              corresponsabilidadmadre.FechaInscripcion = dtpFechaInscripcion.SelectedDate.Value;
              corresponsabilidadmadre.IdMadre = IdMadre;
              corresponsabilidadmadre.IdTutor = IdTutor;
              corresponsabilidadmadre.CodigoFormulario = txtCodigoFormulario.Text;
              corresponsabilidadmadre.FechaUltimaMenstruacion = dtpFechaFUM.SelectedDate.Value; ;
              corresponsabilidadmadre.FechaUltimoParto = dtpFechaUltimoParto.SelectedDate.Value; ;
              corresponsabilidadmadre.NumeroEmbarazo = Convert.ToInt32(txtNumeroEmbarazo.Text);
              corresponsabilidadmadre.ARO = (bool)chkARO.IsChecked;
              corresponsabilidadmadre.FechaSalidaPrograma = dtpFechaSalida.SelectedDate.Value;
              corresponsabilidadmadre.TipoSalidaMadre = 0;
              corresponsabilidadmadre.Observaciones = "";
              corresponsabilidadmadre.AutorizadoPor = txtAutorizado.Text;
              corresponsabilidadmadre.CargoAutorizador = txtCargo.Text;
              corresponsabilidadmadre.DireccionMadre = "";
              corresponsabilidadmadre.DireccionTutor = "";

              modelocorresponsabilidadmadre.Crear(corresponsabilidadmadre);
              IdSeleccionado = corresponsabilidadmadre.Id;

              //generamos los 6 registro de controles

              ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
              DateTime fechitaControles;

              fechitaControles = dtpFechaFUM.SelectedDate.Value;
              fechitaControles = fechitaControles.AddMonths(-1);

              for (int i = 0; i < 6; i++)
              {
                  fechitaControles = fechitaControles.AddMonths(2);

                  ControlMadre controlmadre = new ControlMadre();
                  controlmadre.IdCorresponsabilidadMadre = IdSeleccionado;
                  controlmadre.IdMedico = 1;
                  controlmadre.IdTutor = IdTutor;
                  controlmadre.FechaProgramada = fechitaControles;
                  controlmadre.FechaControl = DateTime.Now;
                  controlmadre.TallaCm = 0;
                  controlmadre.PesoKg = 0;
                  controlmadre.NumeroControl = i + 1;
                  controlmadre.Observaciones = "";
                  controlmadre.EstadoPago = TipoEstadoPago.NoPagado;
                  controlmadre.TipoBeneficiario = TipoBeneficiario.Madre;
                  modelocontrolmadre.Crear(controlmadre);
                  IdControlMadre[i] = controlmadre.Id;
                  if (i == 0)
                      lblPrevisto11.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 1)
                      lblPrevisto12.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 2)
                      lblPrevisto13.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
                  else if (i == 3)
                      lblPrevisto14.Content = string.Format("{0:dd/MM/yyyy}", fechitaControles);
              }
              this.cmdModificar11.IsEnabled = true;
              this.cmdModificar12.IsEnabled = true;
              this.cmdModificar13.IsEnabled = true;
              this.cmdModificar14.IsEnabled = true;
              this.cmdModificar15.IsEnabled = true;
              this.cmdModificar16.IsEnabled = true;
              txtCodigoFormulario.IsEnabled = false;
              dtpFechaInscripcion.IsEnabled = false;
              rdbNueva.IsEnabled = false;
              rdbTransferencia.IsEnabled = false;
              dtpFechaFUM.IsEnabled = false;
              dtpFechaUltimoParto.IsEnabled = false;
              txtNumeroEmbarazo.IsEnabled = false;
              chkARO.IsEnabled = false;
              cmdGuardar.IsEnabled = false;
          }
      }

      private void cmdModificar11_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMadre[0] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMadre[0];
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = false;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMadre(IdControlMadre[0], 0);
          }
      }

      private void cmdModificar12_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMadre[1] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMadre[1];
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = false;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMadre(IdControlMadre[1], 1);
          }
      }

      private void cmdModificar13_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMadre[2] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMadre[2];
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = false;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMadre(IdControlMadre[2], 2);
          }
      }

      private void cmdModificar14_Click(object sender, RoutedEventArgs e)
      {
          if (IdControlMadre[3] > 0)
          {
              this.Cursor = Cursors.Wait;
              frmControl objControlWindow = new frmControl();
              objControlWindow.IdSeleccionado = IdControlMadre[3];
              objControlWindow.IdMadre = IdMadre;
              objControlWindow.IdTutor = IdTutor;
              objControlWindow.TipoAccion = TipoAccion.Edicion;
              objControlWindow.Tipo = false;
              objControlWindow.Owner = this;
              objControlWindow.ShowDialog();
              objControlWindow = null;
              this.Cursor = Cursors.Arrow;
              RecuperarControlMadre(IdControlMadre[3], 3);
          }
      }

      void RecuperarControlMadre(long id, int opcion)
      {
          ModeloControlMadre modelocontrolmadre = new ModeloControlMadre();
          ControlMadre controlmadre = new ControlMadre();
          controlmadre = modelocontrolmadre.Recuperar(id);
          if (opcion == 0)
          {
              lblPrevisto11.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaProgramada);
              lblTalla11.Content = Convert.ToString(controlmadre.TallaCm);
              lblPeso11.Content = Convert.ToString(controlmadre.PesoKg);
              lblFecha11.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaControl);
          }
          else if (opcion == 1)
          {
              lblPrevisto12.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaProgramada);
              lblTalla12.Content = Convert.ToString(controlmadre.TallaCm);
              lblPeso12.Content = Convert.ToString(controlmadre.PesoKg);
              lblFecha12.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaControl);
          }
          else if (opcion == 2)
          {
              lblPrevisto13.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaProgramada);
              lblTalla13.Content = Convert.ToString(controlmadre.TallaCm);
              lblPeso13.Content = Convert.ToString(controlmadre.PesoKg);
              lblFecha13.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaControl);
          }
          else if (opcion == 3)
          {
              lblPrevisto14.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaProgramada);
              lblTalla14.Content = Convert.ToString(controlmadre.TallaCm);
              lblPeso14.Content = Convert.ToString(controlmadre.PesoKg);
              lblFecha14.Content = string.Format("{0:dd/MM/yyyy}", controlmadre.FechaControl);
          }
      }

      //private void cmdPrueba_Click(object sender, RoutedEventArgs e)
      //{
      //    DateTime fechitaControles;
      //    string strMesesProgramados = "";
      //    string strMesesPerdidos = "";

      //    fechitaControles = dtpFechaFUM.SelectedDate.Value;
      //    fechitaControles = fechitaControles.AddMonths(-1);
      //    for (int i = 0; i < 4; i++)
      //    {
      //        fechitaControles = fechitaControles.AddMonths(2);
      //        //strMesesProgramados += "01/" + fechitaControles.Month.ToString() + "/" + fechitaControles.Year.ToString() + "      "; FECHA COMPLETA
      //        strMesesProgramados += string.Format("{0:MM/yyyy}", fechitaControles) + "            ";

      //        //controles perdidos---------------------------------------------------------------
      //        if ((fechitaControles.Month >= dtpFechaInscripcion.SelectedDate.Value.Month) && (fechitaControles.Year >= dtpFechaInscripcion.SelectedDate.Value.Year))
      //        {
      //            //strMesesPerdidos += fechitaControles.Date; FECHA COMPLETA
      //            strMesesPerdidos += string.Format("{0:MM/yyyy}", fechitaControles) + "        ";
      //        }
      //    }
      //    lblFechas.Content = "Fechas: " + strMesesProgramados;
      //    lblMesesPerdidos.Content = "A Pagar: " + strMesesPerdidos;
      //}

  }
}
