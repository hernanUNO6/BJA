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
                case "CorresponsabilidadMadre":
                    CorresponsabilidadMadreLog corresponsabilidadMadreLog = new CorresponsabilidadMadreLog();
                    
                    object corresponsabilidadmadrelogobj = (object)corresponsabilidadMadreLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref corresponsabilidadmadrelogobj);
                    
                    corresponsabilidadMadreLog.IdLog = IdentifierGenerator.NewId();
                    corresponsabilidadMadreLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    corresponsabilidadMadreLog.DescripcionEstado = "Pendiente";
                    corresponsabilidadMadreLog.UltimoRegistro = true;

                    context.CorresponsabilidadMadreLog.Add(corresponsabilidadMadreLog);
                    context.SaveChanges();
                    break;
                case "ControlMadre":
                    ControlMadreLog controlMadreLog = new ControlMadreLog();

                    object controlmadrelogobj = (object)controlMadreLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref controlmadrelogobj);

                    controlMadreLog.IdLog = IdentifierGenerator.NewId();
                    controlMadreLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    controlMadreLog.DescripcionEstado = "Pendiente";
                    controlMadreLog.UltimoRegistro = true;

                    context.ControlMadreLog.Add(controlMadreLog);
                    context.SaveChanges();
                    break;

                case "Menor":
                    MenorLog menorLog = new MenorLog();

                    object menorLogObj = (object)menorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref menorLogObj);

                    menorLog.IdLog = IdentifierGenerator.NewId();
                    menorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    menorLog.DescripcionEstado = "Pendiente";
                    menorLog.UltimoRegistro = true;

                    context.MenorLog.Add(menorLog);
                    context.SaveChanges();

                    break;
                case "CorresponsabilidadMenor":
                    CorresponsabilidadMenorLog corresponsabilidadMenorLog = new CorresponsabilidadMenorLog();

                    object corresponsabilidadMenorLogObj = (object)corresponsabilidadMenorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref corresponsabilidadMenorLogObj);

                    corresponsabilidadMenorLog.IdLog = IdentifierGenerator.NewId();
                    corresponsabilidadMenorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    corresponsabilidadMenorLog.DescripcionEstado = "Pendiente";
                    corresponsabilidadMenorLog.UltimoRegistro = true;

                    context.CorresponsabilidadMenorLog.Add(corresponsabilidadMenorLog);
                    context.SaveChanges();
                    break;
                case "ControlMenor":
                    ControlMenorLog controlMenorLog = new ControlMenorLog();

                    object controlMenorLogObj = (object)controlMenorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref controlMenorLogObj);

                    controlMenorLog.IdLog = IdentifierGenerator.NewId();
                    controlMenorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    controlMenorLog.DescripcionEstado = "Pendiente";
                    controlMenorLog.UltimoRegistro = true;

                    context.ControlMenorLog.Add(controlMenorLog);
                    context.SaveChanges();
                    break;

                case "Tutor":
                    TutorLog tutorLog = new TutorLog();

                    object tutorLogObj = (object)tutorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref tutorLogObj);

                    tutorLog.IdLog = IdentifierGenerator.NewId();
                    tutorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    tutorLog.DescripcionEstado = "Pendiente";
                    tutorLog.UltimoRegistro = true;

                    context.TutorLog.Add(tutorLog);
                    context.SaveChanges();

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
