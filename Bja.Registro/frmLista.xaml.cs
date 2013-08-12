using Bja.Soporte.Paginacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para frmLista.xaml
    /// </summary>
    public partial class frmLista : Window
    {
        //entrada
        const int ITEMS_POR_PAGINA = 10;
        public string titulo { get; set; }
        public IGrillaPaginada proveedorDatos { get; set; }
        public bool formularioItemSeleccionado { get; set; }

        //public EnumModoListaRegistros modoListaRegistros { get; set; }

        private ResultadoPaginacion _resultadoBusquedaPaginacion { get; set; }
        private Paginacion _paginacion;

        //private Visibility  mostrarBotonDetalles { get; set; }

        #region Definicion EventoNuevoRegistro

        public event EventHandler NuevoRegistro;

        private void OnNuevoRegistro()
        {
            // release any resources here
            if (NuevoRegistro != null)
            {
                // someone is subscribed, throw event
                NuevoRegistro(this, new EventArgs());
            }
        }
        #endregion

        #region Definicion EventoDetallesRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler MostrarDetallesRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnMostrarDetallesRegistro(Int64 id)
        {
            if (MostrarDetallesRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                MostrarDetallesRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        #region Definicion EventoModificarRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler ModificarRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnModificarRegistro(Int64 id)
        {
            if (ModificarRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                ModificarRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        #region Definicion EventoBorrarRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler BorrarRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnBorrarRegistro(Int64 id)
        {
            if (BorrarRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                BorrarRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        #region Definicion EventoSeleccionarRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler SeleccionarRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnSeleccionarRegistro(Int64 id)
        {
            if (SeleccionarRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                SeleccionarRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        #region Definicion EventoCorresponsabilidadRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler CorresponsabilidadRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnCorresponsabilidadRegistro(Int64 id)
        {
            if (CorresponsabilidadRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                CorresponsabilidadRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        #region Definicion EventoDependienteRegistro

        // Now, create a public event "FireEvent" whose type is our FireEventHandler delegate. 
        public event IdentidadEventHandler DependienteRegistro;

        // This will be the starting point of our event-- it will create FireEventArgs
        // and then raise the event, passing FireEventArgs. 
        private void OnDependienteRegistro(Int64 id)
        {
            if (DependienteRegistro != null)
            {
                IdentidadEventArgs IdentidadArgs = new IdentidadEventArgs(id);

                // Now, raise the event by invoking the delegate. Pass in 
                // the object that initated the event (this) as well as FireEventArgs. 
                // The call must match the signature of FireEventHandler.
                DependienteRegistro(this, IdentidadArgs);
            }
        }
        #endregion

        public frmLista()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCriterioBusqueda.Focus();

            //inicializa datos
            lblTitulo.Content = titulo;

            Int64 totalCount = proveedorDatos.totalRegistros();
            _paginacion = new Paginacion();
            _paginacion.setPaginationTextFormat("   Página {current_page} de {total_pages}   "); //"   Página {current_page} de {total_pages} ({page_start_record} - {page_end_record})   "
            //buscar datos iniciales

            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(0, ITEMS_POR_PAGINA);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            _paginacion.TotalRecords = _resultadoBusquedaPaginacion.totalRegistros;
            _paginacion.totalEncontrados = _resultadoBusquedaPaginacion.totalEncontrados;
            _paginacion.InitPagination(_resultadoBusquedaPaginacion.totalRegistros,
                                            _resultadoBusquedaPaginacion.totalEncontrados, ITEMS_POR_PAGINA);

            lblPagina.Content = _paginacion.getPaginationText();

            //if (modoListaRegistros == EnumModoListaRegistros.seleccion)
            //{
            //    //detalles
            //    dataGridRegistros.Columns[1].Visibility = System.Windows.Visibility.Visible;
            //    //modificar
            //    dataGridRegistros.Columns[2].Visibility = System.Windows.Visibility.Hidden;
            //    //borrar
            //    dataGridRegistros.Columns[3].Visibility = System.Windows.Visibility.Hidden;
            //    //seleccionar
            //    dataGridRegistros.Columns[4].Visibility = System.Windows.Visibility.Visible;
            //}

            //verifica que eventos de botones se registraron para mostrar los botones
            if (NuevoRegistro == null)
            {
                buttonNuevo.Visibility = System.Windows.Visibility.Hidden;
            }

            if (MostrarDetallesRegistro == null)
            {
                //mostrarBotonDetalles = System.Windows.Visibility.Hidden;
                dataGridRegistros.Columns[1].Visibility = System.Windows.Visibility.Hidden;
            }

            if (ModificarRegistro == null)
            {
                dataGridRegistros.Columns[2].Visibility = System.Windows.Visibility.Hidden;
            }

            if (BorrarRegistro == null)
            {
                dataGridRegistros.Columns[3].Visibility = System.Windows.Visibility.Hidden;
            }

            if (SeleccionarRegistro == null)
            {
                dataGridRegistros.Columns[4].Visibility = System.Windows.Visibility.Hidden;
            }

            if (CorresponsabilidadRegistro == null)
            {
                dataGridRegistros.Columns[5].Visibility = System.Windows.Visibility.Hidden;
            }

            if (DependienteRegistro == null)
            {
                dataGridRegistros.Columns[6].Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private void buttonNuevo_Click(object sender, RoutedEventArgs e)
        {
            //disparar evento para que se despliegue ventana de nuevo
            OnNuevoRegistro();

            //vuelve a cargar los datos
            //determinar página
            _paginacion.MoveToFirstPage();
            //volver a llamar a fuente de datos
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void txtCriterioBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            //buscar datos en lista paginada
            //if (txtCriterioBusqueda.Text.Count() <= 3)
            //{
            //    return;
            //}

            //buscar datos
            //_paginacion.InitPagination();
            //se debe resetear la página
            _paginacion.MoveToFirstPage();
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            _paginacion.totalEncontrados = _resultadoBusquedaPaginacion.totalEncontrados;
            _paginacion.TotalRecords = _resultadoBusquedaPaginacion.totalRegistros;
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void cmdPrimero_Click(object sender, RoutedEventArgs e)
        {
            //determinar página
            _paginacion.MoveToFirstPage();
            //volver a llamar a fuente de datos
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void cmdPrevio_Click(object sender, RoutedEventArgs e)
        {
            _paginacion.MoveToPreviousPage();
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void cmdSiguiente_Click(object sender, RoutedEventArgs e)
        {
            _paginacion.MoveToNextPage();
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void cmdUltimo_Click(object sender, RoutedEventArgs e)
        {
            //determinar página
            _paginacion.MoveToLastPage();
            //volver a llamar a fuente de datos
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void cmdCerrar_Click(object sender, RoutedEventArgs e)
        {
            formularioItemSeleccionado = false;
            this.Close();
        }

        private void Button_Click_Corresponsabilidad(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnCorresponsabilidadRegistro(id);

            formularioItemSeleccionado = true;
            this.Close();
        }

        private void Button_Click_Dependiente(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnDependienteRegistro(id);

            formularioItemSeleccionado = true;
            this.Close();
        }

        private void Button_Click_Detalles(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnMostrarDetallesRegistro(id);
        }

        private void Button_Click_Modificar(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnModificarRegistro(id);

            //vuelve a cargar los datos
            //volver a llamar a fuente de datos
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void Button_Click_Borrar(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnBorrarRegistro(id);

            //vuelve a cargar los datos
            _paginacion.MoveToFirstPage();
            //volver a llamar a fuente de datos
            _resultadoBusquedaPaginacion = proveedorDatos.listaPaginada(_paginacion.StartIndex, _paginacion.itemsPorPagina, txtCriterioBusqueda.Text);
            dataGridRegistros.ItemsSource = _resultadoBusquedaPaginacion.resultados;
            lblPagina.Content = _paginacion.getPaginationText();
        }

        private void Button_Click_Seleccionar(object sender, RoutedEventArgs e)
        {
            Button img = (Button)sender;
            Int64 id = (Int64)img.Tag;

            OnSeleccionarRegistro(id);

            formularioItemSeleccionado = true;
            this.Close();
        }

    }
}
