﻿using Bja.Registro.Modelo;
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

namespace Bja.Registro.Aplicacion
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            InicializacionBD.inicializarBD();

        }

        private void aceptarButton_Click(object sender, RoutedEventArgs e)
        {

            //authenticar usuario
            var rbac = new Rbac();

            //rbac.insertUser("rosario", "Rosario", "rosario", 1);


            Boolean result = rbac.authenticate(usuarioTextBox.Text, clavePasswordBox.Password);

            if (result)
            {
                MessageBox.Show("Logeado");
            }
            else
            {
                MessageBox.Show("usuario o clave invalida.");
            }


        }
    }
}