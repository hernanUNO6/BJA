using Bja.AccesoDatos;
using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class AdministradorConfiguracion
    {
        public static Configuracion LeerConfiguracion(String nombreCompleto)
        {
            BjaContext context = new BjaContext();

            Configuracion configuracion = (from c in context.Configuraciones
                     where c.Clase + "." + c.Nombre == nombreCompleto
                     select c).FirstOrDefault();

            if (configuracion == null)
            {
                throw new Exception("Configuracion no encontrada.");
            }

            return configuracion;
        }

        public static int LeerConfiguracionEntero(String nombreCompleto)
        {
            Configuracion configuracion = LeerConfiguracion(nombreCompleto);

            if (configuracion.TipoDatoValor == TipoDato.entero)
                return Convert.ToInt32(configuracion.Valor);
            else
                throw new Exception("Configuracion especificada no es de tipo entero.");
        }

        public static long LeerConfiguracionEnteroLargo(String nombreCompleto)
        {
            Configuracion configuracion = LeerConfiguracion(nombreCompleto);

            if (configuracion.TipoDatoValor == TipoDato.enterolargo)
                return Convert.ToInt64(configuracion.Valor);
            else
                throw new Exception("Configuracion especificada no es de tipo entero largo.");
        }

        public static String LeerConfiguracionCadena(String nombreCompleto)
        {
            Configuracion configuracion = LeerConfiguracion(nombreCompleto);
            
            if (configuracion.TipoDatoValor == TipoDato.cadena)
                return configuracion.Valor;
            else
                throw new Exception("Configuracion especificada no es de tipo cadena.");
        }

        public static void Crear(String nombre, String clase, String valor, TipoDato tipoDato)
        {
            BjaContext context = new BjaContext();

            Configuracion configuracion = new Configuracion();

            configuracion.Id = IdentifierGenerator.NewId();
            configuracion.IdSesion = SessionManager.getCurrentSession().Id;
            configuracion.Nombre = nombre;
            configuracion.Clase = clase;
            configuracion.Valor = valor;
            configuracion.TipoDatoValor = tipoDato;

            context.Configuraciones.Add(configuracion);

            context.SaveChanges();
        }


    }
}
