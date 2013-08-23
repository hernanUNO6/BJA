using Bja.AccesoDatos;
using Bja.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class Logger
    {
        public static void log(object clase)
        {
            BjaContext context = new BjaContext();

            Type tipoClase = clase.GetType();
            /*
            String nombreTipoClaseDestino = tipoClase.FullName + "Log"; //Bja.Entidades.MadreLog

            Type tipoClaseDestino = Type.GetType(nombreTipoClaseDestino);

            object claseDestino = System.Activator.CreateInstance(tipoClaseDestino);

            SoporteObjetos.CopiarDatosObjetos(clase, ref claseDestino);

            var class1Type = Type.GetType(tipoClase.FullName + "Log"); //typeof(claseDestino);

            class1Type.GetProperty("IdLog").SetValue(claseDestino, IdentifierGenerator.NewId());

            class1Type.GetProperty("Enviado").SetValue(claseDestino, false);

            var classContextType = context.GetType(); //Type.GetType("Bja.AccesoDatos.BjaContext.MadresLog" + tipoClase.FullName + "Log");

            var classLog = classContextType.GetProperty(tipoClase.Name + "Log").GetValue(context); // .SetValue(claseDestino, false);

            var classLogAddMethod = classLog.GetType().GetMethod("Add");

            classLogAddMethod.Invoke(classLog, new object[] { claseDestino });
            context.SaveChanges();

            */
            
            switch (tipoClase.Name)
            {
                case "Madre":
                    MadreLog madreLog = new MadreLog();
                    
                    object madrelogobj = (object)madreLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref madrelogobj);
                    
                    madreLog.IdLog = IdentifierGenerator.NewId();
                    madreLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    madreLog.DescripcionEstado = "Pendiente";
                    madreLog.UltimoRegistro = true;

                    context.MadreLog.Add(madreLog);
                    context.SaveChanges();

                    break;
                case "Tutor":

                    break;
            }

            /*
            object claseDestino = System.Activator.CreateInstance(Type.GetType(tipoClase.FullName + "Log"));

            claseDestino = clase;

            var class1Type = Type.GetType(tipoClase.FullName + "Log"); //typeof(claseDestino);

            class1Type.GetProperty("IdLog").SetValue(claseDestino, IdentifierGenerator.NewId());

            class1Type.GetProperty("Enviado").SetValue(claseDestino, false);

            var class2Type = Type.GetType();
             * */


        }
    }
}
