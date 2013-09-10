using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bja.Entidades;
using Bja.Modelo;

namespace Bja.Central.Web.Controllers
{
    public class DatosController : Controller
    {
        private ModeloDepartamento modDepto = new ModeloDepartamento();
        private ModeloProvincia modProvincia = new ModeloProvincia();
        private ModeloMunicipio modMunicipio = new ModeloMunicipio();

        public string Index()
        {
            modDepto.EliminarTodo();
            modProvincia.EliminarTodo();
            modMunicipio.EliminarTodo();

            for (int i = 1; i <= 9; i++)
            {
                Departamento depto = new Departamento();

                depto.Id = IdentifierGenerator.NewId();
                depto.IdSesion = 1;
                depto.FechaUltimaTransaccion = DateTime.Now;
                depto.FechaRegistro = DateTime.Now;
                depto.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
                depto.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                depto.DescripcionEstadoSincronizacion = "";

                switch (i)
                {
                    case 1: depto.Codigo = "01"; depto.Descripcion = "Chuquisaca"; break;
                    case 2: depto.Codigo = "02"; depto.Descripcion = "La Paz"; break;
                    case 3: depto.Codigo = "03"; depto.Descripcion = "Cochabamba"; break;
                    case 4: depto.Codigo = "04"; depto.Descripcion = "Oruro"; break;
                    case 5: depto.Codigo = "05"; depto.Descripcion = "Potosi"; break;
                    case 6: depto.Codigo = "06"; depto.Descripcion = "Tarija"; break;
                    case 7: depto.Codigo = "07"; depto.Descripcion = "Santa Cruz"; break;
                    case 8: depto.Codigo = "08"; depto.Descripcion = "Beni"; break;
                    case 9: depto.Codigo = "09"; depto.Descripcion = "Pando"; break;
                }
                modDepto.Crear(depto);

                int codMunicipio = 1;
                for (int j = 1; j <= 10; j++)
                {
                    Provincia provincia = new Provincia();

                    provincia.IdDepartamento = depto.Id;
                    provincia.Id = IdentifierGenerator.NewId();
                    provincia.IdSesion = 1;
                    provincia.FechaUltimaTransaccion = DateTime.Now;
                    provincia.FechaRegistro = DateTime.Now;
                    provincia.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
                    provincia.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    provincia.DescripcionEstadoSincronizacion = "";

                    provincia.Codigo = j.ToString().PadLeft(2, '0');

                    switch (i)
                    {
                        case 1: provincia.Descripcion = "Chuquisaca"; break;
                        case 2: provincia.Descripcion = "La Paz"; break;
                        case 3: provincia.Descripcion = "Cochabamba"; break;
                        case 4: provincia.Descripcion = "Oruro"; break;
                        case 5: provincia.Descripcion = "Potosi"; break;
                        case 6: provincia.Descripcion = "Tarija"; break;
                        case 7: provincia.Descripcion = "Santa Cruz"; break;
                        case 8: provincia.Descripcion = "Beni"; break;
                        case 9: provincia.Descripcion = "Pando"; break;
                    }
                    provincia.Descripcion += provincia.Codigo;
                    modProvincia.Crear(provincia);


                    for (int k = 1; k <= 5; k++)
                    {
                        Municipio municipio = new Municipio();

                        municipio.IdProvincia = provincia.Id;
                        municipio.Id = IdentifierGenerator.NewId();
                        municipio.IdSesion = 1;
                        municipio.FechaUltimaTransaccion = DateTime.Now;
                        municipio.FechaRegistro = DateTime.Now;
                        municipio.EstadoRegistro = TipoEstadoRegistro.VigenteNuevoRegistro;
                        municipio.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                        municipio.DescripcionEstadoSincronizacion = "";

                        municipio.Codigo = codMunicipio.ToString().PadLeft(2, '0');

                        switch (i)
                        {
                            case 1: municipio.Descripcion = "Chuquisaca"; break;
                            case 2: municipio.Descripcion = "La Paz"; break;
                            case 3: municipio.Descripcion = "Cochabamba"; break;
                            case 4: municipio.Descripcion = "Oruro"; break;
                            case 5: municipio.Descripcion = "Potosi"; break;
                            case 6: municipio.Descripcion = "Tarija"; break;
                            case 7: municipio.Descripcion = "Santa Cruz"; break;
                            case 8: municipio.Descripcion = "Beni"; break;
                            case 9: municipio.Descripcion = "Pando"; break;
                        }
                        municipio.Descripcion += provincia.Codigo + municipio.Codigo;
                        modMunicipio.Crear(municipio);

                        codMunicipio++;
                    }
                }
            }

            return "OK";
        }

        public string Madres()
        {
            ModeloRevisionManual mrm = new ModeloRevisionManual();
            mrm.EliminarTodoMadre();

            string connectionString = "Data Source=HERBERTLAPTOP;Initial Catalog=BDPersonas_new;User ID=sa; password=123456";
            string queryString = @"select 
	                                Nombres, 
	                                PrimerApellido, 
	                                SegundoApellido, 
	                                Nombres+PrimerApellido+SegundoApellido AS 'NombreCompleto',
	                                DocumentoIdentidad,
	                                0 AS 'TipoDocumentoIdentidad',
	                                FechaNacimiento	
                                from SECC_1A
                                where idS1_A < 1000 and S1_02 = 2 and FechaNacimiento is not null";

            string resultado = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Madre madre = new Madre();

                    madre.Id = IdentifierGenerator.NewId();
                    madre.IdSesion = 1;
                    madre.FechaUltimaTransaccion = DateTime.Now;
                    madre.FechaRegistro = DateTime.Now;
                    madre.DescripcionEstadoSincronizacion = "";

                    madre.Nombres = reader[0].ToString();
                    madre.PrimerApellido = reader[1].ToString();
                    madre.SegundoApellido = reader[2].ToString();
                    madre.TercerApellido = "";
                    madre.NombreCompleto = madre.Nombres + madre.PrimerApellido + madre.SegundoApellido + madre.TercerApellido;
                    madre.DocumentoIdentidad = reader[4].ToString();
                    madre.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad;
                    madre.FechaNacimiento = Convert.ToDateTime(reader[6].ToString());

                    //madre.IdDepartamento = 69;
                    //madre.IdProvincia = 540;
                    //madre.IdMunicipio = 2254;
                    madre.IdDepartamento = 1640759134194000000;
                    madre.IdProvincia = 1640759134243000000;
                    madre.IdMunicipio = 1640759134295000000;
                    madre.LocalidadNacimiento = "";
                    madre.Defuncion = false;

                    mrm.Crear(madre);
                }
                reader.Close();
            }
            return "OK: " + resultado;
        }

        public string MadresTemporal()
        {
            ModeloRevisionManual mrm = new ModeloRevisionManual();
            mrm.EliminarTodoMadreTempo();

            string connectionString = "Data Source=HERBERTLAPTOP;Initial Catalog=BDPersonas_new;User ID=sa; password=123456";
            string queryString = @"select 
	                                Nombres, 
	                                PrimerApellido, 
	                                SegundoApellido, 
	                                Nombres+PrimerApellido+SegundoApellido AS 'NombreCompleto',
	                                DocumentoIdentidad,
	                                0 AS 'TipoDocumentoIdentidad',
	                                FechaNacimiento	
                                from SECC_1A
                                where idS1_A < 100 and S1_02 = 2 and FechaNacimiento is not null";

            string resultado = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MadreTemporal madreTempo = new MadreTemporal();

                    madreTempo.Id = IdentifierGenerator.NewId();
                    madreTempo.IdSesion = 1;
                    madreTempo.FechaUltimaTransaccion = DateTime.Now;
                    madreTempo.FechaRegistro = DateTime.Now;
                    madreTempo.DescripcionEstadoSincronizacion = "";

                    madreTempo.Nombres = reader[0].ToString();
                    madreTempo.PrimerApellido = reader[1].ToString();
                    madreTempo.SegundoApellido = reader[2].ToString();
                    madreTempo.TercerApellido = "";
                    madreTempo.NombreCompleto = madreTempo.Nombres + madreTempo.PrimerApellido + madreTempo.SegundoApellido + madreTempo.TercerApellido;
                    madreTempo.DocumentoIdentidad = reader[4].ToString();
                    madreTempo.TipoDocumentoIdentidad = TipoDocumentoIdentidad.CarnetIdentidad;
                    madreTempo.FechaNacimiento = Convert.ToDateTime(reader[6].ToString());

                    //madreTempo.IdDepartamento = 69;
                    //madreTempo.IdProvincia = 540;
                    //madreTempo.IdMunicipio = 2254;
                    madreTempo.IdDepartamento = 1640759134194000000;
                    madreTempo.IdProvincia = 1640759134243000000;
                    madreTempo.IdMunicipio = 1640759134295000000;

                    madreTempo.LocalidadNacimiento = "";
                    madreTempo.Defuncion = false;

                    mrm.Crear(madreTempo);
                }
                reader.Close();
            }
            return "OK: " + resultado;
        }
    }
}
