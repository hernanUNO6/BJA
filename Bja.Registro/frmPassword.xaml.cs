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
    /// Lógica de interacción para frmPassword.xaml
    /// </summary>
    public partial class frmPassword : Window
    {
        public frmPassword()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pasContrasenaActual.Focus();

            //ModeloDepartamento modelodepartamento = new ModeloDepartamento();
            //ModeloProvincia modeloprovincia = new ModeloProvincia();
            //ModeloMunicipio modelomunicipio = new ModeloMunicipio();
            //for (long i = 1; i < 10; i++)
            //{
            //    Departamento departamento = new Departamento();
            //    departamento.Id = i;
            //    departamento.IdSesion = SessionManager.getCurrentSession().Id;
            //    departamento.FechaUltimaTransaccion = DateTime.Now;
            //    departamento.FechaRegistro = DateTime.Now;
            //    departamento.EstadoRegistro = TipoEstadoRegistro.Vigente;
            //    switch (i)
            //    {
            //        case 1:
            //            departamento.Codigo = "CHU";
            //            departamento.Descripcion = "Chuquisaca";
            //            break;
            //        case 2:
            //            departamento.Codigo = "LPZ";
            //            departamento.Descripcion = "La Paz";
            //            break;
            //        case 3:
            //            departamento.Codigo = "CBA";
            //            departamento.Descripcion = "Cochabamba";
            //            break;
            //        case 4:
            //            departamento.Codigo = "ORU";
            //            departamento.Descripcion = "Oruro";
            //            break;
            //        case 5:
            //            departamento.Codigo = "POT";
            //            departamento.Descripcion = "Potosí";
            //            break;
            //        case 6:
            //            departamento.Codigo = "TAR";
            //            departamento.Descripcion = "Tarija";
            //            break;
            //        case 7:
            //            departamento.Codigo = "SCZ";
            //            departamento.Descripcion = "Santa Cruz";
            //            break;
            //        case 8:
            //            departamento.Codigo = "BEN";
            //            departamento.Descripcion = "Beni";
            //            break;
            //        case 9:
            //            departamento.Codigo = "PAN";
            //            departamento.Descripcion = "Pando";
            //            break;
            //    }
            //    modelodepartamento.Crear(departamento);
            //    Provincia provincia = new Provincia();
            //    provincia.Id = i;
            //    provincia.IdSesion = SessionManager.getCurrentSession().Id;
            //    provincia.FechaUltimaTransaccion = DateTime.Now;
            //    provincia.FechaRegistro = DateTime.Now;
            //    provincia.EstadoRegistro = TipoEstadoRegistro.Vigente;
            //    provincia.IdDepartamento = departamento.Id;
            //    provincia.Codigo = i.ToString();
            //    if (i == 1)
            //        provincia.Descripcion = "Azurduy";
            //    else if (i == 2)
            //        provincia.Descripcion = "Murillo";
            //    else if (i == 3)
            //        provincia.Descripcion = "Mizque";
            //    else if (i == 4)
            //        provincia.Descripcion = "Pagador";
            //    else if (i == 5)
            //        provincia.Descripcion = "Tomás Frías";
            //    else if (i == 6)
            //        provincia.Descripcion = "OConnor";
            //    else if (i == 7)
            //        provincia.Descripcion = "Sara";
            //    else if (i == 8)
            //        provincia.Descripcion = "Moxox";
            //    else if (i == 9)
            //        provincia.Descripcion = "Abuná";
            //    modeloprovincia.Crear(provincia);

            //    Municipio municipio = new Municipio();
            //    municipio.Id = i;
            //    municipio.IdSesion = SessionManager.getCurrentSession().Id;
            //    municipio.FechaUltimaTransaccion = DateTime.Now;
            //    municipio.FechaRegistro = DateTime.Now;
            //    municipio.EstadoRegistro = TipoEstadoRegistro.Vigente;
            //    municipio.IdProvincia = provincia.Id;
            //    municipio.Codigo = i.ToString();
            //    if (i == 1)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Sucre";
            //    else if (i == 2)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de La Paz";
            //    else if (i == 3)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Cochabamba";
            //    else if (i == 4)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Oruro";
            //    else if (i == 5)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Potosí";
            //    else if (i == 6)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Tarija";
            //    else if (i == 7)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Santa Cruz de la Sierra";
            //    else if (i == 8)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Trinidad";
            //    else if (i == 9)
            //        municipio.Descripcion = "Gobierno Autónomo Municipal de Cobija";
            //    modelomunicipio.Crear(municipio);
            //}
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
