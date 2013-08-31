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
        /*
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
            /*
            switch (tipoClase.Name)
            {
                case "Madre":
                    MadreTemporal madreLog = new MadreTemporal();
                    
                    object madrelogobj = (object)madreLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref madrelogobj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroMadre = (from m in context.MadreLog
                                             where m.Id == madreLog.Id
                                             && m.UltimoRegistro == true
                                             select m).FirstOrDefault();

                    ultimoRegistroMadre.UltimoRegistro = false;

                    //insertar nuevo registro
                    madreLog.IdLog = IdentifierGenerator.NewId();
                    madreLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    madreLog.DescripcionEstado = "Pendiente";
                    madreLog.UltimoRegistro = true;

                    context.MadreLog.Add(madreLog);
                    context.SaveChanges();

                    break;
                case "CorresponsabilidadMadre":
                    CorresponsabilidadMadreTemporal corresponsabilidadMadreLog = new CorresponsabilidadMadreTemporal();
                    
                    object corresponsabilidadmadrelogobj = (object)corresponsabilidadMadreLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref corresponsabilidadmadrelogobj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroCorresponsabilidadMadre = (from m in context.CorresponsabilidadMadreLog
                                                                 where m.Id == corresponsabilidadMadreLog.Id
                                               && m.UltimoRegistro == true
                                               select m).FirstOrDefault();

                    ultimoRegistroCorresponsabilidadMadre.UltimoRegistro = false;

                    
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

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistrocontrolMadreLog = (from m in context.ControlMadreLog
                                                                 where m.Id == controlMadreLog.Id
                                                                    && m.UltimoRegistro == true
                                                                 select m).FirstOrDefault();

                    ultimoRegistrocontrolMadreLog.UltimoRegistro = false;
                    
                    controlMadreLog.IdLog = IdentifierGenerator.NewId();
                    controlMadreLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    controlMadreLog.DescripcionEstado = "Pendiente";
                    controlMadreLog.UltimoRegistro = true;

                    context.ControlMadreLog.Add(controlMadreLog);
                    context.SaveChanges();
                    break;

                case "Menor":
                    MenorTemporal menorLog = new MenorTemporal();

                    object menorLogObj = (object)menorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref menorLogObj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroMenorLog = (from m in context.MenorLog
                                                         where m.Id == menorLog.Id
                                                            && m.UltimoRegistro == true
                                                         select m).FirstOrDefault();

                    ultimoRegistroMenorLog.UltimoRegistro = false;

                    menorLog.IdLog = IdentifierGenerator.NewId();
                    menorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    menorLog.DescripcionEstado = "Pendiente";
                    menorLog.UltimoRegistro = true;

                    context.MenorLog.Add(menorLog);
                    context.SaveChanges();

                    break;
                case "CorresponsabilidadMenor":
                    CorresponsabilidadMenorTemporal corresponsabilidadMenorLog = new CorresponsabilidadMenorTemporal();

                    object corresponsabilidadMenorLogObj = (object)corresponsabilidadMenorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref corresponsabilidadMenorLogObj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroCorresponsabilidadMenorLog = (from m in context.CorresponsabilidadMenorLog
                                                                    where m.Id == corresponsabilidadMenorLog.Id
                                                     && m.UltimoRegistro == true
                                                  select m).FirstOrDefault();

                    ultimoRegistroCorresponsabilidadMenorLog.UltimoRegistro = false;

                    corresponsabilidadMenorLog.IdLog = IdentifierGenerator.NewId();
                    corresponsabilidadMenorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    corresponsabilidadMenorLog.DescripcionEstado = "Pendiente";
                    corresponsabilidadMenorLog.UltimoRegistro = true;

                    context.CorresponsabilidadMenorLog.Add(corresponsabilidadMenorLog);
                    context.SaveChanges();
                    break;
                case "ControlMenor":
                    ControlMenorTemporal controlMenorLog = new ControlMenorTemporal();

                    object controlMenorLogObj = (object)controlMenorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref controlMenorLogObj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroControlMenorLog = (from m in context.ControlMenorLog
                                                         where m.Id == controlMenorLog.Id
                                                     && m.UltimoRegistro == true
                                                                    select m).FirstOrDefault();

                    ultimoRegistroControlMenorLog.UltimoRegistro = false;

                    controlMenorLog.IdLog = IdentifierGenerator.NewId();
                    controlMenorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    controlMenorLog.DescripcionEstado = "Pendiente";
                    controlMenorLog.UltimoRegistro = true;

                    context.ControlMenorLog.Add(controlMenorLog);
                    context.SaveChanges();
                    break;

                case "Tutor":
                    TutorTemporal tutorLog = new TutorTemporal();

                    object tutorLogObj = (object)tutorLog;

                    SoporteObjetos.CopiarDatosObjetos(clase, ref tutorLogObj);

                    //Limpiar todos los registros previos como no vigentes
                    var ultimoRegistroTutorLog = (from m in context.TutorLog
                                                  where m.Id == tutorLog.Id
                                                     && m.UltimoRegistro == true
                                                         select m).FirstOrDefault();

                    ultimoRegistroTutorLog.UltimoRegistro = false;

                    tutorLog.IdLog = IdentifierGenerator.NewId();
                    tutorLog.EstadoSincronizacion = TipoEstadoSincronizacion.Pendiente;
                    tutorLog.DescripcionEstado = "Pendiente";
                    tutorLog.UltimoRegistro = true;

                    context.TutorLog.Add(tutorLog);
                    context.SaveChanges();

                    break;            
            }
            */
            /*
            object claseDestino = System.Activator.CreateInstance(Type.GetType(tipoClase.FullName + "Log"));

            claseDestino = clase;

            var class1Type = Type.GetType(tipoClase.FullName + "Log"); //typeof(claseDestino);

            class1Type.GetProperty("IdLog").SetValue(claseDestino, IdentifierGenerator.NewId());

            class1Type.GetProperty("Enviado").SetValue(claseDestino, false);

            var class2Type = Type.GetType();
             * */


       // }*/
    }
}
